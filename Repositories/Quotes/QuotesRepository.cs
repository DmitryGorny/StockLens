using EFCore.BulkExtensions;
using StockLens.data;
using QuotesModel = StockLens.Models.Quotes;

namespace StockLens.Repositories.Quotes
{
    public class QuotesRepository : IQuotesRepository
    {
        private readonly AppDBContext _db_context;

        public QuotesRepository(AppDBContext context)
        {
            _db_context = context;
        }

        public async Task CreateQuotesBulkAsync(List<QuotesModel> quotes)
        {
            foreach (var quotesChunk in quotes.Chunk(500))
            {
                await _db_context.BulkInsertOrUpdateAsync(quotesChunk, new BulkConfig
                {
                    SetOutputIdentity = true,
                    PreserveInsertOrder = true,
                    UpdateByProperties = new()
                    {
                        nameof(QuotesModel.ts),
                        nameof(QuotesModel.TickerId),
                    },
                    PropertiesToExclude = new List<string> { "Id" }
                });
            }
        }
    }
}
