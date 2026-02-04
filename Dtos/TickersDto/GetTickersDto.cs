namespace StockLens.Dtos.TickersDto
{
    public class GetTickersDto
    {   
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }

        public bool Privalaged { get; set; }

        public string LongName { get; set; }

        public int ListLevel { get; set; }

        public decimal DividentsValue { get; set; }

        public int IndustryId { get; set; }
    }
}
