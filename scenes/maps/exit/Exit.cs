using System;
using Godot;

namespace wmg
{
    [Tool]
    public partial class Exit : Node3D
    {
        private Vector2 _areaSize = new Vector2(1, 1);

        [Export]
        private MeshInstance3D _placeholderVisual = null;

        [Export]
        public Vector2 AreaSize
        {
            get => _areaSize;
            set
            {
                _areaSize = value;
                if (_placeholderVisual != null)
                {
                    _placeholderVisual.Scale = new Vector3(value.X, 0.2f, value.Y);
                }
            }
        }

        public override void _Ready()
        {
            if (!Engine.IsEditorHint())
            {
                _placeholderVisual.QueueFree();
            }
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta) { }
    }
}
