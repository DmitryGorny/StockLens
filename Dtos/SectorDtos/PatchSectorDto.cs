using System.ComponentModel.DataAnnotations;

namespace StockLens.Dtos.SectorDtos
{
    public class PatchSectorDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
