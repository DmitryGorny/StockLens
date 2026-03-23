using StockLens.Dtos.AuthDtos;

namespace StockLens.Services.Analytics.Portfolio
{
    public interface IPortfolioService
    {
        public Task<string> GetPorfolioMetrics(Dictionary<int, decimal> tickersAndPercantages, UsersСharacteristicsDto dto);
        public Task<string> GetOptimizedPortfolio(List<int> tickersIds, UsersСharacteristicsDto dto);
    }
}
