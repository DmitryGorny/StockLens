using StockLens.Models;
using System.ComponentModel.DataAnnotations;

namespace StockLens.Dtos.SectorDtos
{
    public class CreateSectorDto
    {  
        /// <summary>
        /// Название Сектора (читаемое имя).
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [Required]
        public string Description { get; set; }
        
    }
}
