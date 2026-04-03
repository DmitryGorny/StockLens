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
        public static Briefcases ToBriefcase(this CreateBriefcaseDto dto)
        {
            return new Briefcases
            {
                Description = dto.Description,
                Name = dto.Name,
                UserId = dto.UserId,
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
    }
}
