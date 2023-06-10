using System;
using Wallet.Application.Contracts.Persistence;

namespace Wallet.Application.Utilities
{
    public static class CacheUtils
    {
        public static void SetCacheData<T>(this ICacheService cacheService, string key, T data)
        {
            var expiryTime = DateTimeOffset.Now.AddSeconds(50);
            cacheService.SetData(key,data,expiryTime);
        }
    }
}
