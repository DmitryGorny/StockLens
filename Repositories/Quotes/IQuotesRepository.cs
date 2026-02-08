using QuotesModel = StockLens.Models.Quotes;

namespace StockLens.Repositories.Quotes
{
    public interface IQuotesRepository
    {
        public Task CreateQuotesBulkAsync(List<QuotesModel> quotes);
    }
}
