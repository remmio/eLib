using System;
using System.Collections.Generic;
using System.Linq;

namespace eLib.Caching
{
    public class Cache<TKey, TValue> : IDisposable
    {
        protected class CacheValue<TCacheKey, TCacheValue>
        {
            public CacheValue(TCacheValue value)
            {
                LastAccess = DateTime.Now;
                Value = value;
            }

            public LinkedListNode<KeyValuePair<TCacheKey, CacheValue<TCacheKey, TCacheValue>>> IndexRef { get; set; }
            public DateTime LastAccess { get; set; }
            public TCacheValue Value { get; set; }
        }

        protected readonly LinkedList<KeyValuePair<TKey, CacheValue<TKey, TValue>>> IndexList = new LinkedList<KeyValuePair<TKey, CacheValue<TKey, TValue>>>();
        private readonly Dictionary<TKey, CacheValue<TKey, TValue>> _valueCache = new Dictionary<TKey, CacheValue<TKey, TValue>>();
        protected object SyncRoot = new object();
        private DateTime _lastCacheAccess = DateTime.MaxValue;

        public virtual int Count
        {
            get
            {
                lock (SyncRoot)
                {
                    return _valueCache.Count;
                }
            }
        }

        public virtual bool TryGetValue(TKey key, out TValue value)
        {
            CacheValue<TKey, TValue> v;
            value = default(TValue);

            lock (SyncRoot)
            {
                _lastCacheAccess = DateTime.Now;
                v = GetCacheValueUnlocked(key);
                if (v != null)
                {
                    value = v.Value;
                    UpdateElementAccess(key, v);
                    return true;
                }
            }

            return false;
        }

        protected virtual void UpdateElementAccess(TKey key, CacheValue<TKey, TValue> cacheValue)
        {
            // update last access and move it to the head of the list
            cacheValue.LastAccess = DateTime.Now;
            var idxRef = cacheValue.IndexRef;
            if (idxRef != null)
            {
                IndexList.Remove(idxRef);
            }
            else
            {
                idxRef = new LinkedListNode<KeyValuePair<TKey, CacheValue<TKey, TValue>>>(new KeyValuePair<TKey, CacheValue<TKey, TValue>>(key, cacheValue));
                cacheValue.IndexRef = idxRef;
            }
            IndexList.AddFirst(idxRef);
        }

        protected virtual CacheValue<TKey, TValue> GetCacheValueUnlocked(TKey key)
        {
            CacheValue<TKey, TValue> v;
            return _valueCache.TryGetValue(key, out v) ? v : null;
        }

        public virtual void SetValue(TKey key, TValue value)
        {
            lock (SyncRoot)
            {
                SetValueUnlocked(key, value);
            }
        }

        protected virtual CacheValue<TKey, TValue> SetValueUnlocked(TKey key, TValue value)
        {
            _lastCacheAccess = DateTime.Now;
            CacheValue<TKey, TValue> cacheValue = GetCacheValueUnlocked(key);
            if (cacheValue == null)
            {
                cacheValue = new CacheValue<TKey, TValue>(value);
                _valueCache[key] = cacheValue;
            }
            else
            {
                cacheValue.Value = value;
            }
            UpdateElementAccess(key, cacheValue);
            return cacheValue;
        }

        public virtual void Invalidate(TKey key)
        {
            lock (SyncRoot)
            {
                _lastCacheAccess = DateTime.Now;
                InvalidateUnlocked(key);
            }
        }

        protected void InvalidateUnlocked(TKey key)
        {
            var value = GetCacheValueUnlocked(key);
            if (value != null)
            {
                _valueCache.Remove(key);
                IndexList.Remove(value.IndexRef);
            }
        }

        public virtual void Expire(TimeSpan maxAge)
        {
            lock (SyncRoot)
            {
                var toExpire = _valueCache.Where(x => IsExpired(x.Key, x.Value.Value, x.Value.LastAccess, maxAge)).Select(x => x.Key).ToList();
                toExpire.ForEach(InvalidateUnlocked);
            }
        }

        public virtual void Expire(int maxSize)
        {
            lock (SyncRoot)
            {
                while (IndexList.Count > maxSize)
                {
                    InvalidateUnlocked(IndexList.Last.Value.Key);
                }
            }
        }

        public virtual void Flush()
        {
            lock (SyncRoot)
            {
                _valueCache.Clear();
                IndexList.Clear();
            }
        }

        protected virtual bool IsExpired(TKey key, TValue value, DateTime lastValueAccess, TimeSpan maxAge)
        {
            return lastValueAccess + maxAge < _lastCacheAccess;
        }

        public List<TKey> GetKeys()
        {
            lock (SyncRoot)
            {
                return new List<TKey>(_valueCache.Keys);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
