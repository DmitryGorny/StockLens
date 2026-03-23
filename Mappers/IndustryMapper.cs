using StockLens.Dtos.IndustriesDtos;
using StockLens.Models;

namespace StockLens.Mappers
{
    public static class IndustryMapper
    {
        public static Industries ToIndustriesFromDto(this CreateIndustryDto dto)
        {
            return new Industries 
            {
                Description = dto.Description,
                Name = dto.Name,
                SectorId = dto.SectorId
            };
        }

        public static GetIndustryDto ToDtoFromIndustries(this Industries industry)
        {
            return new GetIndustryDto
            {
                Id = industry.Id,
                Description = industry.Description,
                Name = industry.Name,
                SectorId = industry.SectorId,
            };
        }

        public static void PatchIndustriesFromDto(this Industries industry, PatchIndustryDto dto)
        {
            if (dto.Description != null)
                industry.Description = dto.Description;
            if (dto.Name != null)
                industry.Name = dto.Name;
        }
    }
}
