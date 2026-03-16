namespace StockLens.Dtos.QuotesDtos.Analytics
{
    public class OptimizePortfolioDto : IAnalyticsDto
    {
        public string Symbol { get; set; }
        public string Date { get; set; }
        public decimal close { get; set; }
    }
}
