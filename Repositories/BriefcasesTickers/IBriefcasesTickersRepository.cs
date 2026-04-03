using StockLens.Dtos.BriefcasesDtos;
using StockLens.Dtos.BriefcasesTickersDtos;
using StockLens.Models;

namespace StockLens.Repositories.BriefcasesTickers
{
    public interface IBriefcasesTickersRepository
    {
        public Task CreateBrifcasesTickers(Models.BriefcasesTickers bct);
        public Task DeleteBriefcasesTickers(int tickerId, int  briefcaseId);
        public Task CreateBriefcaseBulk(IEnumerable<CreateBriefcasesTickersDto> dto);
    }
}
