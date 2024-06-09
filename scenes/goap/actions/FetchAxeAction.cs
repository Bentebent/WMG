using System.Collections.Generic;
using core;
using Godot;

namespace wmg
{
    public class FetchAxeAction<T> : BaseMoveAction<T>
        where T : Node3D, INavigatable3D
    {
        public FetchAxeAction(string name, GameManager gameManager)
            : base(name, gameManager) { }

        public override float GetCost(WorldState state)
        {
            return 5.0f;
        }

        public override bool CheckPreconditions(WorldState state)
        {
            //TODO: Check if axe is not in inventory and if inventory has space
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
            //TODO: Add axe to inventory
            state.Set("hasAxe", true);
            return state;
        }

        protected override Vector3 GetTargetPosition(T actor)
        {
            var foo = actor.GetParent().GetNode<Node3D>("SM_Wep_Bat_01");
            return foo.GlobalPosition;
        }

        public override void EnterAction(GOAPAgent<T> agent, T actor)
        {
            //Placeholder duh
            actor.NavigationAgent.TargetPosition = GetTargetPosition(actor);

            /*
            //Find nearest axe
            var axes = _gameManager.GetTree().GetNodesInGroup("Axe");
            var selectedAxe = axes[0] as Node3D;

            agent.WorldState.Set("axeTarget", selectedAxe.GlobalTransform.origin);
            actor.NavigationAgent.TargetPosition = agent.WorldState.Get<Vector3>("axeTarget");
            */
        }

        public override void ExitAction(GOAPAgent<T> agent, T actor, bool interrupted)
        {
            base.ExitAction(agent, actor, interrupted);
        }

        public override bool Run(GOAPAgent<T> agent, T actor)
        {
            return base.Run(agent, actor);
        }

        public override GOAPAction<T> ShallowCopy()
        {
            return new FetchAxeAction<T>(Name, _gameManager);
        }
    }
}
