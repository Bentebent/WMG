using Godot;

namespace wmg
{
    public interface INavigatable3D
    {
        NavigationAgent3D NavigationAgent { get; }
        Vector3 Velocity { get; set; }
        bool MoveAndSlide();
    }
}
