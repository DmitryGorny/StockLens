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
        public async Task BulkCreateIndustriesAsync(List<IndustiesModel> industries)
        {
            var config = new BulkConfig
            {
                SetOutputIdentity = true,
                PreserveInsertOrder = true,
                UpdateByProperties = new()
                {
                   nameof(IndustiesModel.Name),
                }
            };
            await _db_context.BulkInsertOrUpdateAsync(industries, config);
            await _db_context.SaveChangesAsync();
        }

        public async Task CreateIndustriesAsync(IndustiesModel sector)
        {
            await _db_context.AddAsync(sector);
            await _db_context.SaveChangesAsync();
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
