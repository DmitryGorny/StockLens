using QuotesModel = StockLens.Models.Quotes;

namespace StockLens.Repositories.Quotes
{
    public interface IQuotesRepository
    {
        public Task CreateQuotesBulkAsync(List<QuotesModel> quotes);
        public Task<List<QuotesModel>> GetQuotesByTickerId(int tickerId, int limit);
        public Task<List<QuotesModel>> GetQuotesByTickerId(int tickerId, DateTime startDate, DateTime endDate);
        public Task DeleteQuotesBulkHard(IEnumerable<QuotesModel> quotes);
    }
}
