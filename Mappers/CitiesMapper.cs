using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockLens.Dtos.CitiesDtos;
using StockLens.Dtos.IndustriesDtos;
using StockLens.Models;

namespace StockLens.Mappers
{
    public static class CitiesMapper
    {
        public static Cities ToCitiesFromDto(this CreateCitiesDtos dto)
        {
            return new Cities
            {
                Name = dto.Name,
            };
        }

        public static GetCitiesDtos ToDtoFromCities(this Cities city)
        {
            return new GetCitiesDtos
            {
                Id = city.Id,
                Name = city.Name,
            };
        }
    }
}
