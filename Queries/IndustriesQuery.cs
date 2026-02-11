using System.ComponentModel.DataAnnotations;

namespace StockLens.Queries
{
    public class IndustriesQuery
    {
        public int SectorId { get; set; } 
        public bool InsideTickers { get; set; } = false;
        public int IndustryId { get; set; }
    }
}
