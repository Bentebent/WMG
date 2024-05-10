using Godot;

namespace wmg
{
    class FindResourceAction<T> : core.GOAPAction<T>
        where T : INavigatable3D
    {
        public FindResourceAction(GameManager gameManager)
            : base("FindResource", gameManager) { }

        public override float GetCost(core.WorldState state)
        {
            return 1;
        }

        public override bool CheckPreconditions(core.WorldState state)
        {
            return state.Get<bool>("hasResource") == false;
        }

        public override core.WorldState ApplyEffects(core.WorldState state)
        {
            state.Set("hasResource", true);
            return state;
        }

        public override void EnterAction(core.GOAPAgent<T> agent, T actor)
        {
            GD.Print("Entering FindResource action");
            //Find nearest resource

            var resources = _gameManager.GetTree().GetNodesInGroup("Resource");
            var selectedResource = resources[0] as Node3D;

            agent.WorldState.Set("resourceTarget", selectedResource.GlobalTransform.Origin);
            actor.NavigationAgent.TargetPosition = agent.WorldState.Get<Vector3>("resourceTarget");
        }

        public override void ExitAction(core.GOAPAgent<T> agent, T actor, bool interrupted)
        {
            //Clear resource target
            GD.Print("Exiting FindResource action");
        }

        public override bool Run(core.GOAPAgent<T> agent, T actor)
        {
            var player = actor as PlayerAgent;
            //Move towards resource
            GD.Print("Running FindResource action");
            var nextPathPos = player.NavigationAgent.GetNextPathPosition();
            var newVelocity = player.GlobalPosition.DirectionTo(nextPathPos) * 1.0f;
            actor.NavigationAgent.Velocity = newVelocity;

            player.Velocity = actor.NavigationAgent.Velocity;
            actor.MoveAndSlide();
            return false;
        }
    }
}
