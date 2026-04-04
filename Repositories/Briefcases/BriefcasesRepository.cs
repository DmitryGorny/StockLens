using Microsoft.EntityFrameworkCore;
using StockLens.data;
using StockLens.Dtos.BriefcasesDtos;
using StockLens.Mappers;

namespace StockLens.Repositories.Briefcases
{
    public class BriefcasesRepository : IBriefcasesRepository
    {
        private readonly AppDBContext _db_context;
        public BriefcasesRepository(AppDBContext db_context)
        { 
            _db_context = db_context;
        }
        public async Task<Models.Briefcases?> GetBriefcaseAsync(int briefcaseId)
        {
            return await _db_context.Briefcases
                                    .Where(b => b.BriefcasesId == briefcaseId)
                                    .Include(b => b.Tickers)
                                    .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Models.Briefcases>> GetUsersBriefcasesAsync(string userId, int start, int size)
        {
            return await _db_context.Briefcases.Where(b => b.UserId.Equals(userId))
                .OrderBy(b => b.Name)
                .Skip(start)
                .Take(size)
                .ToListAsync();
        }
        public async Task CreateBriefcase(Models.Briefcases dto)
        {
            _db_context.Briefcases.Add(dto);
            await _db_context.SaveChangesAsync();
        }

        public async Task PatchBriefcase(Models.Briefcases briefcase, PatchBriefcaseDto patchBriefcaseDto)
        {
            patchBriefcaseDto.PatchBriefcase(briefcase);
            await _db_context.SaveChangesAsync();
        }
        public async Task DeleteBriefcase(Models.Briefcases briefcase)
        {
            _db_context.Briefcases.Remove(briefcase);
            await _db_context.SaveChangesAsync();
        }
    }
}
