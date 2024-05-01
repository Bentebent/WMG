using System;
using core;
using Godot;

namespace wmg
{
    public partial class EventBus : Node
    {
        private core.EventHandler _eventHandler = new core.EventHandler();

        // Called when the node enters the scene tree for the first time.
        public override void _Ready() { }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta) { }

        public void Subscribe(string eventName, EventDelegate callback)
        {
            _eventHandler.Subscribe(eventName, callback);
        }

        public void Unsubscribe(string eventName, EventDelegate callback)
        {
            _eventHandler.Unsubscribe(eventName, callback);
        }

        public void Publish(string eventName, params object[] args)
        {
            _eventHandler.Publish(eventName, args);
        }
    }
}
