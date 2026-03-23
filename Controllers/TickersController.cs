using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockLens.Dtos.TickersDto;
using StockLens.Models;
using StockLens.Services.Tickers;

namespace StockLens.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TickersController : ControllerBase
    {
        private readonly ITickersService _tickersService;

        public TickersController(ITickersService tickersService)
        {
            _tickersService = tickersService;
        }

        /// <summary>
        /// Выгружает данные по одному тикеру , который соответствует переданному id. Если тикера с таким id не существует, то возвращает 404 ошибку
        /// </summary>
        /// <param name="TickerId">Id нужного тикера</param> 
        /// 
        [HttpGet("{TickerId}")]
        public async Task<ActionResult<GetTickersDto>> GetTicker(int TickerId)
        {
            try
            {
                return await _tickersService.GetTickerByIdAsync(TickerId);
            } catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        ///Выгружает данные по всем тикерам с пагинацией. Если тикеров нет, то возвращает 404 ошибку
        /// </summary>
        /// <param name="start">Индекс начала выборки</param> 
        /// <param name="size">Количество тикеров в запросе</param> 
        [HttpGet]
        [Route("all-tickers")]
        public async Task<ActionResult<GetTickersDto>> GetAllTickers(int start, int size)
        {
            try
            {
                return Ok(await _tickersService.GetTickersAsync(start, size));
            }
            catch (NullReferenceException ex)
            {
                return NotFound();
            }
        }

        /// <summary>
        ///Выгружает данные по всем тикерам, которые принадлежат к городам с переданными id, с пагинацией. Если тикеров нет, то возвращает 404 ошибку
        /// </summary>
        /// <param name="CitiesId">Список с id идустрий</param> 
        /// <param name="start">Индекс начала выборки</param> 
        /// <param name="size">Количество тикеров в запросе</param> 
        [HttpGet]
        [Route("by-cities-id")]
        public async Task<ActionResult<IEnumerable<GetTickersDto>>> GetTickersByCities([FromQuery] List<int> CitiesId, int start, int size)
        {
            try
            {
                return Ok(await _tickersService.GetTickersByCitiesAsync(CitiesId, start: start, size: size));
            }
            catch (NullReferenceException ex)
            {
                return NotFound();
            }
        }

        /// <summary>
        ///Выгружает данные по всем тикерам, которые принадлежат к индустриям с переданными id, с пагинацией. Если тикеров нет, то возвращает 404 ошибку
        /// </summary>
        /// <param name="IndustriesId">Список с id идустрий</param> 
        /// <param name="start">Индекс начала выборки</param> 
        /// <param name="size">Количество тикеров в запросе</param> 
        [HttpGet]
        [Route("by-industries-ids")]
        public async Task<ActionResult<IEnumerable<GetTickersDto>>> GetTickersByIndustries([FromQuery] List<int> IndustriesId, int start, int size)
        {
            try
            {
                return Ok(await _tickersService.GetTickersByIndustriesAsync(IndustriesId, start: start, size: size));
            }
            catch (NullReferenceException ex)
            {
                return NotFound();
            }
        }
    } 
}
