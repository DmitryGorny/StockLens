namespace StockLens.Dtos.QuotesDtos
{
    public class PortfolioDto
    {
        public string Symbol { get; set; }
        public string Date { get; set; }
        public decimal close { get; set; }
        public decimal Percentage { get; set; }
    }
}
