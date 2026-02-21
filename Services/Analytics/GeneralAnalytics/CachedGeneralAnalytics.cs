using StockLens.Queries;
using StockLens.Services.Cache;

namespace StockLens.Services.Analytics.GeneralAnalytics
{
    public class CachedGeneralAnalytics : IGeneralAnalyticsFacade
    {
        private readonly IGeneralAnalyticsFacade _generalAnalyticsFacadeInner;

        private readonly ICacheService _cacheService;

        public CachedGeneralAnalytics(IGeneralAnalyticsFacade generalAnalyticsFacadeInner, ICacheService cacheService)
        {
            _generalAnalyticsFacadeInner = generalAnalyticsFacadeInner;
            _cacheService = cacheService;
        }

        public async Task<string> GetSectorsGeneralAnalytics(int SectorId)
        {
            string? result = await _cacheService.GetUnserializedCache("GeneralAnalytics", "GeneralAnalyticsSector", SectorId.ToString());
            if (result != null) 
                return result;

            result = await _generalAnalyticsFacadeInner.GetSectorsGeneralAnalytics(SectorId);
            await _cacheService.SetCacheWithoutSerializing(result, "GeneralAnalytics", "GeneralAnalyticsSector", SectorId.ToString());
            return result; 
        }
        public async Task<string> GetIndustriesGeneralAnalytics(int IndustryId)
        {
            string? result = await _cacheService.GetUnserializedCache("GeneralAnalytics", "GeneralAnalyticsIndustry", IndustryId.ToString());
            if (result != null)
                return result;

            result = await _generalAnalyticsFacadeInner.GetIndustriesGeneralAnalytics(IndustryId);
            await _cacheService.SetCacheWithoutSerializing(result, "GeneralAnalytics", "GeneralAnalyticsIndustry", IndustryId.ToString());
            return result;
        }
        public async Task<string> GetTickersGeneralAnalytics(int TickerId)
        {
            string? result = await _cacheService.GetUnserializedCache("GeneralAnalytics", "GeneralAnalyticsTicker", TickerId.ToString());
            if (result != null)
                return result;

            result = await _generalAnalyticsFacadeInner.GetIndustriesGeneralAnalytics(TickerId);
            await _cacheService.SetCacheWithoutSerializing(result, "GeneralAnalytics", "GeneralAnalyticsTicker", TickerId.ToString());
            return result;
        }
    }
}
