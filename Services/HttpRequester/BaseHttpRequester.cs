using System.Diagnostics;
using System.Text.Json;

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

        public async Task<string> PostJsonAsync<T>(string url, List<T> jsonData)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            Console.WriteLine(
                JsonSerializer.Serialize(jsonData, options));
            var response = await _httpClient.PostAsJsonAsync(url, jsonData, options);

            if (!response.IsSuccessStatusCode)
            {
                string errorBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(errorBody);
                throw new Exception(errorBody);
            }
                

            return await response.Content.ReadAsStringAsync();
        }
    }
}
