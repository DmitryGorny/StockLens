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
    }

}
