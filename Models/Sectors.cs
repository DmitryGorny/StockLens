using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace StockLens.Models
{
    public class Sectors
    {
        public int Id { get; set; }
        public string Name { get; set; } //Сделать валидацию уникальности
        public string? Description { get; set; } 
        public List<Industries> Industries { get; set; } = new();
    }
}
