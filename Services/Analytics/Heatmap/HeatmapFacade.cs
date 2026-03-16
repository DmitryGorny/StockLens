using StockLens.Dtos.AuthDtos;
using StockLens.Dtos.QuotesDtos.Analytics;
using StockLens.Dtos.QuotesDtos.Analytics.Fabric;
using StockLens.Mappers;
using StockLens.Queries;
using StockLens.Repositories.Industries;
using StockLens.Repositories.Quotes;
using StockLens.Repositories.Sector;
using StockLens.Repositories.Tickers;
using StockLens.Services.HttpRequester;

namespace StockLens.Services.Analytics.Heatmap
{
    public class HeatmapFacade : IHeatmapFacade
    {
        private readonly IQuotesRepository _quotesRepository;
        private readonly ITickersRepository _tickersRepository;
        private readonly IHttpRequester _analyticsRequester;

        public HeatmapFacade(IQuotesRepository quotesRepository, ITickersRepository tickersRepository, IHttpRequester analyticsRequester)
        {
            _quotesRepository = quotesRepository;
            _tickersRepository = tickersRepository;
            _analyticsRequester = analyticsRequester;
        }

        public async Task<string> GetTickersHeatmap(UsersСharacteristicsDto dto)
        {
            AnalyticsFabric<HeatmapDto> fabric = new AnalyticsFabric<HeatmapDto>();
            var tickers = await _tickersRepository.GetTickersByListLevel(1);
            if (tickers == null || tickers.Count() == 0)
                throw new Exception($"Тикеры с ListLevel 1 не были найдены");

            foreach (var ticker in tickers)
            {
                ticker.Quotation = await _quotesRepository.GetQuotesByTickerId(ticker.Id, 180);
            }

            var quotes = tickers.SelectMany(t => t.Quotation);
            foreach (var quote in quotes)
            {
                var Qdto = quote.ToHeatmapFromQuotaion();
                fabric.AddAnalyticsDto(Qdto);
            };
                                                             
                                                    

            string json = await _analyticsRequester.PostJsonAsync("/sector-correlations", fabric.WrapAnalyticsDtos(dto));

            return json;
        }
    }
}
