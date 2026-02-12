using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using StockLens.data;
using TickersModel = StockLens.Models.Tickers;


namespace StockLens.Repositories.Tickers
{
    public class TickersRepository : ITickersRepository
    {
        private readonly AppDBContext _db_context;

        public TickersRepository(AppDBContext db_context)
        {
            _db_context = db_context;
        }

        public async Task BulkCreateTickersAsync(List<TickersModel> tickers)
        { 
            var config = new BulkConfig
            {
                SetOutputIdentity = true,
                PreserveInsertOrder = true,
                UpdateByProperties = new()
                {
                   nameof(TickersModel.Name),
                   nameof(TickersModel.Privileged), 
                },
                PropertiesToExclude = new List<string> { "Id" }
            };
            await _db_context.BulkInsertOrUpdateAsync(tickers, config);
        }
        public async Task AddTickerAsync(TickersModel ticker) 
        {
            await _db_context.Tickers.AddAsync(ticker);
        }

        public async Task<IReadOnlyList<TickersModel>> GetTickers(IReadOnlyCollection<int> industriesId, int start, int end)
        {
            IReadOnlyList<TickersModel> tickers = await _db_context.Tickers.Where(t => industriesId.Contains(t.IndustryId))
                                        .OrderBy(t => t.Industry.Name)
                                        .Skip(start)
                                        .Take(end)
                                        .ToListAsync();
            return tickers;
        }
        public async Task<IReadOnlyList<TickersModel>> GetTickers(int start, int end)
        {
            IReadOnlyList<TickersModel> tickers =
                await _db_context.Tickers.Skip(start).Take(end).OrderBy(t => t.Industry.Name).ToListAsync();
            return tickers;
        }
        public async Task<TickersModel>? GetTicker(int tickerId)
        {
            return await _db_context.Tickers.FindAsync(tickerId);
        }

        public async Task<TickersModel?> GetTickerWithDependencies(int tickerId, int quotesNumbers)
        {
            return await _db_context.Tickers.Include(t => t.Quotation
                                                           .OrderByDescending(q => q.ts)
                                                           .Take(quotesNumbers)).FirstOrDefaultAsync(t => t.Id == tickerId);
        }

        public async Task<List<TickersModel>> GetTickersByListLevel(int listLevel)
        {
            return await _db_context.Tickers.Where(t => t.ListLevel == listLevel)
                                                    .Include(t => t.Industry)
                                                    .ThenInclude(i => i.Sector)
                                                    .ToListAsync();
        }

        public async Task<TickersModel?> GetTicker(string symbol)
        {
            return await _db_context.Tickers.Include(t => t.Industry)
                                                    .ThenInclude(i => i.Sector).FirstOrDefaultAsync(t => t.Symbol == symbol);
                                                    
        }
    }
}
