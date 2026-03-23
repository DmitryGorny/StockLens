using StockLens.Dtos.AuthDtos;

namespace StockLens.Services.Analytics.TopTen
{
    public interface ITopTenFacade
    {
        public Task<string> GetTickersTopTen(UsersСharacteristicsDto CharDto);
    }
}
