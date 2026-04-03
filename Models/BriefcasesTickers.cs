namespace StockLens.Models
{
    public class BriefcasesTickers
    {
        public int TickerId { get; set; }
        public int BriefcaseId { get; set; }
        public Briefcases Briefcase { get; set; }
        public Tickers Ticker { get; set; }
        public decimal percantage { get; set; }
    }
}
