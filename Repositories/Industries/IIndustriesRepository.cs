using Microsoft.AspNetCore.Mvc.ApplicationModels;
using StockLens.Dtos.IndustriesDtos;
using IndustiesModel = StockLens.Models.Industries;

namespace StockLens.Repositories.Industries
{
    public interface IIndustriesRepository
    {
        public Task BulkCreateIndustriesAsync(List<IndustiesModel> sector);
        public Task CreateIndustriesAsync(IndustiesModel sector);
        public Task<IndustiesModel?> GetIndustryBySectorAsync(int sectorId);
        public Task<IndustiesModel?> GetIndustryAsync(int industryId);
        public Task<IndustiesModel>? GetIndustriesWithDependencies(int industryId);
        public Task<IEnumerable<IndustiesModel>?> GetIndustriesBySectorsAsync(List<int> sectorsIds, int start, int size);
        public Task<IEnumerable<IndustiesModel>?> GetAllIndustriesAsync(int start, int size);
    }
}
