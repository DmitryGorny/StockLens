using LinqKit;
using Microsoft.EntityFrameworkCore;
using StockLens.Dtos.TickersDto;

namespace StockLens.Services.Tickers.Filters.Facade
{
    public class FilterRepository : IFilterRepository<Models.Tickers>
    {
        private List<IFilter<Models.Tickers>> filters = new() { 
            new SectorFilter(), 
            new IndustryFilter(), 
            new CityFilter(),
            new ListLevelFilter()
            };

        public IQueryable<Models.Tickers> Filter(IQueryable<Models.Tickers> request, FiltrationDto filterDto)
        {
            var predicate = PredicateBuilder.New<Models.Tickers>();

            foreach (var filter in filters)
            {
                var filterPredicate = filter.GetFilterPredicate(filterDto);
                if (filterPredicate == null)
                    continue;

                if (filter.isParallel == ParallelEnum.ParallelEnum.Parallel)
                {
                    predicate = predicate.Or(filterPredicate);
                }
                else
                {
                    request = request.Where(filterPredicate);
                }
            }
            if (predicate.IsStarted)
                request = request.Where(predicate);
            return request;
        }
    }

    class TickerComparer : IEqualityComparer<GetTickersDto>
    {
        public bool Equals(GetTickersDto x, GetTickersDto y) => x.Id == y.Id;
        public int GetHashCode(GetTickersDto obj) => obj.Id.GetHashCode();
    }
}
