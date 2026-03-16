namespace StockLens.Dtos.QuotesDtos.Analytics
{
    public class PortfolioDto : IAnalyticsDto
    {
        public string Symbol { get; set; }
        public string Date { get; set; }
        public decimal close { get; set; }
        public decimal Percentage { get; set; }
    }
}
