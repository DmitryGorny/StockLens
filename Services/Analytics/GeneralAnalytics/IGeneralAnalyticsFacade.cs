using StockLens.Dtos.AuthDtos;
using StockLens.Models;
using StockLens.Queries;

namespace StockLens.Services.Analytics.GeneralAnalytics
{
    public interface IGeneralAnalyticsFacade
    {
        public Task<string> GetSectorsGeneralAnalytics(int sectorId, int daysNumber, UsersСharacteristicsDto dto);
        public Task<string> GetIndustriesGeneralAnalytics(int IndustryId, int daysNumber, UsersСharacteristicsDto dto);
        public Task<string> GetTickersGeneralAnalytics(int TickerId, int daysNumber, UsersСharacteristicsDto dto);
        public Task<string> GetCityGeneralAnalytics(int CityId, int daysNumber, UsersСharacteristicsDto dto);
    }
}
