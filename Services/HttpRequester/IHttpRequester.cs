using StockLens.Dtos.AuthDtos;
using StockLens.Dtos.QuotesDtos.Analytics;

namespace StockLens.Services.HttpRequester
{
    public interface IHttpRequester
    {
        public Task<T?> GetJsonAsync<T>(string url);

        public Task<string> PostJsonAsync<T>(string url, AnalyticsContainerDto<T> AnalyticsDto) 
            where T : IAnalyticsDto;
    }
}
