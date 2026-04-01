using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StockLens.Services.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache) { _cache = cache; }
        public async Task<IEnumerable<T>?> GetCache<T>(string serviceName, params string[] args)
        {
            string key = BuildKey(serviceName, args);
            var result = await _cache.GetStringAsync(key);

            if (result == null)
                return null;

            return JsonSerializer.Deserialize<IEnumerable<T>>(result);
        }

        public async Task<string?> GetUnserializedCache(string serviceName, params string[] args)
        {
            string key = BuildKey(serviceName, args);
            var result = await _cache.GetStringAsync(key);

            return result;
        }
        public async Task SetCache<T>(T value, string serviceName, params string[] args)
        {
            string key = BuildKey(serviceName, args);
            string serializedValue = JsonSerializer.Serialize(value);

            await _cache.SetStringAsync(
                key,
                serializedValue,
                new DistributedCacheEntryOptions
                { 
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
        }

        public async Task SetCacheWithoutSerializing(string value, string serviceName, params string[] args)
        {
            string key = BuildKey(serviceName, args);

            await _cache.SetStringAsync(
                key,
                value,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
        }

        public async Task DeleteCache(string serviceName, params string[] args)
        {
            string key = BuildKey(serviceName, args);
            await _cache.RemoveAsync(key);
        }

        private string BuildKey(string serviceName, params string[] args)
        {
            string key = serviceName;
            foreach(var arg in args)
            {
                key += $";{arg}";
            }

            return key;
        }
    }
}
