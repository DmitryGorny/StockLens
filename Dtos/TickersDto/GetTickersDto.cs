using StockLens.Dtos.CitiesDtos;
using StockLens.Dtos.IndustriesDtos;

namespace StockLens.Dtos.TickersDto
{
    public class GetTickersDto
    {   
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
        public bool Privileged { get; set; }
        public string LongName { get; set; }
        public int ListLevel { get; set; }
        public decimal DividendsValue { get; set; }
        public decimal DividendsPercents { get; set; }
        public int IndustryId { get; set; }
        public int CityId { get; set; }
        public GetIndustryDto Industry { get; set; }
        public GetCitiesDto City { get; set; }
    }
}
