using StockLens.Dtos.AuthDtos;
using StockLens.Queries;

namespace StockLens.Services.Analytics.TopTen
{
    public interface ITopTenFacade
    {
        public Task<string> GetTickersTopTen(UsersСharacteristicsDto CharDto);
    }
}
