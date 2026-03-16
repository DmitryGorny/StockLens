using StockLens.Dtos.AuthDtos;
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

        public async Task<string> GetSectorsGeneralAnalytics(int SectorId, UsersСharacteristicsDto dto)
        {
            string? result = await _cacheService.GetUnserializedCache("GeneralAnalytics", "GeneralAnalyticsSector", SectorId.ToString());
            if (result != null) 
                return result;

            result = await _generalAnalyticsFacadeInner.GetSectorsGeneralAnalytics(SectorId, dto);
            await _cacheService.SetCacheWithoutSerializing(result, "GeneralAnalytics", "GeneralAnalyticsSector", SectorId.ToString());
            return result; 
        }
        public async Task<string> GetIndustriesGeneralAnalytics(int IndustryId, UsersСharacteristicsDto dto)
        {
            string? result = await _cacheService.GetUnserializedCache("GeneralAnalytics", "GeneralAnalyticsIndustry", IndustryId.ToString());
            if (result != null)
                return result;

            result = await _generalAnalyticsFacadeInner.GetIndustriesGeneralAnalytics(IndustryId, dto);
            await _cacheService.SetCacheWithoutSerializing(result, "GeneralAnalytics", "GeneralAnalyticsIndustry", IndustryId.ToString());
            return result;
        }
        public async Task<string> GetTickersGeneralAnalytics(int TickerId, UsersСharacteristicsDto dto)
        {
            string? result = await _cacheService.GetUnserializedCache("GeneralAnalytics", "GeneralAnalyticsTicker", TickerId.ToString());
            if (result != null)
                return result;

            result = await _generalAnalyticsFacadeInner.GetTickersGeneralAnalytics(TickerId, dto);
            await _cacheService.SetCacheWithoutSerializing(result, "GeneralAnalytics", "GeneralAnalyticsTicker", TickerId.ToString());
            return result;
        }
    }
}
