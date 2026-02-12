using StockLens.Dtos.QuotesDtos;
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

        public async Task<string> GetTickersHeatmap()
        {
            var tickers = await _tickersRepository.GetTickersByListLevel(1);
            if (tickers == null || tickers.Count() == 0)
                throw new Exception($"Тикеры с ListLevel 1 не были найдены");

            foreach (var ticker in tickers)
            {
                ticker.Quotation = await _quotesRepository.GetQuotesByTickerId(ticker.Id, 180);
            }

            List<HeatmapDto> dtos = tickers.SelectMany(t => t.Quotation
                                                            .Select(q => q.ToHeatmapFromQuotaion()))
                                                             .ToList();
                                                    

            string json = await _analyticsRequester.PostJsonAsync("/sector-correlations", dtos);

            return json;
        }
    }
}
