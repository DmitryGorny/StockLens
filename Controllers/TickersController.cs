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

        [HttpGet("{TickerId}")]
        public async Task<ActionResult<GetTickersDto>> GetTicker(int TickerId)
        {
            try
            {
                return await _tickersService.GetTickerByIdAsync(TickerId);
            } catch (NullReferenceException ex)
            {
                return NotFound("Тикера с таким id не было найдено");
            }
        }

          
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
