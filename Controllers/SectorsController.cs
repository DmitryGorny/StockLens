using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using StockLens.Dtos.SectorDtos;
using StockLens.Services.Sector;

namespace StockLens.Controllers
{
    [Route("api/sectors")]
    [ApiController]
    public class SectorsController : ControllerBase
    {
        private readonly ISectorService _sectorService;

        public SectorsController(ISectorService sectorService)
        {
            _sectorService = sectorService;
        }

        /// <summary>
        ///Выгружает данные по всем секторам с пагинацией. Если секторов нет, то возвращает 404 ошибку
        /// </summary>
        /// <param name="start">Индекс начала выборки</param> 
        /// <param name="size">Количество тикеров в запросе</param>
        [HttpGet]
        public async Task<IActionResult> GetAllSectors(int start, int size)
        {
            try
            {
                return Ok(await _sectorService.GetAllSectorsAsync(start, size));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Выгружает данные по одной индустрии, которая соответствует переданному id. Если индустрии с таким id не существует, то возвращает 404 ошибку
        /// </summary>
        /// <param name="sectorId">Id нужного тикера</param> 
        [HttpGet("{sectorId}")]
        public async Task<ActionResult<GetSectorDto>> GetSector(int sectorId)
        {
            try
            {
                return await _sectorService.GetSectorAsync(sectorId);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Создаёт новый сектор в базе данных.
        /// Доступно только пользователям с ролью Admin.
        /// Возвращает 201 Created при успешном создании, 400 BadRequest при ошибке.
        /// </summary>
        /// <param name="dto">DTO с данными нового сектора.</param>
        /// <returns>201 Created или 400 BadRequest.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("create-sector")]
        public async Task<IActionResult> CreateSector(CreateSectorDto dto)
        {
            try
            {
                await _sectorService.CreateSectorAsync(dto);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Частично обновляет сектор по идентификатору.
        /// Доступно только пользователям с ролью Admin.
        /// Возвращает 204 NoContent при успешном обновлении, 400 BadRequest при ошибке.
        /// </summary>
        /// <param name="sectorId">Идентификатор обновляемого сектора.</param>
        /// <param name="dto">DTO с полями для обновления.</param>
        /// <returns>204 NoContent или 400 BadRequest.</returns>
        [Authorize(Roles = "Admin")]
        [HttpPatch("{sectorId}")]
        public async Task<IActionResult> PatchSector(int sectorId, PatchSectorDto dto)
        {
            try
            {
                await _sectorService.PatchSector(sectorId, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Удаляет сектор из базы данных (жёсткое удаление).
        /// Доступно только пользователям с ролью Admin.
        /// Возвращает 204 NoContent при успешном удалении, 400 BadRequest при ошибке.
        /// </summary>
        /// <param name="sectorId">Идентификатор удаляемого сектора.</param>
        /// <returns>204 NoContent или 400 BadRequest.</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{sectorId}")]
        public async Task<IActionResult> DeleteSector(int sectorId)
        {
            try
            {
                await _sectorService.DeleteSectorHard(sectorId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
