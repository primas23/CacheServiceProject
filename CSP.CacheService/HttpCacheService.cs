using System;
using System.Web;
using System.Web.Caching;

using CSP.Common.Contracts;

namespace CSP.CacheService
{
    public class HttpCacheService : ICacheService
    {
        private static readonly object LockObject = new object();

        /// <summary>
        /// Gets the value of the specified item name if exists or inserts it in the cache.
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="itemName">Name of the item.</param>
        /// <param name="getDataFunc">The get data function.</param>
        /// <param name="durationInSeconds">The duration in seconds. The defauld value is 10 minutes</param>
        /// <returns>
        /// The cached item value
        /// </returns>
        public T Get<T>(string itemName, Func<T> getDataFunc, int durationInSeconds = 60 * 10)
        {
            if (HttpRuntime.Cache[itemName] == null)
            {
                lock (LockObject)
                {
                    if (HttpRuntime.Cache[itemName] == null)
                    {
                        T data = getDataFunc();

                        if (data != null)
                        {
                            HttpRuntime.Cache.Insert(
                            itemName,
                            data,
                            null,
                            DateTime.UtcNow.AddSeconds(durationInSeconds),
                            Cache.NoSlidingExpiration);
                        }
                    }
                }
            }

            return (T)HttpRuntime.Cache[itemName];
        }

        /// <summary>
        /// Removes the specified item name.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        public void Remove(string itemName)
        {
            if (string.IsNullOrWhiteSpace(itemName))
            {
                return;
            }

            HttpRuntime.Cache.Remove(itemName);
        }
    }
}
