using StockLens.Queries;

namespace StockLens.Services.Analytics.TopTen
{
    public interface ITopTenFacade
    {
        public Task<string> GetTickersTopTen();
        public Task<string> GetCustomTickersTopTen(TickersQuery query);
    }
}
