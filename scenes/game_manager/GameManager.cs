using System;
using System.Collections.Generic;
using Godot;

namespace wmg
{
    public partial class GameManager : Node
    {
        [Export]
        public EventBus EventBus { get; private set; }

        [Export]
        public DecisionEngine DecisionEngine { get; private set; }

        [Export]
        public UIManager UIManager { get; private set; }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            GD.Print("GameManager Ready");

            core.WorldState worldState = new core.WorldState();
            worldState.Set("hasFood", false);
            worldState.Set("isHungry", true);

            core.WorldState goalState = new core.WorldState();
            goalState.Set("isHungry", false);

            List<core.GOAPAction> actions = new List<core.GOAPAction>()
            {
                new CollectFoodAction(),
                new EatFoodAction()
            };

            core.GOAPPlanner planner = new core.GOAPPlanner();
            var plan = planner.Plan(worldState, goalState, actions);
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta) { }
    }
}
