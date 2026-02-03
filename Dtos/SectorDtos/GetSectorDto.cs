namespace StockLens.Dtos.SectorDtos
{
    public class GetSectorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<string> IndustriesNames { get; set; } //TODO: Нужно ли?
    }
}
