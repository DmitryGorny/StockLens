using Org.BouncyCastle.Bcpg.OpenPgp;
using StockLens.Dtos.TickersDto;
using TickersModel = StockLens.Models.Tickers;

namespace StockLens.Repositories.Tickers
{
    public interface ITickersRepository
    {
        public Task BulkCreateTickersAsync(List<TickersModel> tickers);
        public Task CreateTicker(TickersModel dto);
        public Task PatchTicker(int TikerId, PatchTickerDto dto);
        public Task DeleteTickerHardAsync(TickersModel tiker);
        public Task AddTickerAsync(TickersModel ticker);
        public Task<IEnumerable<TickersModel>?> GetTickersByCitiesAsync(IEnumerable<int> citiesId, int start, int size);
        public Task<IEnumerable<TickersModel>?> GetTickersByCitiesAsync(IEnumerable<int> citiesId);
        public Task<IEnumerable<TickersModel>> GetTickersByIndustriesAsync(IEnumerable<int> industriesId, int start, int end);
        public Task<IEnumerable<TickersModel>> GetTickersAsync(int start, int end);
        public Task<IEnumerable<TickersModel>> GetTickersAsync();
        public Task<TickersModel?> GetTicker(int tickerId);
        public Task<List<TickersModel>> GetTickersByListLevel(int listLevel);
        public Task<TickersModel?> GetTicker(string symbol);
    }
}
