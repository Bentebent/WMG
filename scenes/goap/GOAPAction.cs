using wmg;

namespace core
{
    public abstract class GOAPAction<T>
    {
        public string Name { get; protected set; }
        protected GameManager _gameManager = null;

        public GOAPAction(string name, GameManager gameManager)
        {
            Name = name;
            _gameManager = gameManager;
        }

        public abstract float GetCost(WorldState state);
        public abstract bool CheckPreconditions(WorldState state);
        public abstract WorldState ApplyEffects(WorldState state);

        public abstract void EnterAction(GOAPAgent<T> agent, T actor);
        public abstract void ExitAction(GOAPAgent<T> agent, T actor, bool interrupted = false);
        public abstract bool Run(GOAPAgent<T> agent, T actor);
    }
}
