using StockLens.Dtos.TickersDto;
using System.Linq.Expressions;

namespace StockLens.Services.Tickers.Filters
{
    public interface IFilter<T> where T : class
    {
        int Order { get;  }
        ParallelEnum.ParallelEnum isParallel { get; }
        public Expression<Func<Models.Tickers, bool>?> GetFilterPredicate(FiltrationDto dto);
    }
}
