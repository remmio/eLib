using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eLib.Caching
{
   
    public class MemoryCache <TValue> where TValue : class
    {
        // ReSharper disable once InconsistentNaming
        private static readonly ConcurrentDictionary<Guid, KeyValuePair<TValue, DateTime>> _innerDictionary 
            = new ConcurrentDictionary<Guid, KeyValuePair<TValue,DateTime>>();
            
        private readonly TimeSpan _expirySpan = TimeSpan.FromSeconds(30);
            
        public MemoryCache()
        {
            
        }
        public MemoryCache(TimeSpan expirySpan)
        {
            _expirySpan = expirySpan;
        }
                     
        public void Add(Guid key, TValue value)
        {
            _innerDictionary.TryAdd(key, new KeyValuePair<TValue, DateTime>(value, DateTime.Now + _expirySpan));
        }

        /// <summary>
        /// Return the cached object else default(type)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue Get(Guid key)
        {
            KeyValuePair<TValue, DateTime> value;
            _innerDictionary.TryGetValue(key, out value);
            if (value.Value >= DateTime.Now) return value.Key;
            Clear(key);
            return default(TValue);
        }

        public async Task<TValue> Get(Guid key, Func<Task<TValue>> func) 
        {
            var cached = Get(key);
            if (cached != default(TValue))
                return cached;

            var queried = await func();

            if (queried != null)
                Add(key, queried);
            return queried;
        }

        public static bool Contains(Guid key)
        {
            if (!_innerDictionary.ContainsKey(key)) return false;
            if (_innerDictionary[key].Value > DateTime.Now) return true;

            KeyValuePair<TValue, DateTime> toRemove;
            _innerDictionary.TryRemove(key, out toRemove);
            return false;
        }
        
        public void Clear(Guid key)
        {          
            if (!Contains(key)) return;
            KeyValuePair<TValue, DateTime> toRemove;
            _innerDictionary.TryRemove(key, out toRemove);
        }
        
        public static void Clear()
        {
            _innerDictionary.Clear();
        }
        
        public int Count
        {
            get
            {              
                Clean();
                return _innerDictionary.Count;              
            }
        }
        
        public void Clean()
        {
            foreach (var cache in _innerDictionary.Where(kvp => kvp.Value.Value < DateTime.Now))
                Clear(cache.Key);
        }
        
    }
}
