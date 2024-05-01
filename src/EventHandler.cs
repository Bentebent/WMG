//Scaffolded file
using System;
using System.Collections.Generic;
using System.Linq;

namespace core
{
    public delegate void EventDelegate(params object[] args);

    public class EventHandler
    {
        private Dictionary<string, List<EventDelegate>> _subscribedEvents =
            new Dictionary<string, List<EventDelegate>>();

        public void Subscribe(string eventName, EventDelegate callback)
        {
            if (!_subscribedEvents.ContainsKey(eventName))
            {
                _subscribedEvents[eventName] = new List<EventDelegate>();
            }

            _subscribedEvents[eventName].Add(callback);
        }

        public void Unsubscribe(string eventName, EventDelegate callback)
        {
            if (!_subscribedEvents.ContainsKey(eventName))
            {
                return;
            }

            _subscribedEvents[eventName].Remove(callback);
        }

        public void Publish(string eventName, params object[] args)
        {
            if (!_subscribedEvents.ContainsKey(eventName))
            {
                return;
            }

            foreach (var callback in _subscribedEvents[eventName])
            {
                callback(args);
            }
        }
    }
}
