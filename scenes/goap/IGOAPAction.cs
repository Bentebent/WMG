namespace core
{
    public abstract class GOAPAction
    {
        public string Name { get; protected set; }

        public GOAPAction(string name)
        {
            Name = name;
        }

        public abstract float GetCost(WorldState worldState);
        public abstract bool CheckPreconditions(WorldState worldState);
        public abstract WorldState ApplyEffects(WorldState worldState);
        public abstract void Run(GOAPAgent agent);
    }
}
