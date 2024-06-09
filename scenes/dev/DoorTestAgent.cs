using System;
using System.Collections.Generic;
using core;
using Godot;
using wmg;

public partial class DoorTestAgent : CharacterBody3D, INavigatable3D
{
    [Export]
    public NavigationAgent3D NavigationAgent { get; private set; }
    private GOAPAgent<DoorTestAgent> _agent = null;
    private GOAPPlanner _planner = null;
    private List<GOAPAction<DoorTestAgent>> _currentPlan = null;

    bool once = true;
    bool nav_loaded = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var gameManager = GetNode<GameManager>("/root/GameManager");
        _agent = new GOAPAgent<DoorTestAgent>(
            new WorldState(),
            new List<GOAPAction<DoorTestAgent>>()
            {
                new FetchAxeAction<DoorTestAgent>("FetchAxe", gameManager)
            }
        );
        _planner = new GOAPPlanner();

        _agent.WorldState.Set("hasAxe", false);
        //_agent.WorldState.Set("navigationMap", GetWorld3D().NavigationMap.Id);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        //Agent wants to move to location
        //Get all keys in inventory
        //For each key get connected doors
        //Move doors to "Unlockable" navigation layer
        //Search for path
        //If path found
        //For each position in path
        //Check if it is a door
        //If door is open
        //Move as expected
        //If door is closed but not locked
        //Queue move to door
        //Queue open door
        //Queue move to next position
        //If door is closed and locked
        //Queue move to door
        //Queue unlock door
        //Queue open door
        //Queue move to next position
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!nav_loaded)
        {
            nav_loaded = true;
            return;
        }
        if (once)
        {
            once = false;

            var goalState = new WorldState();
            goalState.Set("hasAxe", true);

            var gameManager = GetNode<GameManager>("/root/GameManager");
            var result = _planner.Plan(
                gameManager,
                _agent.WorldState,
                goalState,
                _agent.Actions,
                this
            );
            if (result != null)
            {
                _agent.CurrentPlan = result;
                _agent.CurrentAction = _agent.CurrentPlan.PopAt(0);
                _agent.CurrentAction.EnterAction(_agent, this);

                GD.Print(_currentPlan);
            }

            /*
            var foo = new NavigationPathQueryParameters3D();
            var bar = new NavigationPathQueryResult3D();

            foo.Map = GetWorld3D().NavigationMap;
            foo.StartPosition = GlobalPosition;
            foo.TargetPosition = Target.GlobalPosition;

            NavigationServer3D.QueryPath(foo, bar);
            GD.Print(bar.Path);
            GD.Print(bar.PathOwnerIds);

            foreach (ulong id in bar.PathOwnerIds)
            {
                var node = GodotObject.InstanceFromId(id);
                GD.Print(node);
            }
            */
        }
        if (_agent.CurrentAction?.Run(_agent, this) == true)
        {
            _agent.CurrentAction.ExitAction(_agent, this);
            _agent.CurrentAction =
                _agent.CurrentPlan.Count > 0 ? _agent.CurrentPlan.PopAt(0) : null;
            _agent.CurrentAction?.EnterAction(_agent, this);
        }
        base._PhysicsProcess(delta);
    }
}
