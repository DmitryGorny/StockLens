using StockLens.Dtos.AuthDtos;

namespace StockLens.Services.Analytics.Heatmap
{
    public interface IHeatmapFacade
    {
        public Task<string> GetTickersHeatmap(UsersСharacteristicsDto dto);
    }
}
