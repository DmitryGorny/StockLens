namespace StockLens.Models
{
    public class Cities
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Tickers> Tickers { get; set; } = new();
    }
}
