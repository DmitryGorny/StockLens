using StockLens.Dtos.QuotationsDtos;
using StockLens.Mappers;
using StockLens.Repositories.Quotes;

namespace StockLens.Services.QuotesService
{
    public class QuotesService : IQuotesService
    {
        private readonly IQuotesRepository _quotesRepository;

        public QuotesService(IQuotesRepository quotesRepository) { _quotesRepository = quotesRepository; }

        public async Task CreateQuotesBulk(List<CreateQuotesDto> quotes)
        {
            await _quotesRepository.CreateQuotesBulkAsync(quotes.Select(q => q.ToQuotesFromDto()).ToList());
        }

        public async Task DeleteQuotesHard(int TickerId, DateTime startDate, DateTime endDate)
        {
            var quotes = await _quotesRepository.GetQuotesByTickerId(TickerId, startDate, endDate);
            if (quotes == null || quotes.Count() == 0) throw new Exception("Тикер неккоректен");
            await _quotesRepository.DeleteQuotesBulkHard(quotes);
        }
    }
}
