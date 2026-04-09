using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StockLens.Services.Search.SymbolsTree;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StockLens.Services.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache) { _cache = cache; }
        public async Task<IEnumerable<T>?> GetCacheEnumarable<T>(string serviceName, params string[] args)
        {
            string key = BuildKey(serviceName, args);
            var result = await _cache.GetStringAsync(key);
            
            if (result == null)
                return null;

            var deserializedValue = JsonConvert.DeserializeObject<IEnumerable<T>>(result, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });

            return deserializedValue;
        }

        public async Task<T?> GetCache<T>(string serviceName, params string[] args)
        {
            string key = BuildKey(serviceName, args);
            var result = await _cache.GetStringAsync(key);

            if (result == null)
                return default(T);

            var deserializedValue = JsonConvert.DeserializeObject<T>(result, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
            return deserializedValue;
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

            var serializedValue = JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });

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
