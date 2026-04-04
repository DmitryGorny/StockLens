using Microsoft.Identity.Client;
using StockLens.Dtos.BriefcasesDtos;
using StockLens.Dtos.BriefcasesTickersDtos;
using StockLens.Dtos.TickersDto;

namespace StockLens.Services.BriefcasesTickers
{
    public interface IBriefcasesService
    {
        public Task<IEnumerable<GetBrifcasesListDto>> GetBrifcasesListAsync(string userEmail, int start, int size);
        public Task<GetBriefcasesDto> GetBriefcase(int briefcaseId);
        public Task CreateBriefcase(string userEmail, CreateBriefcaseDto dto);

        public Task PatchBriefcasesTickers(int briefcaseId, PatchBriefcaseDto patchDto);
        public Task DeleteBriefcase(int briefcaseId);
        
    }
}
