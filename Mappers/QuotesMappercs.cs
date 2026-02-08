using StockLens.Dtos.QuotationsDtos;
using StockLens.Models;

namespace StockLens.Mappers
{
    public static class QuotesMappercs
    {
        public static Quotes ToQuotesFromDto(this CreateQuotesDto dto)
        {
            return new Quotes
            {
                ts = dto.ts,
                numtrades = dto.numtrades,
                value = dto.value,
                volume = dto.volume,
                close = dto.close,
                high = dto.high,
                TickerId = dto.TickerId,
                low = dto.low,
                open = dto.open,
                waprice = dto.waprice,
            };
        }
    }
}
