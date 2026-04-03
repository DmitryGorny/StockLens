using StockLens.Dtos.TickersDto;
using StockLens.Mappers;
using StockLens.Repositories.Tickers;
using StockLens.Services.Tickers.Filters.Facade;
using TickersModel = StockLens.Models.Tickers;

namespace StockLens.Services.Tickers
{
    public class TickersService : ITickersService
    {
        private readonly ITickersRepository _tickersRepository;
        private readonly IFilterFacade _filter;

        public TickersService(ITickersRepository tickersRepository, IFilterFacade filterFacade)
        {
            _tickersRepository = tickersRepository;
            _filter = filterFacade;
        }

        public async Task<List<GetTickersDto>> BulkCreateTickersAsync(List<CreateTickersDto> dtos)
        {
            List<TickersModel> tickers = dtos.Select(d => d.CreateTickerFromDto()).ToList();
            await _tickersRepository.BulkCreateTickersAsync(tickers);
            return tickers.Select(t => t.CreateDtoFromTickers()).ToList();
        }
        public async Task AddTikersAsync(CreateTickersDto dto)
        {
            await _tickersRepository.AddTickerAsync(dto.CreateTickerFromDto());
        }
        public async Task<GetTickersDto?> GetTickerAsync(int industryId)
        {
            var ticker = await _tickersRepository.GetTicker(industryId);
            return ticker.CreateDtoFromTickers();
        }

        public async Task<GetTickersDto> GetTickerByIdAsync(int TickerId)
        {
            var ticker = await _tickersRepository.GetTicker(TickerId);
            if (ticker == null) 
                throw new NullReferenceException();
            return ticker!.CreateDtoFromTickers();
        }

        public async Task<IEnumerable<GetTickersDto?>> GetTickersByIndustriesAsync(IEnumerable<int> industriesId, int start, int size)
        {
            var inds = await _tickersRepository.GetTickersByIndustriesAsync(industriesId, start, size);
            if (inds == null)
                throw new NullReferenceException($"{nameof(inds)}");

            return inds.Select(t => t.CreateDtoFromTickers());

        }

        public async Task<IEnumerable<GetTickersDto>> GetTickersByCitiesAsync(IEnumerable<int> citiesId, int start, int size)
        {
            var tickers = await _tickersRepository.GetTickersByCitiesAsync(citiesId, start, size);
            if (tickers == null)
                throw new Exception($"Тикеры для этих городов {string.Join(", ", citiesId)} не были найдены");

            return tickers.Select(t => t.CreateDtoFromTickers());
        }

        public async Task<IEnumerable<GetTickersDto>> GetTickersAsync(int start, int size)
        {
            var tickers = await _tickersRepository.GetTickersAsync(start, size);
            return tickers.Select(t => t.CreateDtoFromTickers());
        }

        public async Task<IEnumerable<GetTickersDto>> GetTickersAsync()
        {
            return (await _tickersRepository.GetTickersAsync()).Select(t => t.CreateDtoFromTickers());
        }

        public async Task CreateTicker(CreateTickersDto dto)
        {
            if (dto == null) throw new Exception("Данные неккоретны");
            await _tickersRepository.CreateTicker(dto.CreateTickerFromDto());
        }

        public async Task PatchTickerAsync(int TickerId, PatchTickerDto dto)
        {
            await _tickersRepository.PatchTicker(TickerId, dto);
        }

        public async Task DeleteTickerAsync(int TickerId)
        {
            var ticker = await  _tickersRepository.GetTicker(TickerId);
            if (ticker == null) throw new NullReferenceException($"Тикер с id {TickerId} не найден");
            await _tickersRepository.DeleteTickerHardAsync(ticker);
        }

        public async Task<IEnumerable<GetTickersDto>> LayeredFiltration(FiltrationDto dto)
        {
            var allTickers = await GetTickersAsync();

            allTickers = await _filter.Filter(allTickers, dto);    
            
            return allTickers;
            
        }
    }

}
