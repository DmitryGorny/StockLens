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
                Privileged = ticker.Privileged,
                LongName = ticker.LongName,
                DividendsValue = ticker.DividendsValue,
                DividendsPercents = ticker.DividendsPercents,
                Symbol = ticker.Symbol,
                IndustryId = ticker.IndustryId,
                ListLevel = ticker.ListLevel,
                CityId = ticker.CityId,
            };
        }

        public static Tickers CreateTickerFromDto(this CreateTickersDto dto)
        {
            return new Tickers
            {
                Name = dto.Name,
                Description = dto.Description,
                Privileged = dto.Privileged,
                LongName = dto.LongName,
                DividendsValue = dto.DividendsValue,
                DividendsPercents = dto.DividendsPercents,
                Symbol = dto.Symbol,
                IndustryId = dto.IndustryId,
                ListLevel = dto.ListLevel,
                CityId = dto.CityId,
            };
        }
    }
}
