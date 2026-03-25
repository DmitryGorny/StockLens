using StockLens.data;
using StockLens.Dtos.QuotationsDtos;
using StockLens.Services.Moex;
using StockLens.Services.QuotesService;
using StockLens.Services.Tickers;

namespace StockLens.Services.Cron
{
    public class CroneFacade : ICronFacade
    {
        private readonly IQuotesService _quotesService;
        private readonly IMoexService _moexService;
        private readonly ITickersService tickersService;
        private readonly AppDBContext _appDBContext;

        public CroneFacade(IQuotesService quotesService, 
            IMoexService moexService, 
            ITickersService tickersService,
            AppDBContext appDBContext)
        {
            _quotesService = quotesService;
            _moexService = moexService;
            this.tickersService = tickersService;
            _appDBContext = appDBContext;
        }

        public async Task RequestQuotesDaily()
        {
            var Tickers = await tickersService.GetTickersAsync();
           
            using (var transaction = await _appDBContext.Database.BeginTransactionAsync())
            {
                try
                {
                    foreach (var ticker in Tickers)
                    {
                        var quotes = await _moexService.RequestQuotesByDays(ticker.Symbol, ticker.Id, 1);
                        if (quotes != null && quotes.Count() != 0)
                        {
                            await _quotesService.CreateQuotesBulk(quotes.ToList());
                        }
                    }

                    await transaction.CommitAsync();
                }
                catch (Exception ex) 
                {
                    await transaction.RollbackAsync();
                }
               
            }

        }
    }
}
