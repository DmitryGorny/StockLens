using StockLens.Dtos.SectorDtos;

namespace StockLens.Services.Sector
{
    public interface ISectorService
    {
        public Task CreateSectorsBulkAsync(List<CreateSectorDto> dtos);
        public Task<GetSectorDto> CreateSectorAsync(CreateSectorDto dto);
        public Task<GetSectorDto> GetSectorAsync(int sectorId);
        public Task<IEnumerable<GetSectorDto>> GetAllSectorsAsync(int start, int size);
        public Task PatchSector(int sectorId, PatchSectorDto dto);
        public Task DeleteSectorHard(int sectorId);
    }
}
