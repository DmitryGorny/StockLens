using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using StockLens.data;
using StockLens.Dtos.SectorDtos;
using StockLens.Mappers;
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

        public async Task<SectorModel> CreateSectorAsync(SectorModel sector)
        {
            var alreadyExist = await _db_context.Sectors.FirstOrDefaultAsync(s => s.Name == sector.Name);
            if (alreadyExist != null)
            {
                alreadyExist.Description = sector.Description;
                return alreadyExist;
            }
            await _db_context.Sectors.AddAsync(sector);
            await _db_context.SaveChangesAsync();
            return sector;
        }

        public async Task<SectorModel?> GetSectorAsync(int sectorId)
        {
            return await _db_context.Sectors.FindAsync(sectorId);
        }

        public async Task<SectorModel?> GetSectorAsync(int sectorId, int quotesNumber)
        {
            var sector = await _db_context.Sectors.Include(s => s.Industries)
                                            .ThenInclude(i => i.Tickers)
                                            .FirstOrDefaultAsync(s => s.Id == sectorId);
            return sector;

        }
        public async Task<IEnumerable<SectorModel>> GetAllSectorsAsync(int start, int size)
        {
            return await _db_context.Sectors.Skip(start).Take(size).ToListAsync();
        }

        public async Task PatchSectorAsync(int sectorId, PatchSectorDto sector)
        {
            var sec = await _db_context.Sectors.FindAsync(sectorId);

            if (sec == null)
                throw new Exception($"Сектор с id {sectorId} не найден");

            sec.PatchSectorFromDto(sector);
            await _db_context.SaveChangesAsync();

        }

        public async Task DeleteSectorHardAsync(int sectorId)
        {
            var sec = await _db_context.Sectors.FindAsync(sectorId);
            if (sec == null)
                throw new Exception($"Сектор с id {sectorId} не найден");

            _db_context.Sectors.Remove(sec);
            await _db_context.SaveChangesAsync();
        }
    }
}
