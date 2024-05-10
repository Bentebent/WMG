using Godot;

namespace wmg
{
    [Tool]
    public partial class Entrance : Node3D
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

        public override void _EnterTree()
        {
            base._EnterTree();
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            if (!Engine.IsEditorHint())
            {
                _placeholderVisual.QueueFree();
            }

            base._Ready();
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta) { }

        public Vector3 GetRandomPositionInArea()
        {
            return GlobalPosition
                + new Vector3(
                    (float)GD.RandRange(-_areaSize.X / 2.0f, _areaSize.X / 2.0f),
                    0,
                    (float)GD.RandRange(-_areaSize.Y / 2.0f, _areaSize.Y / 2.0f)
                );
        }
    }
}
