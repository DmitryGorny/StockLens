using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using StockLens.data;
using IndustiesModel = StockLens.Models.Industries;

namespace StockLens.Repositories.Industries
{
    public class IndustriesRepository: IIndustriesRepository
    {
        private readonly AppDBContext _db_context;
        
        public IndustriesRepository(AppDBContext db_context)
        {
            _db_context = db_context;
        }
        public async Task BulkCreateIndustriesAsync(List<IndustiesModel> sector)
        {
            await _db_context.BulkInsertAsync(sector);
        }

        public async Task AddIndustriesAsync(IndustiesModel sector)
        {
            await _db_context.AddAsync(sector);
        }

        public async Task<IndustiesModel?> GetIndustryAsync(int sectorId)
        {
            return await _db_context.Industries.FindAsync(sectorId);
        }

        public async Task<IReadOnlyList<IndustiesModel>> GetIndustriesFilteredAsync(int industryId)
        {
            return await _db_context.Industries.Take(industryId).ToListAsync();
        }
    }
}
