using StockLens.Dtos.TickersDto;
using StockLens.Services.Search;

namespace StockLens.Services.Tickers
{
    public interface ITickersService : ISearch<string, SearchTickerDto>
    {
        public Task<List<GetTickersDto>> BulkCreateTickersAsync(List<CreateTickersDto> dtos);
        public Task CreateTicker(CreateTickersDto dto);
        public Task PatchTickerAsync(int TickerId, PatchTickerDto dto);
        public Task DeleteTickerAsync(int TickerId);
        public Task AddTikersAsync(CreateTickersDto dto);
        public Task<GetTickersDto> GetTickerByIdAsync(int TickerId);
        public Task<IEnumerable<GetTickersDto?>> GetTickersByIndustriesAsync(IEnumerable<int> industriesId, int start, int size);
        public Task<IEnumerable<GetTickersDto>> GetTickersByCitiesAsync(IEnumerable<int> citiesId, int start, int size);
        public Task<IEnumerable<GetTickersDto>> GetTickersAsync(int start, int size);
        public Task<IEnumerable<GetTickersDto>> GetTickersAsync();
        public Task<IEnumerable<GetTickersDto>> GetTickersFiltered(FiltrationDto dto);
    }
}
