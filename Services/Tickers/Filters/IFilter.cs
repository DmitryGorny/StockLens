using StockLens.Dtos.TickersDto;

namespace StockLens.Services.Tickers.Filters
{
    public interface IFilter
    {
        int Order { get;  }
        ParallelEnum.ParallelEnum isParallel { get; }
        public Task<IEnumerable<GetTickersDto>> Filter(IEnumerable<GetTickersDto> dtos, FiltrationDto dto);
    }
}
