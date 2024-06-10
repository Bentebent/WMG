using System.Collections.Generic;
using core;
using Godot;

namespace wmg
{
    public partial class Key : Node3D
    {
        public override void _Ready()
        {
            GD.Print("Key is ready");
        }
    }
}
