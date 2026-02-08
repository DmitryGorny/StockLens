using StockLens.Dtos.CitiesDtos;
using StockLens.Dtos.QuotationsDtos;

namespace StockLens.Services.Moex
{
    public interface IMoexService
    {
        public Task<List<CreateQuotesDto>> RequestQuotes(string TickerSymbol, int TickerId);
    }
}
