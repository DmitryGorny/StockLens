using Microsoft.AspNetCore.Authorization;
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


        /// <summary>
        /// Создаёт новый город в базе данных.
        /// Доступно только пользователям с ролью Admin.
        /// Возвращает 201 Created при успешном создании, 404 NotFound при ошибке.
        /// </summary>
        /// <param name="dto">DTO с данными создаваемого города.</param>
        /// <returns>201 Created или 404 NotFound.</returns>
        [Authorize(Roles = "Admin")]
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

        /// <summary>
        /// Удаляет город по идентификатору.
        /// Доступно только пользователям с ролью Admin.
        /// Возвращает 204 NoContent при успешном удалении, 404 NotFound при ошибке.
        /// </summary>
        /// <param name="cityId">Идентификатор удаляемого города.</param>
        /// <returns>204 NoContent или 404 NotFound.</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{cityId}")]
        public async Task<IActionResult> DeleteCities(int cityId)
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
