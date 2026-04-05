using System.ComponentModel.DataAnnotations;

namespace StockLens.Dtos.TickersDto
{
    /// <summary>
    /// DTO для создания записи тикера.
    /// Все поля обязательны и используются для валидации при создании тикера.
    /// </summary>
    public class CreateTickersDto
    {
        /// <summary>
        /// Название тикера (читаемое имя).
        /// Пример: "Газпром".
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Тикер/символ для торгов (уникальный идентификатор на бирже).
        /// Пример: "GAZP".
        /// </summary>
        [Required]
        public string Symbol { get; set; }

        /// <summary>
        /// Краткое описание компании или эмитента.
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Признак привилегированности акций (true — привилегированные).
        /// </summary>
        [Required]
        public bool Privileged { get; set; }

        /// <summary>
        /// Уровень листинга (целое число).
        /// Используется для сортировки/фильтрации по биржевым уровням.
        /// </summary>
        [Required]
        public int ListLevel { get; set; }

        /// <summary>
        /// Полное наименование эмитента.
        /// </summary>
        [Required]
        public string LongName { get; set; }

        /// <summary>
        /// Абсолютная величина дивидендов (в валюте).
        /// </summary>
        [Required]
        public decimal DividendsValue { get; set; }

        /// <summary>
        /// Процент дивидендной доходности.
        /// </summary>
        [Required]
        public decimal DividendsPercents { get; set; }

        /// <summary>
        /// Идентификатор отрасли (внешний ключ).
        /// </summary>
        [Required]
        public int IndustryId { get; set; }

        /// <summary>
        /// Идентификатор города (внешний ключ).
        /// </summary>
        [Required]
        public int CityId { get; set; }
    }
}
