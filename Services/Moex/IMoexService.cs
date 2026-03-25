using StockLens.Dtos.CitiesDtos;
using StockLens.Dtos.QuotationsDtos;

namespace StockLens.Services.Moex
{
    public interface IMoexService
    {
        public Task<List<CreateQuotesDto>> RequestQuotesByYears(string TickerSymbol, int TickerId, int yearsDelta);

        public Task<int?> RequesTickersListLevel(string TickerSymbol);

        public Task<IEnumerable<CreateQuotesDto>> RequestQuotesByDays(string TickerSymbol, int TickerId, int daysDelta);

    }
}
