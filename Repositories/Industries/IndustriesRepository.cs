using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using StockLens.data;
using StockLens.Dtos.IndustriesDtos;
using StockLens.Mappers;
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

        public async Task CreateIndustriesAsync(IndustiesModel industry)
        {
            var sector = await _db_context.Sectors.FindAsync(industry.SectorId);
            if (sector == null)
                throw new Exception($"Сектор с id {industry.SectorId} не найден");

            await _db_context.AddAsync(industry);
            await _db_context.SaveChangesAsync();
        }

        public async Task<IndustiesModel?> GetIndustryBySectorAsync(int sectorId)
        {
            return await _db_context.Industries.FindAsync(sectorId);
        }

        public async Task<IndustiesModel?> GetIndustryAsync(int industryId)
        {
            return await _db_context.Industries.FirstOrDefaultAsync(i => i.Id == industryId);
            
        }

        public async Task<IndustiesModel>? GetIndustriesWithDependencies(int industryId)
        {
            var industry = await _db_context.Industries.Include(i => i.Tickers)
                                                        .FirstOrDefaultAsync(i => i.Id == industryId);

            return industry; 
        }

        public async Task<IEnumerable<IndustiesModel>?> GetIndustriesBySectorsAsync(List<int> sectorsIds, int start, int size)
        {
            return await _db_context.Industries.Where(i => sectorsIds.Contains(i.SectorId))
                                        .Skip(start)
                                        .Take(size)
                                        .ToListAsync();
        }

        public async Task<IEnumerable<IndustiesModel>?> GetAllIndustriesAsync(int start, int size)
        {
            return await _db_context.Industries.Skip(start)
                                        .Take(size)
                                        .ToListAsync();
        }

        public async Task PatchIndustry(int industryId, PatchIndustryDto dto)
        {
            var ind = await _db_context.Industries.FindAsync(industryId);
            if (ind == null)
                throw new Exception($"Индустрия с id {industryId} не найдена");

            ind.PatchIndustriesFromDto(dto);
            await _db_context.SaveChangesAsync();
        }

        public async Task DeleteIndustryHardAsync(IndustiesModel industry)
        {
            _db_context.Industries.Remove(industry);
            await _db_context.SaveChangesAsync();
        }

    }
}
