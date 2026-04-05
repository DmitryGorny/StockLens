using System.ComponentModel.DataAnnotations;

namespace StockLens.Dtos.IndustriesDtos
{
    /// <summary>
    /// DTO для частичного обновления индустрии (PATCH).
    /// Все свойства необязательны — передавайте только те поля, которые нужно изменить.
    /// </summary>
    public class PatchIndustryDto
    {
        /// <summary>
        /// Новое название индустрии.
        /// Если null — поле не будет изменено.
        /// Пример: "Энергетика".
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Новое краткое описание индустрии.
        /// Если null — поле не будет изменено.
        /// </summary>
        public string? Description { get; set; }
    }
}
