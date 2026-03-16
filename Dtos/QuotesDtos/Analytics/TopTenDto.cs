using System.Numerics;
using System.Text.Json.Serialization;

namespace StockLens.Dtos.QuotesDtos.Analytics
{
    public class TopTenDto : IAnalyticsDto
    {
        public string Symbol { get; set; }
        public string Date { get; set; }
        public decimal close { get; set; }
        public decimal avg_dividend { get; set; }
        public string value { get; set; }
    }
}
