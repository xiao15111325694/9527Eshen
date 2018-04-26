using System;
using System.Web;

namespace Infrastructure.Cache
{
    public class CacheContext:ICacheContext
    {
        private readonly System.Web.Caching.Cache _Cache = HttpRuntime.Cache;

        public override T GetCache<T>(string cacheKey)
        {
            var objCache = _Cache.Get(cacheKey);
            return (T)objCache;
        }

        public override bool SetCache<T>(string key, T t)
        {
            var obj = GetCache<T>(key);
            if (obj != null)
            {
                RemoveCache(key);
            }
            _Cache.Insert(key, t);
            return true;
        }


        public override bool SetCache<T>(string key, T t, DateTime time)
        {
            var obj = GetCache<T>(key);
            if (obj != null)
            {
                RemoveCache(key);
            }
            _Cache.Insert(key, t, null, time, System.Web.Caching.Cache.NoSlidingExpiration);
            return true;
        }

        public override bool RemoveCache(string key)
        {
            _Cache.Remove(key);
            return true;
        }

    }
}
