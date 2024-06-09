using Godot;

namespace wmg
{
    /*
    class FindExitAction<T> : core.GOAPAction<T>
        where T : Node3D, INavigatable3D
    {
        public FindExitAction(GameManager gameManager)
            : base("FindExit", gameManager) { }

        public override float GetCost(core.WorldState state)
        {
            return 1;
        }

        public override bool CheckPreconditions(core.WorldState state)
        {
            return true;
        }

        public override bool RunProceduralConditions(
            core.WorldState currentState,
            core.WorldState goalState,
            core.GOAPAgent<T> agent,
            T actor
        )
        {
            return true;
        }

        public override core.WorldState ApplyEffects(core.WorldState state)
        {
            state.Set("exitMap", true);
            return state;
        }

        public override void EnterAction(core.GOAPAgent<T> agent, T actor)
        {
            var closestExit = _gameManager.LevelManager.CurrentLevel.GetClosestExit(
                actor.GlobalPosition
            );

            agent.WorldState.Set("exitTarget", closestExit.GlobalPosition);
            actor.NavigationAgent.TargetPosition = agent.WorldState.Get<Vector3>("exitTarget");
        }

        public override void ExitAction(core.GOAPAgent<T> agent, T actor, bool interrupted)
        {
            //Clear exit target
        }

        public override bool Run(core.GOAPAgent<T> agent, T actor)
        {
            var player = actor as PlayerAgent;
            //Move towards exit
            var nextPathPos = player.NavigationAgent.GetNextPathPosition();
            var newVelocity = player.GlobalPosition.DirectionTo(nextPathPos) * 1.0f;
            actor.NavigationAgent.Velocity = newVelocity;

            player.Velocity = actor.NavigationAgent.Velocity;
            actor.MoveAndSlide();
            return false;
        }
    }
    */
}
