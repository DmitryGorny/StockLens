using StockLens.Dtos.QuotationsDtos;
using System.Diagnostics.Contracts;

namespace StockLens.Services.QuotesService
{
    public interface IQuotesService
    {
        public Task CreateQuotesBulk(List<CreateQuotesDto> quotes);
    }
}
