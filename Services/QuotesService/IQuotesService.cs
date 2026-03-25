using StockLens.Dtos.QuotationsDtos;
using System.Diagnostics.Contracts;

namespace StockLens.Services.QuotesService
{
    public interface IQuotesService
    {
        public Task CreateQuotesBulk(List<CreateQuotesDto> quotes);
        public Task DeleteQuotesHard(int TickerId, DateTime startDate, DateTime endDate);
    }
}
