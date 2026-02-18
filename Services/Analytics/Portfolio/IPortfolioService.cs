using StockLens.Queries;

namespace StockLens.Services.Analytics.Portfolio
{
    public interface IPortfolioService
    {
        public Task<string> GetPorfolioMetrics(TickersQuery query);
    }
}
