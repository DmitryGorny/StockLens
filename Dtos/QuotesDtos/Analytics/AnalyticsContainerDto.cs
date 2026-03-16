namespace StockLens.Dtos.QuotesDtos.Analytics
{
    public class AnalyticsContainerDto<T> where T : IAnalyticsDto
    {
        public int ReactionToDrop { get; set; }
        public int MaxDrawdownPercent { get; set; }
        public int InvestmentHorizon { get; set; }
        public int Experience { get; set; }
        public IEnumerable<T> AnalyticsDtos { get; set; }
    }
}
