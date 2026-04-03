using Microsoft.Identity.Client;
using StockLens.Dtos.BriefcasesDtos;

namespace StockLens.Services.BriefcasesTickers
{
    public interface IBriefcasesTickersService
    {
        public Task<IEnumerable<GetBrifcasesListDto>> GetBrifcasesListAsync(string userEmail, int start, int size);
        public Task<GetBrifcasesListDto> GetBriefcase(int briefcaseId);
        public Task CreateBriefcase(CreateBriefcaseDto dto);
        public Task DeleteBriefcase(int briefcaseId);
        
    }
}
