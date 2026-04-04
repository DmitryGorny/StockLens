using StockLens.Dtos.BriefcasesDtos;

namespace StockLens.Repositories.Briefcases
{
    public interface IBriefcasesRepository
    {
        public Task<Models.Briefcases?> GetBriefcaseAsync(int briefcaseId);
        public Task<IEnumerable<Models.Briefcases>> GetUsersBriefcasesAsync(string userId, int start, int size);
        public Task CreateBriefcase(Models.Briefcases dto);
        public Task PatchBriefcase(Models.Briefcases briefcase, PatchBriefcaseDto patchBriefcaseDto);
        public Task DeleteBriefcase(Models.Briefcases briefcase);
    }
}
