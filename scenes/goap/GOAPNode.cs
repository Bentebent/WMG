using System.Reflection.Metadata;

namespace core
{
    public class GOAPNode
    {
        public WorldState State { get; set; }
        public float G { get; set; }
        public float H { get; set; }
        public float F => G + H;
        public GOAPAction Action { get; set; }
        public GOAPNode Parent { get; set; }

        public GOAPNode(WorldState state, float g, float h, GOAPAction action, GOAPNode parent)
        {
            State = state;
            G = g;
            H = h;
            Action = action;
            Parent = parent;
        }
    }
}
