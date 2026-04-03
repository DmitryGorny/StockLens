using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using StockLens.data;
using StockLens.Dtos.BriefcasesTickersDtos;
using StockLens.Mappers;

namespace StockLens.Repositories.BriefcasesTickers
{
    public class BriefcasesTickersRepository : IBriefcasesTickersRepository
    {
        private readonly AppDBContext _dbContext;
        public BriefcasesTickersRepository(AppDBContext dBContext) 
        {
            _dbContext = dBContext;
        }

        public async Task CreateBrifcasesTickers(Models.BriefcasesTickers bct)
        {
            _dbContext.BriefcasesTickers.Add(bct);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteBriefcasesTickers(int tickerId, int briefcaseId)
        {
            await _dbContext.BriefcasesTickers
                .Where(bct => bct.TickerId == tickerId && bct.BriefcaseId == briefcaseId)
                .FirstOrDefaultAsync();
        }
        public async Task CreateBriefcaseBulk(IEnumerable<CreateBriefcasesTickersDto> dto)
        {
            var models = dto.Select(d => d.ToBriefcaseTickers());
            await _dbContext.BulkInsertAsync(dto);
            await _dbContext.BulkSaveChangesAsync();
        }
    }
}
