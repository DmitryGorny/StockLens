namespace StockLens.Services.HttpRequester
{
    public interface IHttpRequester
    {
        public Task<T?> GetJsonAsync<T>(string url);

        public Task<string> PostJsonAsync<T>(string url, List<T> jsonData);
    }
}
