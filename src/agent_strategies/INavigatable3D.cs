using Godot;

namespace wmg
{
    public interface INavigatable3D
    {
        NavigationAgent3D NavigationAgent { get; }
        bool MoveAndSlide();
    }
}
