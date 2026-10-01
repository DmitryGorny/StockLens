using StockLens.Dtos.TickersDto;
using System.Linq.Expressions;

namespace StockLens.Services.Tickers.Filters
{
    public class CityFilter : IFilter<Models.Tickers>
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

        public Expression<Func<Models.Tickers, bool>?> GetFilterPredicate(FiltrationDto dto)
        {
            if (dto.CityIds == null)
                return null;

            var cityIds = dto.CityIds;
            return ticker => cityIds.Contains(ticker.CityId);
        }
    
    }
}
