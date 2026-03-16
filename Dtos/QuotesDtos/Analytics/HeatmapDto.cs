namespace StockLens.Dtos.QuotesDtos.Analytics
{
    public class HeatmapDto : IAnalyticsDto
    {
        public string Symbol { get; set; }
        public string Date { get; set; }
        public decimal close { get; set; }
        public string Sector { get; set; }
    }
}
