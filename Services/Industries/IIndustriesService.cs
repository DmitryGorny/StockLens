using StockLens.Dtos.IndustriesDtos;
using StockLens.Dtos.SectorDtos;
using StockLens.Queries;
using IndustiesModel = StockLens.Models.Industries;

namespace StockLens.Services.Industries
{
    public interface IIndustriesService
    {
        public Task<IReadOnlyList<GetIndustryDto>> BulkCreateIndustriesAsync(List<CreateIndustryDto> dtos);
        public Task<GetIndustryDto> AddIndustriesAsync(CreateIndustryDto dto);
        public Task<IReadOnlyList<GetIndustryDto>> GetIndustriesFilteredAsync(IndustriesQuery query);
        public Task<GetIndustryDto?> GetIndustryAsync(int industryId);
    }
}
