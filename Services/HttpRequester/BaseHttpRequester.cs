using StockLens.Dtos.AuthDtos;
using StockLens.Dtos.QuotesDtos.Analytics;
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

        public async Task<string> PostJsonAsync<T>(string url, AnalyticsContainerDto<T> AnalyticsDto) 
            where T :IAnalyticsDto
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            Console.WriteLine(
                JsonSerializer.Serialize(AnalyticsDto, options));
            var response = await _httpClient.PostAsJsonAsync(url, AnalyticsDto, options);

            if (!response.IsSuccessStatusCode)
            {
                string errorBody = await response.Content.ReadAsStringAsync();
                throw new Exception(errorBody);
            }
                

            return await response.Content.ReadAsStringAsync();
        }
    }
}
