using Godot;

namespace wmg
{
    public class CollectFoodAction : core.GOAPAction
    {
        public CollectFoodAction()
            : base("CollectFood") { }

        public override float GetCost(core.WorldState worldState)
        {
            return 1.0f;
        }

        public override bool CheckPreconditions(core.WorldState worldState)
        {
            return true;
        }

        public override core.WorldState ApplyEffects(core.WorldState worldState)
        {
            worldState.Set("hasFood", true);
            return worldState;
        }

        public override void Run(core.GOAPAgent agent)
        {
            GD.Print("CollectFoodAction: Running");
        }
    }
}
