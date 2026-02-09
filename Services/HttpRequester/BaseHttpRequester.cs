namespace StockLens.Services.HttpRequester
{
    public class BaseHttpRequester : IHttpRequester
    {
        protected readonly HttpClient _httpClient;

        public BaseHttpRequester(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T?> GetJsonAsync<T>(string url)
        {
            T? deserialized = await _httpClient.GetFromJsonAsync<T>(url);
            if (deserialized == null)
                throw new NullReferenceException("Данные в JSON не подходят под переданный тип");

            return deserialized;
        }

        public async Task<string> PostJsonAsync(string url, string jsonData)
        {
            var response = await _httpClient.PostAsJsonAsync(url, jsonData);

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.StatusCode.ToString());

            return await response.Content.ReadAsStringAsync();
        }
    }
}
