using StockLens.Dtos.TickersDto;
using TickersModel = StockLens.Models.Tickers;

namespace StockLens.Repositories.Tickers
{
    public interface ITickersRepository
    {
        public Task BulkCreateTickersAsync(List<TickersModel> tickers);
        public Task AddTickerAsync(TickersModel ticker);
        public Task<IEnumerable<TickersModel>?> GetTickersByCitiesAsync(IEnumerable<int> citiesId, int start, int size);
        public Task<IEnumerable<TickersModel>> GetTickersByIndustriesAsync(IEnumerable<int> industriesId, int start, int end);
        public Task<IEnumerable<TickersModel>> GetTickersAsync(int start, int end);
        public Task<IEnumerable<TickersModel>> GetTickersAsync();
        public Task<TickersModel?> GetTicker(int tickerId);
        public Task<TickersModel?> GetTickerWithDependencies(int tickerId, int quotesNumbers);
        public Task<List<TickersModel>> GetTickersByListLevel(int listLevel);
        public Task<TickersModel?> GetTicker(string symbol);
    }
}
