namespace StockLens.Queries
{
    public class TickersQuery
    {
        public int TickerId { get; set; }
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 50;
        public IReadOnlyCollection<int>? InudustriesId { get; set; } = null;
        public bool? levelSortDesc { get; set; } = null;
        public bool? PrivalagedSort { get; set; } = null;
        public bool? DividendsSortDesc { get; set; } = null;
        public int? CityFiltersId { get; set; } = null;

     }
}
