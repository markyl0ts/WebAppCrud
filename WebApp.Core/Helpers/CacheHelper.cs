using Microsoft.Extensions.Caching.Memory;

namespace WebApp.Core.Helpers
{
    public static class CacheHelper
    {
        private static readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        public static (bool IsExist, T Data) GetFromCache<T>(string key)
        {
            T data;
            bool isExist = _cache.TryGetValue(key, out data);

            return (isExist, data);
        }

        public static void SetToCache(string key, object objectValue)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1));

            _cache.Set(key, objectValue, cacheEntryOptions);
        }

        public static void RemoveFromCache(string key) => _cache.Remove(key);
    }
}
