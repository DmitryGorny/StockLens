namespace StockLens.Services.Cache
{
    public interface ICacheService
    {
        public Task<T?> GetCache<T>(string serviceName, params string[] args);
        public Task<string?> GetUnserializedCache(string serviceName, params string[] args);
        public Task SetCache<T>(T value, string serviceName, params string[] args);
        public Task DeleteCache(string serviceName, params string[] args);
        public Task SetCacheWithoutSerializing(string value, string serviceName, params string[] args);
    }
}
