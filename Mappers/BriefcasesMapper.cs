using Npgsql.Replication;
using StockLens.Dtos.BriefcasesDtos;
using StockLens.Dtos.BriefcasesTickersDtos;
using StockLens.Models;

namespace StockLens.Mappers
{
    public static class BriefcasesMapper
    {
        public static GetBrifcasesListDto ToBriefcaseListDto(this Briefcases briefcases)
        {
            return new GetBrifcasesListDto
            {
                BriefcasesId = briefcases.BriefcasesId,
                Description = briefcases.Description,
                Name = briefcases.Name,
            };
        }
        public static Briefcases ToBriefcase(this CreateBriefcaseDto dto, string userId)
        {
            return new Briefcases
            {
                Description = dto.Description,
                Name = dto.Name,
                UserId = userId
            };
        }

        public static BriefcasesTickers ToBriefcaseTickers(this CreateBriefcasesTickersDto dto)
        {
            return new BriefcasesTickers
            {
                percantage = dto.percantage,
                Briefcase = dto.Briefcase,
                BriefcaseId = dto.BriefcaseId,
                Ticker = dto.Ticker,
                TickerId = dto.TickerId,
            };
        }

        public static GetBriefcasesDto ToBriefcasesDto(this Briefcases briefcases)
        {
            return new GetBriefcasesDto 
            { 
                Description = briefcases.Description,
                Name = briefcases.Name,
                BriefcasesId = briefcases.BriefcasesId,
                Tickers = briefcases.Tickers.Select(t => t.ToBriefcasesDto()),
            };

        }

        public static void PatchBriefcase(this PatchBriefcaseDto briefcaseDto, Briefcases briefcase)
        {
            if (briefcaseDto.Name != null) 
                briefcase.Name = briefcaseDto.Name;

            if (briefcaseDto.Description != null)
                briefcase.Description = briefcaseDto.Description;
        }
    }
}
