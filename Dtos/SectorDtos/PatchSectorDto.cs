using System.ComponentModel.DataAnnotations;

namespace StockLens.Dtos.SectorDtos
{
    public class PatchSectorDto
    {
        /// <summary>
        /// Название сектора (читаемое имя).
        /// Если не указано, то не будет изменено.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Описание.
        /// Если не указано, то не будет изменено.
        /// </summary>
        public string? Description { get; set; }
    }
}
