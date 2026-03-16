using StockLens.Dtos.AuthDtos;
using StockLens.Services.Cache;

namespace StockLens.Services.Analytics.TopTen
{
    public class CachedTopTen : ITopTenFacade
    {
        private readonly ITopTenFacade _topTenFacade;
        private readonly ICacheService _cacheService;

        public CachedTopTen(ITopTenFacade topTenFacade, ICacheService cacheService)
        {
            _topTenFacade = topTenFacade;
            _cacheService = cacheService;
        }

        public async Task<string> GetTickersTopTen(UsersСharacteristicsDto CharDto)
        {
            string? result = await _cacheService.GetUnserializedCache("TopTen", "TickersTopTen");
            if (result != null)
                return result;

            result = await _topTenFacade.GetTickersTopTen(CharDto);
            await _cacheService.SetCacheWithoutSerializing(result, "TopTen", "TickersTopTen"); 
            return result;
            
        }
    }
}
