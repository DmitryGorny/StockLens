using SectorModel = StockLens.Models.Sectors;

namespace StockLens.Repositories.Sector
{
    public interface ISectorRepository
    {
        public Task BulkCreateSectorsAsync(List<SectorModel> sector);

        public Task AddSectorAsync(SectorModel sector);

        public Task<SectorModel?> GetSectorAsync(int sectorId);
    }
}
