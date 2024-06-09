using System.Collections.Generic;
using System.Linq;
using core;
using Godot;

namespace wmg
{
    public abstract class BaseMoveAction<T> : GOAPAction<T>
        where T : Node3D, INavigatable3D
    {
        public BaseMoveAction(string name, GameManager gameManager)
            : base(name, gameManager) { }

        public override float GetCost(WorldState state)
        {
            return 1;
        }

        public override bool CheckPreconditions(WorldState state)
        {
            return true;
        }

        public override bool ProceduralCondition(
            WorldState currentState,
            WorldState goalState,
            T actor
        )
        {
            Vector3 startPosition = actor.GlobalPosition;
            Vector3 targetPosition = GetTargetPosition(actor);

            //Navigation layers
            //TODO
            uint availableNavigationLayers = 1;
            availableNavigationLayers = availableNavigationLayers | (1 << 1);

            //if (currentState.TryGet("navigationMap", out Rid navigationMap))
            {
                var queryParams = new NavigationPathQueryParameters3D
                {
                    Map = actor.GetWorld3D().NavigationMap,
                    NavigationLayers = availableNavigationLayers,
                    StartPosition = startPosition,
                    TargetPosition = targetPosition
                };

                var queryResult = new NavigationPathQueryResult3D();
                NavigationServer3D.QueryPath(queryParams, queryResult);
                if (queryResult.Path.Count() > 0)
                {
                    foreach (ulong owner in queryResult.PathOwnerIds)
                    {
                        var node = GodotObject.InstanceFromId(owner);
                        if (node is NavigationLink3D link)
                        {
                            var parent = link.GetParent();
                            if (parent is Door door)
                            {
                                _doors.Enqueue(door);
                            }
                        }
                    }
                }
            }

            return true;
        }

        public override WorldState ApplyEffects(WorldState state)
        {
            return state;
        }

        protected abstract Vector3 GetTargetPosition(T actor);

        public override void EnterAction(GOAPAgent<T> agent, T actor)
        {
            //Clear target
        }

        public override void ExitAction(GOAPAgent<T> agent, T actor, bool interrupted)
        {
            //Clear target
        }

        public override bool Run(GOAPAgent<T> agent, T actor)
        {
            var navigatableAgent = actor as INavigatable3D;

            if (navigatableAgent.NavigationAgent.IsTargetReached())
            {
                return true;
            }

            var transform = (actor as Node3D).GlobalTransform;

            var nextPathPos = navigatableAgent.NavigationAgent.GetNextPathPosition();

            //1.0f is supposed to be the speed of the agent
            var newVelocity = transform.Origin.DirectionTo(nextPathPos) * 1.0f;
            actor.NavigationAgent.Velocity = newVelocity;
            actor.Velocity = actor.NavigationAgent.Velocity;
            actor.MoveAndSlide();

            /*
            var player = actor as PlayerAgent;
            //Move towards target
            var nextPathPos = player.NavigationAgent.GetNextPathPosition();
            var newVelocity = player.GlobalPosition.DirectionTo(nextPathPos) * 1.0f;
            actor.NavigationAgent.Velocity = newVelocity;

            player.Velocity = actor.NavigationAgent.Velocity;
            return true;
            */
            return false;
        }
    }
}
