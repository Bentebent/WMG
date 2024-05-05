namespace core
{
    public class Originator<T>
        where T : IDeepCopyable<T>
    {
        private T _state;

        public Originator(T state)
        {
            _state = state;
        }

        public Memento<T> CreateMemento()
        {
            return new Memento<T>(_state);
        }

        public void SetMemento(Memento<T> memento)
        {
            _state = memento.GetState();
        }

        public T GetState()
        {
            return _state;
        }
    }
}
