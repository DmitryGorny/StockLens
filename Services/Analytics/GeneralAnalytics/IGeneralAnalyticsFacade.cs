using StockLens.Dtos.AuthDtos;
using StockLens.Models;
using StockLens.Queries;

namespace StockLens.Services.Analytics.GeneralAnalytics
{
    public interface IGeneralAnalyticsFacade
    {
        public Task<string> GetSectorsGeneralAnalytics(int sectorId, UsersСharacteristicsDto dto);
        public Task<string> GetIndustriesGeneralAnalytics(int IndustryId, UsersСharacteristicsDto dto);
        public Task<string> GetTickersGeneralAnalytics(int TickerId, UsersСharacteristicsDto dto);
    }
}
