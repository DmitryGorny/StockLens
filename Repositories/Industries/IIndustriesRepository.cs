using Microsoft.AspNetCore.Mvc.ApplicationModels;
using IndustiesModel = StockLens.Models.Industries;

namespace StockLens.Repositories.Industries
{
    public interface IIndustriesRepository
    {
        public Task BulkCreateIndustriesAsync(List<IndustiesModel> sector);

        public Task CreateIndustriesAsync(IndustiesModel sector);

        public Task<IndustiesModel?> GetIndustryAsync(int sectorId);

        public Task<IReadOnlyList<IndustiesModel>> GetIndustriesFilteredAsync(int industryId);
    }
}
