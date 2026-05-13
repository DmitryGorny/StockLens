using StockLens.Dtos.TickersDto;
using StockLens.Models;

namespace StockLens.Services.Tickers.Filters.Facade
{
    public class FilterFacade : IFilterFacade
    {
        private List<IFilter> filters = new() { 
            new SectorFilter(), 
            new IndustryFilter(), 
            new CityFilter(),
            new ListLevelFilter()
            };

        public async Task<IEnumerable<GetTickersDto>> Filter(IEnumerable<GetTickersDto> dtos, FiltrationDto filterDto)
        {

            List<Task<IEnumerable<GetTickersDto>>> tasks = new();

            IEnumerable<GetTickersDto> filteredDtos = dtos;

            foreach (var filter in filters) 
            {
                if (filter.isParallel == ParallelEnum.ParallelEnum.Parallel)
                {
                    tasks.Add(filter.Filter(dtos, filterDto));
                }
                else
                {
                    var results = await Task.WhenAll(tasks);

                    IEnumerable<GetTickersDto> tickers = Enumerable.Empty<GetTickersDto>();

                    foreach (var r in results)
                    {
                        tickers = tickers.Union(r, new TickerComparer()).ToList();
                    }
                    tasks.Clear();
                    
                    if (tickers.Count() == 0)
                        tickers = filteredDtos;

                    var new_tickers = await filter.Filter(tickers, filterDto);
                    var list = new_tickers.ToList();
                    filteredDtos = new_tickers;
                }
            }
            if (tasks.Count > 0)
            {
                var results = await Task.WhenAll(tasks);
                IEnumerable<GetTickersDto> tickers = Enumerable.Empty<GetTickersDto>();
                foreach (var r in results)
                {
                    tickers = tickers.Union(r, new TickerComparer());
                }
                filteredDtos = tickers;
            }

            return filteredDtos;

        }
    }

    class TickerComparer : IEqualityComparer<GetTickersDto>
    {
        public bool Equals(GetTickersDto x, GetTickersDto y) => x.Id == y.Id;
        public int GetHashCode(GetTickersDto obj) => obj.Id.GetHashCode();
    }
}
