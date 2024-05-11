using System.Collections.Generic;
using core;
using Godot;

namespace wmg
{
    public partial class PlayerAgent : CharacterBody3D, INavigatable3D
    {
        [Export]
        public NavigationAgent3D NavigationAgent { get; private set; }

        private GOAPAgent<PlayerAgent> _agent = null;
        private GOAPPlanner _planner = null;

        private bool _once = true;

        public override void _Ready()
        {
            var gameManager = GetNode<GameManager>("/root/GameManager");
            _agent = new GOAPAgent<PlayerAgent>(
                new WorldState(),
                new List<GOAPAction<PlayerAgent>>()
                {
                    new FindResourceAction<PlayerAgent>(gameManager),
                    new FindExitAction<PlayerAgent>(gameManager)
                }
            );
            _planner = new GOAPPlanner();

            _agent.WorldState.Set("hasResource", false);
        }

        public override void _Process(double delta)
        {
            if (_once)
            {
                _once = false;

                var goalState = new WorldState();
                goalState.Set("exitMap", true);

                var newPlan = _planner.Plan(_agent.WorldState, goalState, _agent.Actions);
                if (newPlan != null)
                {
                    _agent.CurrentPlan = newPlan;

                    //Check if current action is not null
                    //Handle interrupting actions
                    _agent.CurrentAction = _agent.CurrentPlan.PopAt(0);
                    _agent.CurrentAction.EnterAction(_agent, this);
                }
            }

            if (_agent.CurrentAction?.Run(_agent, this) == true)
            {
                _agent.CurrentAction.ExitAction(_agent, this);
                _agent.CurrentAction =
                    _agent.CurrentPlan.Count > 0 ? _agent.CurrentPlan.PopAt(0) : null;

                _agent.CurrentAction?.EnterAction(_agent, this);
            }
        }
    }
}
