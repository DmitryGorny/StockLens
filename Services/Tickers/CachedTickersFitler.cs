using StockLens.Dtos.TickersDto;
using StockLens.Services.Cache;
using StockLens.Services.FiltrationService;
using System.Text.Json;

namespace StockLens.Services.Tickers
{
    public class CachedTickersFitler : IFiltrationService
    {
        private readonly ICacheService _cacheService;
        private readonly IFiltrationService _tickersService;

        public CachedTickersFitler(ICacheService cacheService, IFiltrationService filtrationService)
        {
            _cacheService = cacheService;
            _tickersService = filtrationService;
        }

        public async Task<IEnumerable<GetTickersDto>> LayeredFiltration(FiltrationDto dto)
        {
            var tickers = await _cacheService.GetCache<GetTickersDto>("TickersFitler", JsonSerializer.Serialize(dto));
            if (tickers != null)
                return tickers;

            var result = await _tickersService.LayeredFiltration(dto);
            await _cacheService.SetCache(result, "TickersFitler", JsonSerializer.Serialize(dto)); 
            return result;
        }
    }
}
