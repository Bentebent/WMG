using System;
using core;
using Godot;

namespace wmg
{
    public partial class DecisionEngine : Node
    {
        private core.EventHandler _eventHandler = new core.EventHandler();

        public override void _Ready()
        {
            GD.Print("DecisionEngine is ready");
            _eventHandler.Subscribe(
                "decision",
                (args) =>
                {
                    GD.Print("Decision made: " + args[0]);
                }
            );

            _eventHandler.Publish("decision", "Go left");
        }

        public override void _Process(double delta)
        {
            // Called every frame. 'delta' is the elapsed time since the previous frame.
        }
    }
}
