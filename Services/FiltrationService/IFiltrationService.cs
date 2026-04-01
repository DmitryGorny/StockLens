using StockLens.Dtos.TickersDto;

namespace StockLens.Services.FiltrationService
{
    public interface IFiltrationService
    {
        public Task<IEnumerable<GetTickersDto>> LayeredFiltration(FiltrationDto dto);
    }
}
