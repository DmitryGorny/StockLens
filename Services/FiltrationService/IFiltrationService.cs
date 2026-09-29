using StockLens.Dtos.TickersDto;

namespace StockLens.Services.FiltrationService
{
    public interface IFiltrationService
    {
        public Task<IEnumerable<GetTickersDto>> Filter(FiltrationDto dto);
    }
}
