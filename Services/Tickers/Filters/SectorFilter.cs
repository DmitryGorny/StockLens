using StockLens.Dtos.TickersDto;
using System.Linq.Expressions;

namespace StockLens.Services.Tickers.Filters
{
    public class SectorFilter : IFilter<Models.Tickers>
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

        public Expression<Func<Models.Tickers, bool>?> GetFilterPredicate(FiltrationDto dto)
        {
            if (dto.SectorIds == null)
                return null;

            return (Models.Tickers ticker) => dto.SectorIds.Contains(ticker.Industry.SectorId);
           
        }
    }
}
