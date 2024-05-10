using System.Collections.Generic;

namespace core
{
    public class GOAPAgent<T>
    {
        public WorldState WorldState { get; private set; }
        public List<GOAPAction<T>> Actions { get; private set; }
        public List<GOAPAction<T>> CurrentPlan { get; set; }
        public GOAPAction<T> CurrentAction { get; set; }

        public GOAPAgent(WorldState worldState, List<GOAPAction<T>> actions)
        {
            WorldState = worldState;
            Actions = actions;
        }
    }
}
