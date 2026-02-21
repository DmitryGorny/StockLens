using Microsoft.AspNetCore.Mvc.ApplicationModels;
using StockLens.Dtos.QuotesDtos;
using StockLens.Mappers;
using StockLens.Models;
using StockLens.Queries;
using StockLens.Repositories.Industries;
using StockLens.Repositories.Sector;
using StockLens.Repositories.Tickers;
using StockLens.Services.HttpRequester;
using IndustriesModel = StockLens.Models.Industries;

namespace StockLens.Services.Analytics.GeneralAnalytics
{
    public class GeneralAnalyticsFacade : IGeneralAnalyticsFacade
    {
        private readonly ISectorRepository _sectorsRepository;
        private readonly IIndustriesRepository _industriesRepository;
        private readonly ITickersRepository _tickersRepository;
        private readonly IHttpRequester _analyticsRequester;

        public GeneralAnalyticsFacade(ISectorRepository sectorsRepository, 
                                      IIndustriesRepository industriesRepository, 
                                      ITickersRepository tickersRepository,
                                      IHttpRequester httpRequester)
        {
            _sectorsRepository = sectorsRepository;
            _industriesRepository = industriesRepository;
            _tickersRepository = tickersRepository;
            _analyticsRequester = httpRequester;
        }

        public async Task<string> GetSectorsGeneralAnalytics(int sectorId)
        {
            List<GeneralAnalyticsDto> analyticsDtos = new List<GeneralAnalyticsDto>();
            Sectors? sector = await _sectorsRepository.GetSectorAsync(sectorId, 180);

            if (sector == null)
                throw new NullReferenceException($"Сектора с id {sectorId}");

            foreach (var industry in sector.Industries)
            {
                foreach (var ticker in industry.Tickers)
                {
                    var quotes = ticker.Quotation.Select(q => q.ToGeneralAnalyticFromQuotaion()).ToList();
                    analyticsDtos.AddRange(quotes);
                }
            }
            string result = await _analyticsRequester.PostJsonAsync<GeneralAnalyticsDto>("/general_analytics", analyticsDtos);
            return result;
        }

        public async Task<string> GetIndustriesGeneralAnalytics(int IndustryId)
        {
            IndustriesModel? ind = await _industriesRepository.GetIndustriesWithDependencies(IndustryId, 180);

            if (ind == null)
                throw new Exception($"Индустрии с id {IndustryId} не существует");

            List<GeneralAnalyticsDto> dtos = ind.Tickers
                                                    .SelectMany(t => t.Quotation)
                                                    .Select(q => q.ToGeneralAnalyticFromQuotaion()).ToList();

            string result = await _analyticsRequester.PostJsonAsync<GeneralAnalyticsDto>("/general_analytics", dtos);
            return result;
        }
        public async Task<string> GetTickersGeneralAnalytics(int TickerId)
        {
            var ticker = await _tickersRepository.GetTickerWithDependencies(TickerId, 180);
            if (ticker == null)
                throw new Exception($"Тикера с id {TickerId} не существует");
            List<GeneralAnalyticsDto> dtos = ticker.Quotation.Select(q => q.ToGeneralAnalyticFromQuotaion()).ToList();

            string json = await _analyticsRequester.PostJsonAsync<GeneralAnalyticsDto>("/general_analytics", dtos);
            return json;
        }
    }
}
