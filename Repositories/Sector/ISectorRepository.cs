using SectorModel = StockLens.Models.Sectors;

namespace StockLens.Repositories.Sector
{
    public interface ISectorRepository
    {
        public Task BulkCreateSectorsAsync(List<SectorModel> sector);

        public Task<SectorModel> CreateSectorAsync(SectorModel sector);

        public Task<SectorModel?> GetSectorAsync(int sectorId);
        public Task<SectorModel?> GetSectorAsync(int sectorId, int quotesNumber);
    }
}
