using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockLens.Dtos.BriefcasesDtos;
using StockLens.Services.BriefcasesTickers;
using System.Security.Claims;

namespace StockLens.Controllers
{
    /// <summary>
    /// Контроллер для операций с портфелями (briefcases).
    /// </summary>
    [Route("api/briefcases")]
    [ApiController]
    [Authorize]
    public class BriefcasesController : ControllerBase
    {
        private readonly IBriefcasesService _briefcasesTickersService;
        public BriefcasesController(IBriefcasesService briefcasesTickersService)
        {
            _briefcasesTickersService = briefcasesTickersService;
        }

        /// <summary>
        /// Возвращает данные по конкретному портфелю по его идентификатору.
        /// </summary>
        /// <param name="briefcaseId">Идентификатор портфеля.</param>
        /// <returns>200 OK с данными портфеля или 400 BadRequest при ошибке.</returns>
        [HttpGet("{briefcaseId}")]
        public async Task<ActionResult<GetBriefcasesDto>> GetBriefcase(int briefcaseId)
        {
            try
            {
                return Ok(await _briefcasesTickersService.GetBriefcase(briefcaseId));
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Возвращает список портфелей текущего пользователя с пагинацией.
        /// </summary>
        /// <param name="start">Индекс начала выборки (offset).</param>
        /// <param name="size">Количество элементов в выборке (limit).</param>
        /// <returns>200 OK с коллекцией портфелей, 401 Unauthorized если пользователь не авторизован, 400 BadRequest при ошибке.</returns>
        [HttpGet]
        [Route("users-briefcases")]
        public async Task<ActionResult<IEnumerable<GetBrifcasesListDto>>> GetBriefcasesList([FromQuery] int start, int size)
        {

            try
            {
                var email = User.FindFirst(ClaimTypes.Email)!.Value;
                var list = await _briefcasesTickersService.GetBrifcasesListAsync(email, start, size);
                return Ok(list);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Создаёт новый портфель для текущего пользователя.
        /// </summary>
        /// <param name="dto">DTO с данными создаваемого портфеля.</param>
        /// <returns>201 Created при успешном создании, 401 Unauthorized если пользователь не авторизован, 400 BadRequest при ошибке.</returns>
        [HttpPost]
        [Route("create-briefcase")]
        public async Task<ActionResult> CreateBriefcases([FromBody] CreateBriefcaseDto dto)
        {
            string email = User.FindFirst(ClaimTypes.Email)!.Value;
            try
            {
                await _briefcasesTickersService.CreateBriefcase(email, dto);
                return Created();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Удаляет портфель по идентификатору.
        /// </summary>
        /// <param name="breifcaseId">Идентификатор удаляемого портфеля.</param>
        /// <returns>201 Created при успешном выполнении (текущее поведение), 400 BadRequest при ошибке.</returns>
        [HttpDelete("{breifcaseId}")]
        public async Task<ActionResult> DeleteBriefcases(int breifcaseId) 
        {
            try
            {
                await _briefcasesTickersService.DeleteBriefcase(breifcaseId);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Частично обновляет портфель (например, изменение списка тикеров или имени).
        /// </summary>
        /// <param name="breifcaseId">Идентификатор обновляемого портфеля.</param>
        /// <param name="patchBriefcaseDto">DTO с полями для частичного обновления.</param>
        /// <returns>204 NoContent при успешном обновлении, 400 BadRequest при ошибке.</returns>
        [HttpPatch("{breifcaseId}")]
        public async Task<ActionResult> PatchBriefcaseTicker(int breifcaseId, [FromBody] PatchBriefcaseDto patchBriefcaseDto)
        {
            try
            {
                await _briefcasesTickersService.PatchBriefcasesTickers(breifcaseId, patchBriefcaseDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
