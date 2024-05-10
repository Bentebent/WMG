using System;
using Godot;

namespace wmg
{
    public partial class LevelManager : Node
    {
        public Map CurrentLevel { get; private set; }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready() { }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta) { }

        public void LoadLevel(string levelPath)
        {
            if (CurrentLevel != null)
            {
                //Handle this
                GD.Print("Level already loaded");
            }

            //Load selected level
            var levelScene = GD.Load<PackedScene>(levelPath);
            CurrentLevel = levelScene.Instantiate() as Map;

            AddChild(CurrentLevel);

            //Spawn player
            var playerAgent =
                GD.Load<PackedScene>("res://scenes/player/PlayerAgent.tscn").Instantiate()
                as PlayerAgent;
            CurrentLevel.AddChild(playerAgent);
            playerAgent.GlobalPosition = CurrentLevel.GetRandomEntrancePosition();
        }
    }
}
