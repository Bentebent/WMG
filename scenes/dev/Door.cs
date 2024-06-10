using System;
using Godot;

public partial class Door : Node3D
{
    [Export]
    public bool IsOpen { get; private set; }

    [Export]
    public bool IsLocked { get; private set; }

    [Export]
    private Node3D _doorContainer = null;

    [Export]
    private NavigationLink3D _navLink = null;

    [Export]
    public wmg.Key Key { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (_doorContainer == null)
        {
            GD.PrintErr("Door container is null");
            return;
        }

        if (IsOpen)
        {
            _doorContainer.RotateY(-Mathf.Pi / 2.0f);
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }

    public void Lock() { }

    public void Unlock()
    {
        IsLocked = false;
    }

    public void Open()
    {
        if (IsLocked || IsOpen)
        {
            return;
        }

        _doorContainer.RotateY(-Mathf.Pi / 2.0f);
    }

    //TODO
    //Should calculate which side of the door we're going to
    public Vector3 GetOpenPosition(Vector3 origin)
    {
        var startDist = _navLink.GetGlobalStartPosition().DistanceSquaredTo(origin);
        var endDist = _navLink.GetGlobalEndPosition().DistanceSquaredTo(origin);

        if (startDist < endDist)
        {
            return _navLink.GetGlobalStartPosition();
        }

        return _navLink.GetGlobalEndPosition();
    }
}
