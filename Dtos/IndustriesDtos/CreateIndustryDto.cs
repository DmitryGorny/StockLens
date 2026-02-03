using StockLens.Models;

namespace StockLens.Dtos.IndustriesDtos
{
    public class CreateIndustryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Tickers> Tickers { get; set; } = new();

        public int SectorId { get; set; }
        public Sectors Sector { get; set; }
    }
}
