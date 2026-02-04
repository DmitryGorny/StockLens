using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
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
            await _db_context.BulkInsertAsync(tickers);
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
    }
}
