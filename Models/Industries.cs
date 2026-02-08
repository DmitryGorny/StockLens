using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace StockLens.Models
{
    public class Industries
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public List<Tickers> Tickers { get; set; } = new();

        public int SectorId { get; set; }
        public Sectors Sector { get; set; }
    }
}
