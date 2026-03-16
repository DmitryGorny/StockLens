using StockLens.Dtos.AuthDtos;
using StockLens.Dtos.QuotesDtos.Analytics;
using StockLens.Dtos.QuotesDtos.Analytics.Fabric;
using StockLens.Mappers;
using StockLens.Queries;
using StockLens.Repositories.Quotes;
using StockLens.Repositories.Tickers;
using StockLens.Services.HttpRequester;

namespace StockLens.Services.Analytics.Portfolio
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IQuotesRepository _quotesRepository;
        private readonly ITickersRepository _tickersRepository;
        private readonly IHttpRequester _analyticsRequester;

        public PortfolioService(IQuotesRepository quotesRepository,
                            ITickersRepository tickers,
                            IHttpRequester analyticsRequester)
        {
            _quotesRepository = quotesRepository;
            _tickersRepository = tickers;
            _analyticsRequester = analyticsRequester;
        }

        public async Task<string> GetPorfolioMetrics(Dictionary<int, decimal> tickersAndPercantages, UsersСharacteristicsDto CharDto)
        {
            if (tickersAndPercantages.Count() == 0)
                throw new Exception("Неоюходимо выбрать компании для анализа");

            AnalyticsBuilder<PortfolioDto> fabric = new AnalyticsBuilder<PortfolioDto>();
            foreach (var (key, value) in tickersAndPercantages)
            {
                var quotes = await _quotesRepository.GetQuotesByTickerId(key, 360);
                if (quotes == null || quotes.Count() == 0)
                    throw new Exception($"У тикера {key} не найдено котировок");

                quotes.ForEach(q => {
                    var dto = q.ToPortfolioFromQuotaion(value); 
                    fabric.AddAnalyticsDto(dto);
                });
            }

            string json = await _analyticsRequester.PostJsonAsync("/portfolio/own-weights", fabric.WrapAnalyticsDtos(CharDto));
            return json;
        }

        public async Task<string> GetOptimizedPortfolio(List<int> tickersIds, UsersСharacteristicsDto CharDto)
        {
            AnalyticsBuilder<OptimizePortfolioDto> fabric = new AnalyticsBuilder<OptimizePortfolioDto>();
            if (tickersIds.Count() == 0)
                throw new Exception("Неоюходимо выбрать компании для оптимизации");

            foreach (var id in tickersIds)
            {
                var quotes = await _quotesRepository.GetQuotesByTickerId(id, 360);
                if (quotes == null || quotes.Count() == 0)
                    throw new Exception($"У тикера {id} не найдено котировок");

               quotes.ForEach(q => {
                   var dto = q.ToOptimizePortfolioFromQuotaion(); 
                   fabric.AddAnalyticsDto(dto);
               });
            }

            string json = await _analyticsRequester.PostJsonAsync("/portfolio/optimize", fabric.WrapAnalyticsDtos(CharDto));
            return json;
        }
    }
}
