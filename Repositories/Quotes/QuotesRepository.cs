using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
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
            return await _db_context.Quotes.Where(q => q.TickerId == tickerId && q.ts >= DateTime.UtcNow.AddDays(-limit))
                                            .Include(q => q.Ticker)
                                            .OrderByDescending(q => q.ts)
                                            .ToListAsync();
        }

        public async Task<List<QuotesModel>> GetQuotesByTickerId(int tickerId, DateTime startDate, DateTime endDate)
        {

            return await _db_context.Quotes
                                        .Where(q => q.TickerId == tickerId && q.ts >= startDate && q.ts <= endDate)
                                        .OrderByDescending(q => q.ts)
                                        .ToListAsync();
        }

        public async Task DeleteQuotesBulkHard(IEnumerable<QuotesModel> quotes)
        {
            await _db_context.BulkDeleteAsync(quotes);
            await _db_context.SaveChangesAsync();
        }
    }
}
