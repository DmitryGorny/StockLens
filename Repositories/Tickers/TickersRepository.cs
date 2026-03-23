using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using StockLens.data;
using StockLens.Dtos.TickersDto;
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

        public async Task<IEnumerable<TickersModel>> GetTickers(IEnumerable<int> industriesId, int start, int end)
        {
            IEnumerable<TickersModel> tickers = await _db_context.Tickers.Where(t => industriesId.Contains(t.IndustryId))
                                        .Include(t => t.Industry)
                                        .OrderBy(t => t.Industry.Name)
                                        .Skip(start)
                                        .Take(end)
                                        .ToListAsync();
            return tickers;
        }
        public async Task<IEnumerable<TickersModel>> GetTickersAsync(int start, int end)
        {
            IEnumerable<TickersModel> tickers = await _db_context.Tickers
                                                                    .Include(t => t.Industry)
                                                                    .Include(t => t.City)
                                                                    .Skip(start)
                                                                    .Take(end)
                                                                    .OrderBy(t => t.Industry.Name)
                                                                    .ToListAsync();
            return tickers;
        }
        public async Task<TickersModel?> GetTicker(int tickerId)
        {
            var tick = _db_context.Tickers.Include(t => t.Industry).Include(t => t.City).FirstOrDefault(t => t.Id == tickerId);
            return tick;
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

        public async Task<IEnumerable<TickersModel>> GetTickers()
        {
            return await _db_context.Tickers.ToListAsync();
        }

        public async Task<IEnumerable<TickersModel>?> GetTickersByCitiesAsync(IEnumerable<int> citiesId, int start, int size)
        {
            return await _db_context.Tickers.Where(t => citiesId.Contains(t.CityId))
                                            .OrderBy(t => t.Name)
                                            .Skip(start)
                                            .Take(size)
                                            .ToListAsync();
        }

        public async Task<IEnumerable<TickersModel>?> GetTickersByCitiesAsync(IEnumerable<int> citiesId)
        {
            return await _db_context.Tickers.Where(t => citiesId.Contains(t.CityId))
                                            .OrderBy(t => t.Name)               
                                            .ToListAsync();
        }

        public async Task<IEnumerable<TickersModel>> GetTickersByIndustriesAsync(IEnumerable<int> industriesId, int start, int end)
        {
            return await _db_context.Tickers.Where(t => industriesId.Contains(t.IndustryId))
                                            .OrderBy(t => t.Name)
                                            .Skip(start)
                                            .Take(end)
                                            .ToListAsync();
        }

        public async Task<IEnumerable<TickersModel>> GetTickersAsync()
        {
            return await _db_context.Tickers.ToListAsync();
        }

        public async Task CreateTicker(TickersModel dto)
        {
            await _db_context.Tickers.AddAsync(dto);
            await _db_context.SaveChangesAsync();  
        }
    }
}
