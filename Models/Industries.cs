using System.Security.Cryptography.X509Certificates;

namespace StockLens.Models
{
    public class Industries
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        //public bool is_active { get; set; }

        public List<Tickers> Tickers { get; set; } = new();

        public Sectors Sector { get; set; }
    }
}
