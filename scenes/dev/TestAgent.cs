using System;
using System.Collections.Generic;
using System.Linq;
using core;
using Godot;

namespace wmg
{
    public partial class TestAgent
        : CharacterBody2D { /*
        private WorldState _worldState = null;
        private WorldState _goalState = null;
        private GOAPAgent _agent = null;
        private List<GOAPAction> _actions = new List<GOAPAction>();
        private GOAPPlanner _planner = new GOAPPlanner();

        private bool _once = true;
        private List<GOAPAction> _currentPlan = null;

        public override void _Ready()
        {
            _actions = new List<GOAPAction> { new CollectFoodAction(), new EatFoodAction(), };

            _worldState = new WorldState();
            _worldState.Set("hasFood", false);
            _worldState.Set("isHungry", true);

            _goalState = new WorldState();
            _goalState.Set("isHungry", false);

            _agent = new GOAPAgent(_worldState, _actions);
        }

        public override void _Process(double delta)
        {
            // Iterate through sensors
            // Set current worldstate
            // Update goal worldstate if needed

            // If goal worldstate changed OR x seconds has passed
            // Use planner to find a plan
            // If plan found, execute plan
            // Foreach action in plan
            // action.Run(_agent)

            if (_once)
            {
                _once = false;

                _currentPlan = _planner.Plan(_worldState, _goalState, _actions);
            }

            if (_currentPlan.Any())
            {
                _currentPlan[0].Run(_agent);
            }
        }

        public override void _PhysicsProcess(double delta)
        {
            /*
            Vector2 velocity = Velocity;

            // Add the gravity.
            if (!IsOnFloor())
                velocity.Y += gravity * (float)delta;

            // Handle Jump.
            if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
                velocity.Y = JumpVelocity;

            // Get the input direction and handle the movement/deceleration.
            // As good practice, you should replace UI actions with custom gameplay actions.
            Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
            if (direction != Vector2.Zero)
            {
                velocity.X = direction.X * Speed;
            }
            else
            {
                velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            }

            Velocity = velocity;
            MoveAndSlide();
        }
        */
    }
}
