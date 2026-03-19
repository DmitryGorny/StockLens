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
        public Task<IEnumerable<GetIndustryDto>> GetIndustriesBySectorAsync(List<int> sectorsIds, int start, int size);
        public Task<GetIndustryDto> GetIndustryAsync(int industryId);
        public Task<IEnumerable<GetIndustryDto>> GetAllIndustriesAsync(int start, int size);
    }
}
