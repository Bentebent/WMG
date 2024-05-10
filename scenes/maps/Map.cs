using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Godot;

namespace wmg
{
    public partial class Map : Node3D
    {
        [Export]
        private Node3D _entrancesContainer = null;

        [Export]
        private Node3D _exitsContainer = null;

        public List<Entrance> Entrances { get; private set; }
        public List<Exit> Exits { get; private set; }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            Entrances = new List<Entrance>();
            Exits = new List<Exit>();

            foreach (var child in _entrancesContainer.GetChildren())
            {
                if (child is Entrance entrance)
                {
                    Entrances.Add(entrance);
                }
            }

            foreach (var child in _exitsContainer.GetChildren())
            {
                if (child is Exit exit)
                {
                    Exits.Add(exit);
                }
            }
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta) { }

        public Vector3 GetRandomEntrancePosition()
        {
            var randomEntrance = Entrances[GD.RandRange(0, Entrances.Count - 1)];
            return randomEntrance.GetRandomPositionInArea();
        }
    }
}
