using StockLens.Dtos.AuthDtos;
using StockLens.Queries;

namespace StockLens.Services.Analytics.Heatmap
{
    public interface IHeatmapFacade
    {
        public Task<string> GetTickersHeatmap(UsersСharacteristicsDto dto);
    }
}
