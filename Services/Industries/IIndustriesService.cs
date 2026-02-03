using StockLens.Dtos.IndustriesDtos;
using StockLens.Dtos.SectorDtos;
using StockLens.Queries;
using IndustiesModel = StockLens.Models.Industries;

namespace StockLens.Services.Industries
{
    public interface IIndustriesService
    {
        public Task BulkCreateIndustriesAsync(List<CreateIndustryDto> dtos);
        public Task AddIndustriesAsync(CreateIndustryDto dto);

        public Task<IReadOnlyList<GetIndustryDto>> GetIndustriesFilteredAsync(int sectorId);

        public Task<GetIndustryDto?> GetIndustryAsync(int industryId);
    }
}
