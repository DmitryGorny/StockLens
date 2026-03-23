using StockLens.Dtos.AuthDtos;
using StockLens.Services.Cache;

namespace StockLens.Services.Analytics.Portfolio
{
    public class CachedPortfolio : IPortfolioService
    {
        private readonly IPortfolioService _portfolioService;
        private readonly ICacheService _cacheService;

        public CachedPortfolio(IPortfolioService portfolioService, ICacheService cacheService)
        {
            _portfolioService = portfolioService;
            _cacheService = cacheService;
        }

        public async Task<string> GetPorfolioMetrics(Dictionary<int, decimal> tickersAndPercantages, UsersСharacteristicsDto CharDto)
        {
            string pms = string.Join(",", tickersAndPercantages.Select(kv => $"{kv.Key}: {kv.Value}"));
            var result = await _cacheService.GetUnserializedCache("Portfolio",
                                                            "PortfolioMetrics",
                                                            pms);

            if (result != null)
                return result;

            result = await _portfolioService.GetPorfolioMetrics(tickersAndPercantages, CharDto);
            await _cacheService.SetCacheWithoutSerializing(result, "Portfolio", "PortfolioMetrics", pms);
            return result;
        }

        public async Task<string> GetOptimizedPortfolio(List<int> tickersIds, UsersСharacteristicsDto CharDto)
        {
            string ids = string.Join(",", tickersIds);
            string? result = await _cacheService.GetUnserializedCache("Portfolio", "OptimizedPortfolio", ids);
            if (result != null) 
                return result;

            result = await _portfolioService.GetOptimizedPortfolio(tickersIds, CharDto);
            await _cacheService.SetCacheWithoutSerializing(result, "Portfolio", "OptimizedPortfolio", ids);
            return result;
        }
    }
}
