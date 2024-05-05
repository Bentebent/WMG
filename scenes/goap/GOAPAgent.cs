using System.Collections.Generic;

namespace core
{
    public class GOAPAgent
    {
        private WorldState _worldState = null;
        private List<GOAPAction> _actions = new List<GOAPAction>();

        public GOAPAgent(WorldState worldState, List<GOAPAction> actions)
        {
            _worldState = worldState;
            _actions = actions;
        }
    }
}
