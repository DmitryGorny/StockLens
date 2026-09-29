namespace StockLens.Dtos.BriefcasesTickersDtos
{
    public class PatchBriefcasesTickersDto
    {
        public Dictionary<int, decimal> NewTickersAndPercantages { get; set; }
        public List<int>? TickersToDelete { get; set; }
    }
}
