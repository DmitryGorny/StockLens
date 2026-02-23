using StockLens.Dtos.QuotesDtos;
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

        public async Task<string> GetPorfolioMetrics(Dictionary<int, decimal> tickersAndPercantages)
        {
            if (tickersAndPercantages.Count() == 0)
                throw new Exception("Неоюходимо выбрать компании для анализа");

            List<PortfolioDto> dtos = new List<PortfolioDto>();
            foreach (var (key, value) in tickersAndPercantages)
            {
                var quotes = await _quotesRepository.GetQuotesByTickerId(key, 360);
                if (quotes == null || quotes.Count() == 0)
                    throw new Exception($"У тикера {key} не найдено котировок");

                dtos.AddRange(quotes.Select(q => q.ToPortfolioFromQuotaion(value)).ToList());
            }

            string json = await _analyticsRequester.PostJsonAsync("/portfolio/own-weights", dtos);
            return json;
        }

        public async Task<string> GetOptimizedPortfolio(List<int> tickersIds)
        {
            if (tickersIds.Count() == 0)
                throw new Exception("Неоюходимо выбрать компании для оптимизации");

            List<OptimizePortfolioDto> dtos = new List<OptimizePortfolioDto>();
            foreach (var id in tickersIds)
            {
                var quotes = await _quotesRepository.GetQuotesByTickerId(id, 360);
                if (quotes == null || quotes.Count() == 0)
                    throw new Exception($"У тикера {id} не найдено котировок");

                dtos.AddRange(quotes.Select(q => q.ToOptimizePortfolioFromQuotaion()).ToList());
            }

            string json = await _analyticsRequester.PostJsonAsync("/portfolio/optimize", dtos);
            return json;
        }
    }
}
