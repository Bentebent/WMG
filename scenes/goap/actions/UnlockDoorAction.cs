using core;
using Godot;

namespace wmg
{
    public class UnlockDoorAction<T> : BaseMoveAction<T>
        where T : Node3D, INavigatable3D
    {
        private Door _targetDoor = null;

        public UnlockDoorAction(string name, GameManager gameManager, Door targetDoor)
            : base(name, gameManager)
        {
            _targetDoor = targetDoor;
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
            return _targetDoor.GetOpenPosition(actor.GlobalPosition);
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

            if (actor.GlobalPosition.DistanceTo(GetTargetPosition(actor)) < 0.5f)
            {
                _targetDoor.Unlock();
                return true;
            }

            return false;
        }

        public override GOAPAction<T> ShallowCopy()
        {
            return new UnlockDoorAction<T>(Name, _gameManager, _targetDoor);
        }
    }
}
