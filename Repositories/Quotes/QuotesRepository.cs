using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<QuotesModel>> GetQuotesByTickerId(int tickerId, int limit)
        {
            return  await _db_context.Quotes.Where(q => q.TickerId == tickerId)
                                            .Include(q => q.Ticker)
                                            .ThenInclude(t => t.Industry)
                                            .ThenInclude(i => i.Sector)
                                            .OrderByDescending(q => q.ts)
                                            .Take(limit)
                                            .ToListAsync();
        }
    }
}
