using StockLens.Migrations;

namespace StockLens.Models
{
    public class Briefcases
    {
        public int BriefcasesId { get; set; }
        public string  UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<BriefcasesTickers> BriefcasesTickers { get; set; }
        public List<Tickers> Tickers { get; set; }
    }
}
