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

        public static void PatchSectorFromDto(this Sectors sector, PatchSectorDto dto)
        {
            if (dto.Name != null)
                sector.Name = dto.Name;
            if (dto.Description != null)
                sector.Description = dto.Description;
        }
    }
}
