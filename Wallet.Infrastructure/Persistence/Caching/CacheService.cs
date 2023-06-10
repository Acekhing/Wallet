using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using Wallet.Application.Contracts.Persistence;

namespace Wallet.Infrastructure.Persistence.Caching
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _cacheDb;

        public CacheService(IDatabase db) => _cacheDb = db;

        public T GetData<T>(string key)
        {
            var data = _cacheDb.StringGet(key);
            if (string.IsNullOrEmpty(data))
                return default;

            return JsonConvert.DeserializeObject<T>(data);
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            // calculates the time difference between a given expiration time and current time
            var expirytime = expirationTime.DateTime.Subtract(DateTime.Now);
            return _cacheDb.StringSet(key, JsonConvert.SerializeObject(value), expirytime);
        }
    }
}
