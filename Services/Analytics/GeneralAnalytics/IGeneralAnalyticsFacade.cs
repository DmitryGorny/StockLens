using StockLens.Models;
using StockLens.Queries;

namespace StockLens.Services.Analytics.GeneralAnalytics
{
    public interface IGeneralAnalyticsFacade
    {
        public Task<string> GetSectorsGeneralAnalytics(int sectorId);
        public Task<string> GetIndustriesGeneralAnalytics(int IndustryId);
        public Task<string> GetTickersGeneralAnalytics(int TickerId);
    }
}
