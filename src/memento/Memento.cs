using System;

namespace core
{
    public class Memento<T>
        where T : IDeepCopyable<T>
    {
        private T _state;

        public Memento(T state)
        {
            _state = state.DeepCopy();
        }

        public T GetState()
        {
            return _state;
        }
    }
}
