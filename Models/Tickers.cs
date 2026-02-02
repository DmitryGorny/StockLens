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

        public List<Quotation> Quotation { get; set; } = new();

        public Industry Industry { get; set; } 
    }
}
