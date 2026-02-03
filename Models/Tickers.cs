namespace StockLens.Models
{
    public class Tickers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }

        public bool Privalaged { get; set; }

        public string LongName { get; set; }

        public List<Quotes> Quotation { get; set; } = new();

        public Industries Industry { get; set; } 
    }
}
