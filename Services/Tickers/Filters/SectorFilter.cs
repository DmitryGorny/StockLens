using StockLens.Dtos.TickersDto;
using StockLens.Models;

namespace StockLens.Services.Tickers.Filters
{
    public class SectorFilter : IFilter
    {

        private readonly int _order;
        private readonly ParallelEnum.ParallelEnum _isParralel;
        public int Order => _order;
        public ParallelEnum.ParallelEnum isParallel => _isParralel;

        public SectorFilter()
        {
            _order = 2;
            _isParralel = ParallelEnum.ParallelEnum.Parallel;
        }

        public Task<IEnumerable<GetTickersDto>> Filter(IEnumerable<GetTickersDto> dtos, FiltrationDto dto)
        {
            if (dto.SectorIds == null)
                return Task.FromResult(dtos);
            var dtos_new = dtos.Where(d => dto.SectorIds.Contains(d.Industry.SectorId));
            return Task.FromResult(dtos_new);
        }
    }
}
