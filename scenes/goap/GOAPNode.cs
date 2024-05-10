using System.Reflection.Metadata;

namespace core
{
    public class GOAPNode<T>
    {
        public WorldState State { get; set; }
        public float G { get; set; }
        public float H { get; set; }
        public float F => G + H;
        public GOAPAction<T> Action { get; set; }
        public GOAPNode<T> Parent { get; set; }

        public GOAPNode(
            WorldState state,
            float g,
            float h,
            GOAPAction<T> action,
            GOAPNode<T> parent
        )
        {
            State = state;
            G = g;
            H = h;
            Action = action;
            Parent = parent;
        }
    }
}
