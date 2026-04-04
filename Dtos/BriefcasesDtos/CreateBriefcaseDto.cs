using System.Text.Json.Serialization;

namespace StockLens.Dtos.BriefcasesDtos
{
    public class CreateBriefcaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<int, decimal> tickersIdsAndPercantages { get; set; } = [];
    }
}
