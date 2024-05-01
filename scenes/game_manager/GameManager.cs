using System;
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
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta) { }
    }
}
