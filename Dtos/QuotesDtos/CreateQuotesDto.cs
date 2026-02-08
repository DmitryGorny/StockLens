using System.Numerics;

namespace StockLens.Dtos.QuotationsDtos
{
    public class CreateQuotesDto
    {
        public DateTime ts { get; set; }
        public decimal open { get; set; }
        public decimal close { get; set; }
        public decimal low { get; set; }
        public decimal high { get; set; }
        public BigInteger volume { get; set; }
        public BigInteger value { get; set; }
        public BigInteger numtrades { get; set; }
        public decimal waprice { get; set; }
        public int TickerId { get; set; }
    }
}
