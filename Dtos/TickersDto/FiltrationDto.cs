namespace StockLens.Dtos.TickersDto
{
    public class FiltrationDto
    {
        /// <summary>
        /// Список id секторов
        /// </summary>
        public IEnumerable<int>? SectorIds {  get; set; }
        /// <summary>
        /// Список id индустрий
        /// </summary>
        public IEnumerable<int>? IndustryIds { get; set; }
        /// <summary>
        /// Список id городов
        /// </summary>
        public IEnumerable<int>? CityIds { get; set; }

    }
}
