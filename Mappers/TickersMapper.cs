using StockLens.Dtos.SectorDtos;
using StockLens.Dtos.TickersDto;
using StockLens.Models;
using System.Runtime.CompilerServices;

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
                Industry = ticker.Industry.ToDtoFromIndustries(),
                City = ticker.City.ToDtoFromCities(),
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

        public static void PatchTiker(this Tickers ticker, PatchTickerDto dto)
        {
            if (dto.Name != null)
                ticker.Name = dto.Name;

            if (dto.Symbol != null)
                ticker.Symbol = dto.Symbol;

            if (dto.Description != null)
                ticker.Description = dto.Description;

            if (dto.Privileged.HasValue)
                ticker.Privileged = dto.Privileged.Value;

            if (dto.ListLevel.HasValue)
                ticker.ListLevel = dto.ListLevel.Value;

            if (dto.LongName != null)
                ticker.LongName = dto.LongName;

            if (dto.DividendsValue.HasValue)
                ticker.DividendsValue = dto.DividendsValue.Value;

            if (dto.DividendsPercents.HasValue)
                ticker.DividendsPercents = dto.DividendsPercents.Value;
        }
    }
}
