using System.ComponentModel;
using core;
using Godot;

namespace wmg
{
    public class FetchKeyAction<T> : BaseMoveAction<T>
        where T : Node3D, INavigatable3D
    {
        private Key _targetKey = null;

        public FetchKeyAction(string name, GameManager gameManager, Key targetKey)
            : base(name, gameManager)
        {
            _targetKey = targetKey;
        }

        public override float GetCost(WorldState state)
        {
            return 5.0f;
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
            return base.ProceduralCondition(currentState, goalState, actor);
        }

        public override WorldState ApplyEffects(WorldState state)
        {
            return state;
        }

        protected override Vector3 GetTargetPosition(T actor)
        {
            return _targetKey.GlobalPosition;
        }

        public override void EnterAction(GOAPAgent<T> agent, T actor)
        {
            actor.NavigationAgent.TargetPosition = GetTargetPosition(actor);
        }

        public override void ExitAction(GOAPAgent<T> agent, T actor, bool interrupted)
        {
            base.ExitAction(agent, actor, interrupted);
        }

        public override bool Run(GOAPAgent<T> agent, T actor)
        {
            base.Run(agent, actor);

            if (actor.GlobalPosition.DistanceTo(GetTargetPosition(actor)) < 1.0f)
            {
                if (actor is DoorTestAgent doorTestAgent)
                {
                    doorTestAgent.Keys.Add(_targetKey);
                    _targetKey.Visible = false;
                    return true;
                }
            }

            return false;
        }

        public override GOAPAction<T> ShallowCopy()
        {
            return new FetchKeyAction<T>(Name, _gameManager, _targetKey);
        }
    }
}
