using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using wmg;

namespace core
{
    public class GOAPPlanner
    {
        public GOAPPlanner() { }

        public List<GOAPAction<T>> Plan<T>(
            GameManager gameManager,
            WorldState currentState,
            WorldState goalState,
            List<GOAPAction<T>> availableActions,
            T actor
        )
            where T : Node3D, wmg.INavigatable3D
        {
            var open = new List<GOAPNode<T>>();
            var closed = new List<GOAPNode<T>>();

            open.Add(
                new GOAPNode<T>(
                    currentState.DeepCopy(),
                    0,
                    Heuristic(currentState, goalState),
                    null,
                    null
                )
            );

            while (open.Any())
            {
                var current = open.First();
                open.RemoveAt(0);
                closed.Add(current);

                if (current.State.OtherIsSubset(goalState))
                {
                    List<GOAPAction<T>> plan = new List<GOAPAction<T>>();
                    while (current.Parent != null)
                    {
                        plan.Add(current.Action);
                        current = current.Parent;
                    }
                    plan.Reverse();

                    //Post process plan to insert handling navigation obstacles
                    //TODO
                    //Where do we put this shit?
                    Dictionary<int, List<GOAPAction<T>>> insertPositions =
                        new Dictionary<int, List<GOAPAction<T>>>();
                    foreach (var action in plan)
                    {
                        if (action is BaseMoveAction<T> moveAction)
                        {
                            foreach (var door in action.Doors)
                            {
                                if (door.IsLocked)
                                {
                                    int idx = plan.IndexOf(action);
                                    insertPositions.TryAdd(idx, new List<GOAPAction<T>>());
                                    insertPositions[idx].Add(
                                        new FetchKeyAction<T>(
                                            $"FetchKey {door.Name}",
                                            gameManager,
                                            door.Key
                                        )
                                    );

                                    insertPositions[idx].Add(
                                        new UnlockDoorAction<T>(
                                            $"FetchKey {door.Name}",
                                            gameManager,
                                            door
                                        )
                                    );
                                }
                                if (!door.IsOpen)
                                {
                                    int idx = plan.IndexOf(action);
                                    insertPositions.TryAdd(idx, new List<GOAPAction<T>>());
                                    insertPositions[idx].Add(
                                        new OpenDoorAction<T>(
                                            $"OpenDoor {door.Name}",
                                            gameManager,
                                            door
                                        )
                                    );
                                }
                            }
                        }
                    }

                    foreach (var kvp in insertPositions)
                    {
                        plan.InsertRange(kvp.Key, kvp.Value);
                    }

                    return plan;
                }

                foreach (var action in availableActions)
                {
                    var selectedAction = action.ShallowCopy();
                    //Action is not applicable for the current state
                    if (!selectedAction.CheckPreconditions(current.State))
                    {
                        continue;
                    }

                    if (!selectedAction.ProceduralCondition(current.State, goalState, actor))
                    {
                        continue;
                    }

                    var afterActionState = selectedAction.ApplyEffects(current.State.DeepCopy());

                    if (GetNodeWithState(afterActionState, closed) != null)
                    {
                        continue;
                    }

                    var existingOpenNode = GetNodeWithState(afterActionState, open);
                    if (existingOpenNode == null)
                    {
                        var newNode = new GOAPNode<T>(
                            afterActionState,
                            current.G + selectedAction.GetCost(current.State),
                            Heuristic(afterActionState, goalState),
                            selectedAction,
                            current
                        );
                        open.Add(newNode);
                        open.OrderBy(x => x.F);
                    }
                    else
                    {
                        if (current.G + selectedAction.GetCost(current.State) < existingOpenNode.G)
                        {
                            existingOpenNode.G = current.G + selectedAction.GetCost(current.State);
                            existingOpenNode.H = Heuristic(afterActionState, goalState);
                            existingOpenNode.Action = selectedAction;
                            existingOpenNode.Parent = current;
                            open.OrderBy(x => x.F);
                        }
                    }
                }
            }

            return null;
        }

        protected virtual float Heuristic(WorldState currentState, WorldState goalState)
        {
            return 0.0f;
        }

        private GOAPNode<T> GetNodeWithState<T>(WorldState state, List<GOAPNode<T>> list)
        {
            foreach (var node in list)
            {
                if (node.State.Equals(state))
                {
                    return node;
                }
            }

            return null;
        }
    }
}
