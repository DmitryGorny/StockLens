using Microsoft.AspNetCore.Mvc.ApplicationModels;
using StockLens.Dtos.AuthDtos;
using StockLens.Dtos.QuotesDtos.Analytics;
using StockLens.Dtos.QuotesDtos.Analytics.Fabric;
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

        public async Task<string> GetSectorsGeneralAnalytics(int sectorId, UsersСharacteristicsDto dto)
        {
  
            Sectors? sector = await _sectorsRepository.GetSectorAsync(sectorId, 180);

            AnalyticsFabric<GeneralAnalyticsDto> fabric = new AnalyticsFabric<GeneralAnalyticsDto>();

            if (sector == null)
                throw new NullReferenceException($"Сектора с id {sectorId}");

            foreach (var industry in sector.Industries)
            {
                foreach (var ticker in industry.Tickers)
                {
                    ticker.Quotation.ForEach(q =>
                    {
                        var dto = q.ToGeneralAnalyticFromQuotaion();
                        fabric.AddAnalyticsDto(dto);
                    });
                }
            }

            string result = await _analyticsRequester.PostJsonAsync("/general_analytics", 
                fabric.WrapAnalyticsDtos(dto));
            return result;
        }

        public async Task<string> GetIndustriesGeneralAnalytics(int IndustryId, UsersСharacteristicsDto dto)
        {
            IndustriesModel? ind = await _industriesRepository.GetIndustriesWithDependencies(IndustryId, 180);
            AnalyticsFabric<GeneralAnalyticsDto> fabric = new AnalyticsFabric<GeneralAnalyticsDto>();

            if (ind == null)
                throw new Exception($"Индустрии с id {IndustryId} не существует");

            var quotes = ind.Tickers.SelectMany(t => t.Quotation);

            foreach(var quote in quotes)
            {
                var Qdto = quote.ToGeneralAnalyticFromQuotaion();
                fabric.AddAnalyticsDto(Qdto);
            };
                      

            string result = await _analyticsRequester.PostJsonAsync("/general_analytics", fabric.WrapAnalyticsDtos(dto));
            return result;
        }
        public async Task<string> GetTickersGeneralAnalytics(int TickerId, UsersСharacteristicsDto dto)
        {
            AnalyticsFabric<GeneralAnalyticsDto> fabric = new AnalyticsFabric<GeneralAnalyticsDto>();

            var ticker = await _tickersRepository.GetTickerWithDependencies(TickerId, 180);
            if (ticker == null)
                throw new Exception($"Тикера с id {TickerId} не существует");
            ticker.Quotation.ForEach(q => {
                var dto = q.ToGeneralAnalyticFromQuotaion();
                fabric.AddAnalyticsDto(dto);
            });

            string json = await _analyticsRequester.PostJsonAsync("/general_analytics", fabric.WrapAnalyticsDtos(dto));
            return json;
        }
    }
}
