using StockLens.Queries;

namespace StockLens.Services.Analytics.GeneralAnalytics
{
    public interface IGeneralAnalyticsFacade
    {
        public Task<string> GetSectorsGeneralAnalytics(SectorQuery query);
        public Task<string> GetIndustriesGeneralAnalytics(IndustriesQuery query);
        public Task<string> GetTickersGeneralAnalytics(TickersQuery query);
    }
}
