using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace StockLens.Models
{
    public class Quotation
    {
        public int Id { get; set; }
        public DateTime ts { get; set; }
        decimal open { get; set; }
        decimal close { get; set; }
        public decimal low { get; set; }
        public decimal high { get; set; }

        public BigInteger volume { get; set; }
        public BigInteger value { get; set; }

        public BigInteger numtrades { get; set; }

        public decimal waprice { get; set; }

        public Tickers Ticker { get; set; }
    }   
}
