using System.Collections.Generic;
using wmg;

namespace core
{
    public abstract class GOAPAction<T> : IShallowCopyable<GOAPAction<T>>
    {
        public string Name { get; protected set; }
        protected GameManager _gameManager = null;

        protected Queue<Door> _doors = new Queue<Door>();

        public Queue<Door> Doors => _doors;

        public GOAPAction(string name, GameManager gameManager)
        {
            Name = name;
            _gameManager = gameManager;
        }

        public GOAPAction(GOAPAction<T> other)
        {
            Name = other.Name;
            _gameManager = other._gameManager;
        }

        public abstract float GetCost(WorldState state);
        public abstract bool CheckPreconditions(WorldState state);
        public abstract bool ProceduralCondition(
            WorldState currentState,
            WorldState goalState,
            T actor
        );
        public abstract WorldState ApplyEffects(WorldState state);

        public abstract void EnterAction(GOAPAgent<T> agent, T actor);
        public abstract void ExitAction(GOAPAgent<T> agent, T actor, bool interrupted = false);
        public abstract bool Run(GOAPAgent<T> agent, T actor);

        public abstract GOAPAction<T> ShallowCopy();
    }
}
