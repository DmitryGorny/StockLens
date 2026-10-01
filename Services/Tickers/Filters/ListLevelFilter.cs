using StockLens.Dtos.TickersDto;
using System.Linq.Expressions;

namespace StockLens.Services.Tickers.Filters
{
    public class ListLevelFilter : IFilter<Models.Tickers>
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

        public Expression<Func<Models.Tickers, bool>?> GetFilterPredicate(FiltrationDto dto)
        {
            if (dto.ListLevel == null)
                return null;

            return (Models.Tickers ticker) => ticker.ListLevel == dto.ListLevel;
        }
    }
}
