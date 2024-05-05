using Godot;

namespace wmg
{
    public class EatFoodAction : core.GOAPAction
    {
        public EatFoodAction()
            : base("EatFood") { }

        public override float GetCost(core.WorldState worldState)
        {
            return 1.0f;
        }

        public override bool CheckPreconditions(core.WorldState worldState)
        {
            return worldState.Get<bool>("hasFood");
        }

        public override core.WorldState ApplyEffects(core.WorldState worldState)
        {
            worldState.Set("hasFood", false);
            worldState.Set("isHungry", false);
            return worldState;
        }

        public override void Run(core.GOAPAgent agent)
        {
            GD.Print("EatFoodAction: Running");
        }
    }
}
