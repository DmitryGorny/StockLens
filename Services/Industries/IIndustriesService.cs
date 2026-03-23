using StockLens.Dtos.IndustriesDtos;
using StockLens.Dtos.SectorDtos;
using IndustiesModel = StockLens.Models.Industries;

namespace StockLens.Services.Industries
{
    public interface IIndustriesService
    {
        public Task<IReadOnlyList<GetIndustryDto>> BulkCreateIndustriesAsync(List<CreateIndustryDto> dtos);
        public Task<GetIndustryDto> AddIndustriesAsync(CreateIndustryDto dto);
        public Task<IEnumerable<GetIndustryDto>> GetIndustriesBySectorAsync(List<int> sectorsIds, int start, int size);
        public Task<GetIndustryDto> GetIndustryAsync(int industryId);
        public Task<IEnumerable<GetIndustryDto>> GetAllIndustriesAsync(int start, int size);
        public Task CreateIndustry(CreateIndustryDto dto);
        public Task PatchIndustry(int IndustryId, PatchIndustryDto dto);
        public Task DeleteIndustry(int IndustryId);
    }
}
