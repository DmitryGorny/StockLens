using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockLens.Dtos.CitiesDtos;
using StockLens.Services.Cities;

namespace StockLens.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;
        public CitiesController(ICityService cityService) {
            _cityService = cityService;
        }

        /// <summary>
        /// Выгружает данные по одному городу , который соответствует переданному id. Если тикера с города id не существует, то возвращает 404 ошибку
        /// </summary>
        /// <param name="cityId">Id нужного города</param> 
        /// 
        [HttpGet("{cityId}")]
        public async Task<ActionResult<GetCitiesDto>> GetCity(int cityId)
        {
            try
            {
                return await _cityService.GetCity(cityId);
            } catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Выгружает данные всем городам
        /// </summary>
        /// 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCitiesDto>>> GetAllCities()
        {
            try
            {
                return Ok(await _cityService.GetAllCities());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("create-city")]
        public async Task<IActionResult> CreateCity(CreateCitiesDtos dto)
        {
            try
            {
                await _cityService.CreateCity(dto);
                return Created();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{cityId}")]
        public async Task<IActionResult> GetAllCities(int cityId)
        {
            try
            {
                await _cityService.DeleteCity(cityId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
     }

}
