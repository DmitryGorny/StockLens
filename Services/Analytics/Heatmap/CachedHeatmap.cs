using StockLens.Services.Cache;

namespace StockLens.Services.Analytics.Heatmap
{
    public class CachedHeatmap : IHeatmapFacade
    {
        private readonly IHeatmapFacade _heatmapFacade;
        private readonly ICacheService _cacheService;

        public CachedHeatmap(IHeatmapFacade heatmapFacade, ICacheService cacheService)
        {
            _heatmapFacade = heatmapFacade;
            _cacheService = cacheService;
        }

        public async Task<string> GetTickersHeatmap()
        {
            string? result = await _cacheService.GetUnserializedCache("Heatmap", "TickersHeatmap");
            if (result != null)
                return result;
            return await _heatmapFacade.GetTickersHeatmap();
        }
    }
}
