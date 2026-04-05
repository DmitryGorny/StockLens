using StockLens.Models;
using System.ComponentModel.DataAnnotations;

namespace StockLens.Dtos.IndustriesDtos
{
    /// <summary>
    /// DTO для создания новой индустрии (Industry).
    /// Все свойства обязательны и используются для валидации на входе.
    /// </summary>
    public class CreateIndustryDto
    {
        /// <summary>
        /// Название индустрии.
        /// Пример: "Нефтегазовая".
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Краткое описание индустрии.
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Идентификатор сектора, к которому относится индустрия (внешний ключ).
        /// </summary>
        [Required]
        public int SectorId { get; set; }
    }
}
