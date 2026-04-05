using StockLens.Dtos.BriefcasesDtos;
using StockLens.Dtos.BriefcasesTickersDtos;
using StockLens.Models;

namespace StockLens.Repositories.BriefcasesTickers
{
    public interface IBriefcasesTickersRepository
    {
        public Task CreateBrifcasesTickers(Models.BriefcasesTickers bct);
        public Task DeleteBriefcasesTickers(Models.BriefcasesTickers bct);
        public Task CreateBriefcaseBulk(IEnumerable<CreateBriefcasesTickersDto> dto);
        public Task<List<KeyValuePair<int, decimal>>> PatchBriefcasesTickers(int briefcaseId, PatchBriefcasesTickersDto dto);
    }
}
