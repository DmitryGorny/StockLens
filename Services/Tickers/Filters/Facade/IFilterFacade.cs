using StockLens.Dtos.TickersDto;

namespace StockLens.Services.Tickers.Filters.Facade
{
    public interface IFilterFacade
    {
        public Task<IEnumerable<GetTickersDto>> Filter(IEnumerable<GetTickersDto> dtos, FiltrationDto filterDto);
    }
}
