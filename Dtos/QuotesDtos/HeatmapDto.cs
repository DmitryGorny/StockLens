namespace StockLens.Dtos.QuotesDtos
{
    public class HeatmapDto
    {
        public string Symbol { get; set; }
        public string Date { get; set; }
        public decimal close { get; set; }
        public string Sector { get; set; }
    }
}
