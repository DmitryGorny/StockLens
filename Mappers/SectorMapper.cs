using StockLens.Dtos.SectorDtos;
using StockLens.Models;
using System.Runtime.CompilerServices;

namespace StockLens.Mappers
{
    public static class SectorMapper
    {
        public static GetSectorDto CreateDtoFromSectors(this Sectors sector)
        {
            return new GetSectorDto
            {
                Id = sector.Id,
                Name = sector.Name,
                Description = sector.Description,
                IndustriesNames = sector.Industries.Select(x => x.Name).ToList(),
            };
        }

        public static Sectors CreateSectorFromDto(this CreateSectorDto dto)
        {
            return new Sectors
            {
                Name = dto.Name,
                Description = dto.Description
            };
        }
    }
}
