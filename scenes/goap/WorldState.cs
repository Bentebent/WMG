using System;
using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;

namespace core
{
    public class WorldState
    {
        [JsonProperty]
        private Dictionary<string, object> _state = new Dictionary<string, object>();

        public void Set(string key, object value)
        {
            _state[key] = value;
        }

        public T Get<T>(string key)
        {
            return (T)_state[key];
        }

        public bool TryGet<T>(string key, out T outValue, T defaultValue = default)
        {
            outValue = defaultValue;
            if (_state.TryGetValue(key, out object val))
            {
                try
                {
                    outValue = (T)val;
                    return true;
                }
                catch (InvalidCastException e)
                {
                    GD.PrintErr("Invalid cast: " + e.Message);
                }
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Equals(obj as WorldState);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(WorldState other)
        {
            if (other == null)
            {
                return false;
            }

            if (_state.Count != other._state.Count)
            {
                return false;
            }

            foreach (var key in _state.Keys)
            {
                if (!other._state.ContainsKey(key) || !_state[key].Equals(other._state[key]))
                {
                    return false;
                }
            }

            return true;
        }

        public bool OtherIsSubset(WorldState other)
        {
            foreach (var key in other._state.Keys)
            {
                if (!_state.ContainsKey(key) || !_state[key].Equals(other._state[key]))
                {
                    return false;
                }
            }

            return true;
        }

        public WorldState DeepCopy()
        {
            var serialized = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<WorldState>(serialized);
        }
    }
}
