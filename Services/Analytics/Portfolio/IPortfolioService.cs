using StockLens.Queries;

namespace StockLens.Services.Analytics.Portfolio
{
    public interface IPortfolioService
    {
        public Task<string> GetPorfolioMetrics(Dictionary<int, decimal> tickersAndPercantages);
        public Task<string> GetOptimizedPortfolio(List<int> tickersIds);
    }
}
