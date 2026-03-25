using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using StockLens.data;
using StockLens.Dtos.TickersDto;
using StockLens.Mappers;
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
            return await _db_context.Tickers.Include(t => t.Industry).Include(t => t.City).ToListAsync();
        }

        public async Task CreateTicker(TickersModel ticker)
        {
            var industry = await _db_context.Industries.FirstOrDefaultAsync(i => i.Id == ticker.IndustryId);
            var city = await _db_context.Cities.FirstOrDefaultAsync(c => c.Id == ticker.CityId);

            if (industry == null || city == null)
                throw new Exception("Данные неккоректны");

            await _db_context.Tickers.AddAsync(ticker);
            await _db_context.SaveChangesAsync();  
        }

        public async Task PatchTicker(int TikerId, PatchTickerDto dto)
        {
            var ticker = await _db_context.Tickers.FindAsync(TikerId);

            if (ticker == null)
                throw new Exception($"Тикера с id {TikerId} не найдено");
            ticker.PatchTiker(dto);
            await _db_context.SaveChangesAsync();
        }

        public async Task DeleteTickerHardAsync(TickersModel ticker)
        {
            _db_context.Tickers.Remove(ticker); 
            await _db_context.SaveChangesAsync();
        }
    }
}
