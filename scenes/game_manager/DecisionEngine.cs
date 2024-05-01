using System;
using System.Collections.Generic;
using core;
using Godot;

namespace wmg
{
    public partial class DecisionEngine : Node
    {
        /*
        private core.EventHandler _eventHandler = new core.EventHandler();
        private Decision _activeDecision = null;
        private Queue<Decision> _decisionQueue = new Queue<Decision>();
        */

        public override void _Ready()
        {
            /*
            GD.Print("DecisionEngine is ready");
            EnqueueDecision(
                ResourceLoader.Load<Decision>("res://scenes/decision/decisions/dev_decision.tres")
            );
            */
            /*
            _eventHandler.Subscribe(
                "decision",
                (args) =>
                {
                    GD.Print("Decision made: " + args[0]);
                }
            );

            _eventHandler.Publish("decision", "Go left");
            */
        }

        public override void _Process(double delta)
        {
            // Called every frame. 'delta' is the elapsed time since the previous frame.
        }

        /*
        public void EnqueueDecision(Decision decision)
        {

            _decisionQueue.Enqueue(decision);
            _activeDecision ??= _decisionQueue.Dequeue();

            var cardScene = GD.Load<PackedScene>(_activeDecision.CardTemplatePath);
            var instance = cardScene.Instantiate<DecisionCard>();
            instance.SetDecision(_activeDecision);
            AddChild(instance);
        }
        */
    }
}
