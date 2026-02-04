using StockLens.Dtos.IndustriesDtos;
using StockLens.Dtos.TickersDto;
using StockLens.Queries;

namespace StockLens.Services.Tickers
{
    public interface ITickersService
    {
        public Task BulkCreateTickersAsync(List<CreateTickersDto> dtos);
        public Task AddTikersAsync(CreateTickersDto dto);
        public Task<GetTickersDto?> GetTickerAsync(int industryId);
        public Task<List<GetTickersDto>?> GetTickersAsync(TickersQuery query);
    }
}
