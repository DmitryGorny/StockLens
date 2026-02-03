using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using StockLens.data;
using SectorModel = StockLens.Models.Sectors;

namespace StockLens.Repositories.Sector
{
    public class SectorRepository : ISectorRepository
    {
        private readonly AppDBContext _db_context;

        public SectorRepository(AppDBContext context)
        {
            _db_context = context;
        }

        public async Task BulkCreateSectorsAsync(List<SectorModel> sector)
        {
            await _db_context.BulkInsertAsync(sector);
        }

        public async Task AddSectorAsync(SectorModel sector)
        {
            await _db_context.Sectors.AddAsync(sector);
        }

        public async Task<SectorModel?> GetSectorAsync(int sectorId)
        {
            return await _db_context.Sectors.FindAsync(sectorId);
        }

        public async Task<IReadOnlyList<SectorModel>> GetSectorPaginatedAsync(int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;
            return await _db_context.Sectors.Skip(skip).Take(pageSize).ToListAsync();
        }
    }
}
