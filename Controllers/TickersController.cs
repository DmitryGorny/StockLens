using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockLens.Dtos.TickersDto;
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("create-ticker")]
        public async Task<IActionResult> CreateTickers([FromBody] CreateTickersDto dto)
        {
            try
            {
                await _tickersService.CreateTicker(dto);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{TickerId}")]
        public async Task<IActionResult> PatchTickers(int TickerId, [FromBody] PatchTickerDto dto)
        {
            try
            {
                await _tickersService.PatchTickerAsync(TickerId, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{TickerId}")]
        public async Task<IActionResult> DeleteTicker(int TickerId)
        {
            try
            {
                await _tickersService.DeleteTickerAsync(TickerId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("layered-filtration")]
        public async Task<ActionResult<IEnumerable<GetTickersDto>>> LayeredFiltration([FromQuery] FiltrationDto dto)
        {
            try
            {
                return Ok(await _tickersService.LayeredFiltration(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    } 
}
