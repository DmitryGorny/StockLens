using StockLens.Dtos.BriefcasesTickersDtos;

namespace StockLens.Dtos.BriefcasesDtos
{
    public class PatchBriefcaseDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public PatchBriefcasesTickersDto? Tickers { get; set; }
    }
}
