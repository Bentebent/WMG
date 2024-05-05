using System.Collections.Generic;
using System.Linq;

namespace core
{
    public class GOAPPlanner
    {
        public GOAPPlanner() { }

        public List<GOAPAction> Plan(
            WorldState currentState,
            WorldState goalState,
            List<GOAPAction> availableActions
        )
        {
            var open = new List<GOAPNode>();
            var closed = new List<GOAPNode>();

            open.Add(
                new GOAPNode(
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
                    List<GOAPAction> plan = new List<GOAPAction>();
                    while (current.Parent != null)
                    {
                        plan.Add(current.Action);
                        current = current.Parent;
                    }
                    plan.Reverse();
                    return plan;
                }

                foreach (var action in availableActions)
                {
                    //Action is not applicable for the current state
                    if (!action.CheckPreconditions(current.State))
                    {
                        continue;
                    }

                    var afterActionState = action.ApplyEffects(current.State.DeepCopy());

                    if (GetNodeWithState(afterActionState, closed) != null)
                    {
                        continue;
                    }

                    var existingOpenNode = GetNodeWithState(afterActionState, open);
                    if (existingOpenNode == null)
                    {
                        var newNode = new GOAPNode(
                            afterActionState,
                            current.G + action.GetCost(current.State),
                            Heuristic(afterActionState, goalState),
                            action,
                            current
                        );
                        open.Add(newNode);
                        open.OrderBy(x => x.F);
                    }
                    else
                    {
                        if (current.G + action.GetCost(current.State) < existingOpenNode.G)
                        {
                            existingOpenNode.G = current.G + action.GetCost(current.State);
                            existingOpenNode.H = Heuristic(afterActionState, goalState);
                            existingOpenNode.Action = action;
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

        private GOAPNode GetNodeWithState(WorldState state, List<GOAPNode> list)
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
