using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using StockLens.Repositories.Cities;
using StockLens.Services.FileReaderFacade;
using StockLens.Services.Industries;
using StockLens.Services.Sector;
using StockLens.Services.Tickers;

namespace StockLens.Controllers
{
    [Route("api/read_json")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class JsonReaderController : ControllerBase
    {
        private readonly ITickersService _TickersService;
        private readonly IIndustriesService _IndustriesService;
        private readonly ISectorService _SectorsService;
        private readonly ICitiesRepositroy _citiesRepo;
        private readonly IDataBaseFillingFacade _fileReaderFacade;
        
        public JsonReaderController(ITickersService tickersService, IIndustriesService industriesService, ISectorService sectorsService, ICitiesRepositroy citiesRepo, IDataBaseFillingFacade fileReaderFacade)
        {
            _TickersService = tickersService;
            _IndustriesService = industriesService;
            _SectorsService = sectorsService;
            _citiesRepo = citiesRepo;
            _fileReaderFacade = fileReaderFacade;
        }

        /// <summary>
        /// Заполняет/обновляет базу данных по данному JSON файлу
        /// </summary>
        /// <response code="201">
        /// Заполнение/обновление прошло успешно
        /// </response>
        /// 
        [HttpPost]
        public async Task<IActionResult> ReadJsonFile(IFormFile json_file)
        {
            try
            {
                await _fileReaderFacade.ReadJsonFile(json_file);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
