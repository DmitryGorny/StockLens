using StockLens.Dtos.SectorDtos;
using StockLens.Queries;

namespace StockLens.Services.Sector
{
    public interface ISectorService
    {
        public Task CreateSectorsBulkAsync(List<CreateSectorDto> dtos);
        public Task<GetSectorDto> CreateSectorAsync(CreateSectorDto dto);
        public Task<GetSectorDto?> GetSectorAsync(int sectorId);
        public Task<string> GetSectorAnalyticsData(SectorQuery query);
    }
}
