using StockLens.Models;
using System.ComponentModel.DataAnnotations;

namespace StockLens.Dtos.IndustriesDtos
{
    public class CreateIndustryDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int SectorId { get; set; }
    }
}
