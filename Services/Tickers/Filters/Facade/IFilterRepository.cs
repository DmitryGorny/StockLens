using StockLens.Dtos.TickersDto;

namespace StockLens.Services.Tickers.Filters.Facade
{
    public interface IFilterRepository<T> where T : class
    {
        public IQueryable<T> Filter(IQueryable<T> request, FiltrationDto filterDto);
    }
}
