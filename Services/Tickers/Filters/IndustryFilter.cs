using StockLens.Dtos.TickersDto;
using StockLens.Models;

namespace StockLens.Services.Tickers.Filters
{
    public class IndustryFilter : IFilter
    {
        private readonly int _order;
        private readonly ParallelEnum.ParallelEnum _isParralel;
        public int Order => _order;
        public ParallelEnum.ParallelEnum isParallel => _isParralel;

        public IndustryFilter()
        {
            _order = 1;
            _isParralel = ParallelEnum.ParallelEnum.Parallel;
        }

        public Task<IEnumerable<GetTickersDto>> Filter(IEnumerable<GetTickersDto> dtos, FiltrationDto dto)
        {
            if (dto.IndustryIds == null)
                return Task.FromResult(Enumerable.Empty<GetTickersDto>());
            var dtos_new = dtos.Where(d => dto.IndustryIds.Contains(d.IndustryId));
            return Task.FromResult(dtos_new);
        }
    }
}
