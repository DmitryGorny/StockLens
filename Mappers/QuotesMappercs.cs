using StockLens.Dtos.QuotationsDtos;
using StockLens.Dtos.QuotesDtos;
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

        public static GeneralAnalyticsDto ToGeneralAnalyticFromQuotaion(this Quotes quotation)
        {
            return new GeneralAnalyticsDto
            {
                Symbol = quotation.Ticker.Symbol,
                Date = quotation.ts.ToString("yyyy-MM-dd"),
                close = quotation.close
            };
        }

        public static HeatmapDto ToHeatmapFromQuotaion(this Quotes quotation)
        {
            return new HeatmapDto
            {
                Symbol = quotation.Ticker.Symbol,
                Date = quotation.ts.ToString("yyyy-MM-dd"),
                close = quotation.close,
                Sector = quotation.Ticker.Industry.Sector.Name
            };
        }

        public static TopTenDto ToTopTenFromQuotaion(this Quotes quotation)
        {
            return new TopTenDto
            {
                Symbol = quotation.Ticker.Symbol,
                Date = quotation.ts.ToString("yyyy-MM-dd"),
                close = quotation.close,
                avg_dividend = quotation.Ticker.DividendsValue,
                value = quotation.value.ToString()
            };
        }

        public static PortfolioDto ToPortfolioFromQuotaion(this Quotes quotation, decimal percentage)
        {
            return new PortfolioDto
            {
                Symbol = quotation.Ticker.Symbol,
                Date = quotation.ts.ToString("yyyy-MM-dd"),
                close = quotation.close,
                Percentage = percentage,
            };
        }
    }
}
