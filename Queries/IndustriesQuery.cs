using System.ComponentModel.DataAnnotations;

namespace StockLens.Queries
{
    public class IndustriesQuery
    {
        [Required]
        public int SectorId { get; set; } 

        public bool InsideTickers { get; set; } = false;
    }
}
