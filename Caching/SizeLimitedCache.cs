namespace eLib.Caching
{
    public class SizeLimitedCache<TKey, TValue> : Cache<TKey, TValue>
    {
        public int MaxSize { get; set; }

        public SizeLimitedCache(int maxSize)
        {
            MaxSize = maxSize;
        }

        protected override void UpdateElementAccess(TKey key, CacheValue<TKey, TValue> cacheValue)
        {
            base.UpdateElementAccess(key, cacheValue);
            while (IndexList.Count > MaxSize)
            {
                InvalidateUnlocked(IndexList.Last.Value.Key);
            }
        }

        public virtual void Expire()
        {
            base.Expire(MaxSize);
        }
    }
}
