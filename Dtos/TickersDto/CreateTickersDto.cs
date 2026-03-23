using System.ComponentModel.DataAnnotations;

namespace StockLens.Dtos.TickersDto
{
    public class CreateTickersDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Symbol { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool Privileged { get; set; }
        [Required]
        public int ListLevel { get; set; }
        [Required]
        public string LongName { get; set; }
        [Required]
        public decimal DividendsValue { get; set; }
        [Required]
        public decimal DividendsPercents { get; set; }
        [Required]
        public int IndustryId { get; set; }

        [Required]
        public int CityId { get; set; }
    }
}
