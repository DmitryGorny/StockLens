using StockLens.Dtos.TickersDto;
using System.Linq.Expressions;

namespace StockLens.Services.Tickers.Filters
{
    public class IndustryFilter : IFilter<Models.Tickers>
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

        public Expression<Func<Models.Tickers, bool>?> GetFilterPredicate(FiltrationDto dto)
        {
            if (dto.IndustryIds == null)
                return null;

            return (Models.Tickers ticker) => dto.IndustryIds.Contains(ticker.IndustryId);
            
        }
    }
}
