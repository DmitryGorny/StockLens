using StockLens.Dtos.TickersDto;
using TickersModel = StockLens.Models.Tickers;

namespace StockLens.Repositories.Tickers
{
    public interface ITickersRepository
    {
        public Task BulkCreateTickersAsync(List<TickersModel> tickers);
        public Task AddTickerAsync(TickersModel ticker);
        public Task<IReadOnlyList<TickersModel>> GetTickers(IReadOnlyCollection<int> industriesId, int start, int end);
        public Task<IReadOnlyList<TickersModel>> GetTickers(int start, int end);
        public Task<TickersModel>? GetTicker(int tickerId);
    }
}
