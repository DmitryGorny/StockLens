namespace StockLens.Services.HttpRequester
{
    public interface IHttpRequester
    {
        public Task<T?> GetJsonAsync<T>(string url);

        public Task<string> PostJsonAsync(string url, string jsonData);
    }
}
