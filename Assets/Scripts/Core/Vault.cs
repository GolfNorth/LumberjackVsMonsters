using System;
using System.Collections.Generic;

namespace LumberjackVsMonsters
{
    public class Vault<T> where T : class
    {
        private readonly Dictionary<Type, T> _values = new Dictionary<Type, T>();

        public Vault()
        {
            
        }

        public Vault(IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                _values.Add(value.GetType(), value);
            }
        }
        
        public void SetValue<TValue>(TValue value) where TValue : class
        {
            _values.Add(value.GetType(), value as T);
        }
        
        public TValue GetValue<TValue>() where TValue : class
        {
            return _values.ContainsKey(typeof(TValue))
                ? _values[typeof(TValue)] as TValue
                : null;
        }
    }
}