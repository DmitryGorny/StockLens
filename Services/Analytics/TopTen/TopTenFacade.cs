using StockLens.Dtos.QuotesDtos;
using StockLens.Mappers;
using StockLens.Models;
using StockLens.Queries;
using StockLens.Repositories.Quotes;
using StockLens.Repositories.Tickers;
using StockLens.Services.HttpRequester;

namespace StockLens.Services.Analytics.TopTen
{
    public class TopTenFacade : ITopTenFacade
    {
        private readonly IQuotesRepository _quotesRepository;
        private readonly ITickersRepository _tickersRepository;
        private readonly IHttpRequester _analyticsRequester;

        public TopTenFacade(IQuotesRepository quotesRepository, 
                            ITickersRepository tickers,
                            IHttpRequester analyticsRequester)
        {
            _quotesRepository = quotesRepository;
            _tickersRepository = tickers;
            _analyticsRequester = analyticsRequester;
        }

        public async Task<string> GetTickersTopTen()
        {
            var ticker = await _tickersRepository.GetTicker("MOEX");
            if (ticker == null)
                throw new Exception("MOEX тикер не был найден");

            var moex_quotes = await _quotesRepository.GetQuotesByTickerId(ticker.Id, 360);
            if (moex_quotes == null || moex_quotes.Count() == 0)
                throw new Exception("MOEX не содержит котировок");

            List<TopTenDto> dtos = moex_quotes.Select(q => q.ToTopTenFromQuotaion()).ToList();

            var LevelOneTickers = await _tickersRepository.GetTickersByListLevel(1);

            if (LevelOneTickers == null || LevelOneTickers.Count() == 0)
                throw new Exception("Тикеры уровня 1 были не найдены");

            foreach (var t in LevelOneTickers)
            {
                var quotes = await _quotesRepository.GetQuotesByTickerId(t.Id, 360);
                dtos.AddRange(quotes.Select(q => q.ToTopTenFromQuotaion()).ToList());
            }

            string json = await _analyticsRequester.PostJsonAsync("/anti-crisis-top10", dtos);
            return json;
        }
    }
}
