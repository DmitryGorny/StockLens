using StockLens.Dtos.SectorDtos;
using StockLens.Dtos.TickersDto;
using StockLens.Models;

namespace StockLens.Mappers
{
    public static class TickersMapper
    {
        public static GetTickersDto CreateDtoFromTickers(this Tickers ticker)
        {
            return new GetTickersDto
            {
                Id = ticker.Id,
                Name = ticker.Name,
                Description = ticker.Description,
                Privalaged = ticker.Privalaged,
                LongName = ticker.LongName,
                DividentsValue = ticker.DividentsValue,
                Symbol = ticker.Symbol,
                IndustryId = ticker.Industry.Id,
                ListLevel = ticker.ListLevel,
            };
        }

        public static Tickers CreateTickerFromDto(this CreateTickersDto dto)
        {
            return new Tickers
            {
                Name = dto.Name,
                Description = dto.Description,
                Privalaged = dto.Privalaged,
                LongName = dto.LongName,
                DividentsValue = dto.DividentsValue,
                Symbol = dto.Symbol,
                IndustryId = dto.IndustryId,
                ListLevel = dto.ListLevel,
            };
        }
    }
}
