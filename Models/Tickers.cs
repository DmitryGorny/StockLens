using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace StockLens.Models
{
    public class Tickers
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string? Description { get; set; }
        public bool Privileged { get; set; }
        public string LongName { get; set; }
        public decimal DividendsValue { get; set; }
        public decimal DividendsPercents { get; set; }
        public int ListLevel { get; set; }
        public List<Quotes> Quotation { get; set; } = new();
        public int IndustryId { get; set; }
        public Industries Industry { get; set; }     
        public int CityId { get; set; }
        public Cities City { get; set; }
        public List<BriefcasesTickers> BriefcasesTickers { get; set; }
        public List<Briefcases> Briefcases { get; set; }
    }
}
