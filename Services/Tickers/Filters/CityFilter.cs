using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Internal;
using StockLens.Dtos.TickersDto;
using StockLens.Models;

namespace StockLens.Services.Tickers.Filters
{
    public class CityFilter : IFilter
    {
        private readonly int _order;
        private readonly ParallelEnum.ParallelEnum _isParralel;
        public int Order => _order;
        public ParallelEnum.ParallelEnum isParallel => _isParralel;

        public CityFilter() 
        {
            _order = 3;
            _isParralel = ParallelEnum.ParallelEnum.NotParallel;
        }

        public Task<IEnumerable<GetTickersDto>> Filter(IEnumerable<GetTickersDto> dtos, FiltrationDto dto)
        {
            if (dto.CityIds == null)
                return Task.FromResult(dtos);
            var dtos_new = dtos.Where(d => dto.CityIds.Contains(d.CityId));
            return Task.FromResult(dtos_new);
        }
    }
}
