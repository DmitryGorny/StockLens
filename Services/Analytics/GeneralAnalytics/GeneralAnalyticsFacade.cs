using Microsoft.AspNetCore.Mvc.ApplicationModels;
using StockLens.Dtos.AuthDtos;
using StockLens.Dtos.QuotesDtos.Analytics;
using StockLens.Dtos.QuotesDtos.Analytics.Fabric;
using StockLens.Mappers;
using StockLens.Models;
using StockLens.Repositories.Cities;
using StockLens.Repositories.Industries;
using StockLens.Repositories.Quotes;
using StockLens.Repositories.Sector;
using StockLens.Repositories.Tickers;
using StockLens.Services.HttpRequester;
using System.Collections.Generic;
using IndustriesModel = StockLens.Models.Industries;

namespace StockLens.Services.Analytics.GeneralAnalytics
{
    public class GeneralAnalyticsFacade : IGeneralAnalyticsFacade
    {
        private readonly ISectorRepository _sectorsRepository;
        private readonly IIndustriesRepository _industriesRepository;
        private readonly ITickersRepository _tickersRepository;
        private readonly IQuotesRepository _quotesRepository;
        private readonly IHttpRequester _analyticsRequester;

        public GeneralAnalyticsFacade(ISectorRepository sectorsRepository, 
                                      IIndustriesRepository industriesRepository, 
                                      ITickersRepository tickersRepository,
                                      IQuotesRepository quotesRepository,
                                      IHttpRequester httpRequester)
        {
            _sectorsRepository = sectorsRepository;
            _industriesRepository = industriesRepository;
            _tickersRepository = tickersRepository;
            _analyticsRequester = httpRequester;
            _quotesRepository = quotesRepository;
        }

        public async Task<string> GetSectorsGeneralAnalytics(int sectorId, int daysNumber, UsersСharacteristicsDto dto)
        {
  
            Sectors? sector = await _sectorsRepository.GetSectorAsync(sectorId, 180);

            AnalyticsBuilder<GeneralAnalyticsDto> fabric = new AnalyticsBuilder<GeneralAnalyticsDto>();

            if (sector == null)
                throw new NullReferenceException($"Сектора с id {sectorId}");

            foreach (var industry in sector.Industries)
            {
                foreach (var ticker in industry.Tickers)
                {
                    var qs = await _quotesRepository.GetQuotesByTickerId(ticker.Id, daysNumber);
                    qs.ForEach(q =>
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

        public async Task<string> GetIndustriesGeneralAnalytics(int IndustryId, int daysNumber, UsersСharacteristicsDto dto)
        {
            IndustriesModel? ind = await _industriesRepository.GetIndustriesWithDependencies(IndustryId);
            AnalyticsBuilder<GeneralAnalyticsDto> fabric = new AnalyticsBuilder<GeneralAnalyticsDto>();

            if (ind == null)
                throw new Exception($"Индустрии с id {IndustryId} не существует");

            foreach(var t in ind.Tickers)
            {
                var qs = await _quotesRepository.GetQuotesByTickerId(t.Id, daysNumber);
                qs.ForEach(q =>
                {
                    var Qdto = q.ToGeneralAnalyticFromQuotaion();
                    fabric.AddAnalyticsDto(Qdto);
                });
            };
                      

            string result = await _analyticsRequester.PostJsonAsync("/general_analytics", fabric.WrapAnalyticsDtos(dto));
            return result;
        }
        public async Task<string> GetTickersGeneralAnalytics(int TickerId, int daysNumber, UsersСharacteristicsDto dto)
        {
            AnalyticsBuilder<GeneralAnalyticsDto> fabric = new AnalyticsBuilder<GeneralAnalyticsDto>();

            var ticker = await _tickersRepository.GetTicker(TickerId);
            if (ticker == null)
                throw new Exception($"Тикера с id {TickerId} не существует");
            var qs = await _quotesRepository.GetQuotesByTickerId(ticker.Id, daysNumber);

            qs.ForEach(q => {
                var dto = q.ToGeneralAnalyticFromQuotaion();
                fabric.AddAnalyticsDto(dto);
            });

            string json = await _analyticsRequester.PostJsonAsync("/general_analytics", fabric.WrapAnalyticsDtos(dto));
            return json;
        }

        public async Task<string> GetCityGeneralAnalytics(int CityId, int daysNumber, UsersСharacteristicsDto dto)
        {
            AnalyticsBuilder<GeneralAnalyticsDto> fabric = new AnalyticsBuilder<GeneralAnalyticsDto>();
            var tickers = await _tickersRepository.GetTickersByCitiesAsync(new List<int> { CityId });
            if ( tickers == null || tickers.Count() == 0)
            {
                throw new Exception("Тикеры не были найдены");
            }

            List<Quotes> quotes = new List<Quotes>();
            foreach (var t in tickers)
            {
                quotes.AddRange(await _quotesRepository.GetQuotesByTickerId(t.Id, daysNumber));
            }

            quotes.ForEach(q => fabric.AddAnalyticsDto(q.ToGeneralAnalyticFromQuotaion()));

            string json = await _analyticsRequester.PostJsonAsync("/general_analytics", fabric.WrapAnalyticsDtos(dto));
            return json;
        }
    }
}
