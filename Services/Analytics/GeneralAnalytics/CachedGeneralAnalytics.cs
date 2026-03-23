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

        public async Task<string> GetCityGeneralAnalytics(int CityId, int daysNumber, UsersСharacteristicsDto dto)
        {
            string? result = await _cacheService.GetUnserializedCache("GeneralAnalytics", "GeneralAnalyticsCity", CityId.ToString(), daysNumber.ToString());
            if (result != null)
                return result;

            result = await _generalAnalyticsFacadeInner.GetCityGeneralAnalytics(CityId, daysNumber, dto);
            await _cacheService.SetCacheWithoutSerializing(result, "GeneralAnalytics", "GeneralAnalyticsCity", CityId.ToString(), daysNumber.ToString());
            return result;
        }

        public async Task<string> GetSectorsGeneralAnalytics(int SectorId, int daysNumber, UsersСharacteristicsDto dto)
        {
            string? result = await _cacheService.GetUnserializedCache("GeneralAnalytics", "GeneralAnalyticsSector", SectorId.ToString(), daysNumber.ToString());
            if (result != null) 
                return result;

            result = await _generalAnalyticsFacadeInner.GetSectorsGeneralAnalytics(SectorId, daysNumber, dto);
            await _cacheService.SetCacheWithoutSerializing(result, "GeneralAnalytics", "GeneralAnalyticsSector", SectorId.ToString(), daysNumber.ToString());
            return result; 
        }
        public async Task<string> GetIndustriesGeneralAnalytics(int IndustryId, int daysNumber, UsersСharacteristicsDto dto)
        {
            string? result = await _cacheService.GetUnserializedCache("GeneralAnalytics", "GeneralAnalyticsIndustry", IndustryId.ToString(), daysNumber.ToString());
            if (result != null)
                return result;



            result = await _generalAnalyticsFacadeInner.GetIndustriesGeneralAnalytics(IndustryId, daysNumber, dto);
            await _cacheService.SetCacheWithoutSerializing(result, "GeneralAnalytics", "GeneralAnalyticsIndustry", IndustryId.ToString(), daysNumber.ToString());
            return result;
        }
        public async Task<string> GetTickersGeneralAnalytics(int TickerId, int daysNumber, UsersСharacteristicsDto dto)
        {
            string? result = await _cacheService.GetUnserializedCache("GeneralAnalytics", "GeneralAnalyticsTicker", TickerId.ToString(), daysNumber.ToString());
            if (result != null)
                return result;

            result = await _generalAnalyticsFacadeInner.GetTickersGeneralAnalytics(TickerId, daysNumber, dto);
            await _cacheService.SetCacheWithoutSerializing(result, "GeneralAnalytics", "GeneralAnalyticsTicker", TickerId.ToString(), daysNumber.ToString());
            return result;
        }
    }
}
