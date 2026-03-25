using StockLens.data;
using StockLens.Dtos.CitiesDtos;
using StockLens.Dtos.IndustriesDtos;
using StockLens.Dtos.SectorDtos;
using StockLens.Dtos.TickersDto;
using StockLens.Mappers;
using StockLens.Repositories.Cities;
using StockLens.Services.Industries;
using StockLens.Services.Moex;
using StockLens.Services.QuotesService;
using StockLens.Services.Sector;
using StockLens.Services.Tickers;
using System.Runtime.CompilerServices;
using System.Text.Json;


namespace StockLens.Services.FileReaderFacade
{
    public class DataBaseFillingFacade : IDataBaseFillingFacade
    {
        private readonly ISectorService _sectorsService;
        private readonly IIndustriesService _industriesService;
        private readonly ITickersService _tickersService;
        private readonly ICitiesRepositroy _citiesRepo;
        private readonly IMoexService _moexService;
        private readonly IQuotesService _quotesService;

        private readonly AppDBContext _db_context;
        public DataBaseFillingFacade(ISectorService sectorService,
                                IIndustriesService industriesService, 
                                ITickersService tickersService,
                                ICitiesRepositroy citiesRepo,
                                IQuotesService quotesService,
                                IMoexService moexService,
                                AppDBContext context) 
        {
            _sectorsService = sectorService;
            _industriesService = industriesService;
            _tickersService = tickersService;
            _citiesRepo = citiesRepo;
            _db_context = context;
            _moexService = moexService;
            _quotesService = quotesService;
        }

        public async Task ReadJsonFile(IFormFile jsonFile)
        {
            using var stream = jsonFile.OpenReadStream();
            {
                await using var transaction = await _db_context.Database.BeginTransactionAsync();
                try
                {
                    var root = JsonSerializer.Deserialize<Root>(stream);
                    foreach (var sector in root.sectors)
                    {
                        var sectorDto = await CreateSectorDB(sector);
                        var indsDtos = await CreateIndustriesDB(sector.Industries, sectorDto.Id);
                        var indsMap = indsDtos.ToDictionary(x => x.Name);
                        foreach (var ind in sector.Industries)
                        {
                            var tickers_dto = await CreateTickers(ind.Tickers, indsMap[ind.Name].Id);
                            foreach(var ticker in tickers_dto)
                            {
                               var quotes_dtos = await _moexService.RequestQuotesByYears(ticker.Symbol, ticker.Id, 5);
                               await _quotesService.CreateQuotesBulk(quotes_dtos);
                            }
                        }
                    }

                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }

            }
        }

        private async Task<GetSectorDto> CreateSectorDB(Sector sector)
        {
            var dto = new CreateSectorDto
            {
                Name = sector.Name,
                Description = sector.Description,
            };

            return await _sectorsService.CreateSectorAsync(dto);
        }

        private async Task<IReadOnlyList<GetIndustryDto>> CreateIndustriesDB(List<Industry> Inds, int sectorid) 
        {
            var results = Inds.Select(i => new CreateIndustryDto
                {
                    Name = i.Name,
                    Description = i.Description,
                    SectorId = sectorid
                }
            ).ToList();

            IReadOnlyList<GetIndustryDto> dtos = await _industriesService.BulkCreateIndustriesAsync(results);

            return dtos;
        }

        private async Task<List<GetTickersDto>> CreateTickers(List<Ticker> tics, int industryId)
        {
            List<CreateTickersDto> Tickers = new List<CreateTickersDto>();
            foreach (var t in tics) 
            {
                var city_dto = await CreateCity(t.City);
                int? listLevel = await _moexService.RequesTickersListLevel(t.Symbol);
                if (listLevel == null)
                    listLevel = 0;
                var ticker_dto = new CreateTickersDto
                {
                    IndustryId = industryId,
                    CityId = city_dto.Id,
                    Name = t.Name,
                    Symbol = t.Symbol,
                    ListLevel = listLevel!.Value,
                    LongName = t.LongName,
                    Description = t.Description,
                    DividendsPercents = t.DividendsPercents,
                    DividendsValue = t.DividendsValue,
                    Privileged = t.Privileged,
                };
                Tickers.Add(ticker_dto);
            }
            return await _tickersService.BulkCreateTickersAsync(Tickers);
        }

        private async Task<GetCitiesDto> CreateCity(City city)
        {
            var dto = new CreateCitiesDtos
            {
                Name = city.Name,
            };

            var cityEnt = await _citiesRepo.AddCityAsync(dto.ToCitiesFromDto());
            return cityEnt.ToDtoFromCities();
        }

        private class Root
        {
            public List<Sector> sectors { get; set; } = new();
        }
        private class Sector
        {
            public string Name { get; set; }
            public string Description { get; set; }

            public List<Industry> Industries { get; set; } = new();

        }

        private class Industry
        {
            public string Name { get; set; }
            public string Description { get; set; }

            public List<Ticker> Tickers { get; set; } = new();
        }

        private class Ticker
        {
            public string Name { get; set; }
            public string Symbol { get; set; }
            public string Description { get; set; }
            public bool Privileged { get; set; }
            public string LongName { get; set; }
            public int ListLevel { get; set; }
            public decimal DividendsValue { get; set; }
            public decimal DividendsPercents { get; set; }
            public City City { get; set; }
        }

        private class City
        {
            public string Name { get; set; }
        }
    }
}
