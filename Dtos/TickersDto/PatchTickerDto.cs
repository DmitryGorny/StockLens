using System.ComponentModel.DataAnnotations;

namespace StockLens.Dtos.TickersDto
{
    /// <summary>
    /// DTO для частичного обновления тикера (PATCH).
    /// Все свойства необязательны — передавайте только те поля, которые нужно обновить.
    /// </summary>
    public class PatchTickerDto
    {
        /// <summary>
        /// Новое читаемое название тикера.
        /// Если null — поле не будет изменено.
        /// Пример: "Газпром".
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Новый торговый символ (тикер).
        /// Если null — поле не будет изменено.
        /// Пример: "GAZP".
        /// </summary>
        public string? Symbol { get; set; }

        /// <summary>
        /// Новое краткое описание компании или эмитента.
        /// Если null — поле не будет изменено.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Флаг привилегированности акций.
        /// Если null — поле не будет изменено. true — привилегированные акции.
        /// </summary>
        public bool? Privileged { get; set; }

        /// <summary>
        /// Уровень листинга (целое число).
        /// Если null — поле не будет изменено.
        /// </summary>
        public int? ListLevel { get; set; }

        /// <summary>
        /// Полное наименование эмитента.
        /// Если null — поле не будет изменено.
        /// </summary>
        public string? LongName { get; set; }

        /// <summary>
        /// Абсолютная величина дивидендов (в валюте).
        /// Если null — поле не будет изменено.
        /// </summary>
        public decimal? DividendsValue { get; set; }

        /// <summary>
        /// Процент дивидендной доходности.
        /// Если null — поле не будет изменено.
        /// </summary>
        public decimal? DividendsPercents { get; set; }

        /// <summary>
        /// Идентификатор отрасли (внешний ключ).
        /// Если null — поле не будет изменено.
        /// </summary>
        public int? IndustryId { get; set; }
    }
}
