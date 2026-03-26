namespace StockLens.Dtos.TickersDto
{
    public class FiltrationDto
    {
        public IEnumerable<int>? SectorIds {  get; set; }
        public IEnumerable<int>? IndustryIds { get; set; }
        public IEnumerable<int>? CityIds { get; set; }

    }
}
