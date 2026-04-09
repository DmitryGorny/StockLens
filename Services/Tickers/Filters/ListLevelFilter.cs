using StockLens.Dtos.TickersDto;

namespace StockLens.Services.Tickers.Filters
{
    public class ListLevelFilter : IFilter
    {
        private readonly int _order;
        private readonly ParallelEnum.ParallelEnum _isParralel;
        public int Order => _order;
        public ParallelEnum.ParallelEnum isParallel => _isParralel;

        public ListLevelFilter()
        {
            _order = 4;
            _isParralel = ParallelEnum.ParallelEnum.NotParallel;
        }

        public Task<IEnumerable<GetTickersDto>> Filter(IEnumerable<GetTickersDto> dtos, FiltrationDto dto)
        {
            if (dto.ListLevel == null)
                return Task.FromResult(dtos);
            var dtos_new = dtos.Where(d => d.ListLevel == dto.ListLevel);
            return Task.FromResult(dtos_new);
        }
    }
}
