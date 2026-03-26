
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockLens.Dtos.IndustriesDtos;
using StockLens.Services.Industries;

namespace StockLens.Controllers
{
    [Route("api/industries")]
    [ApiController]
    public class IndustriesController : ControllerBase
    {
        private readonly IIndustriesService _industryService;

        public IndustriesController(IIndustriesService industryService)
        {
            _industryService = industryService;
        }

        /// <summary>
        ///Выгружает данные по всем индустриям, которые принадлежат к секторам с переданными id, с пагинацией. Если тикеров нет, то возвращает 404 ошибку
        /// </summary>
        /// <param name="SectorId">Список с id секторов</param> 
        /// <param name="start">Индекс начала выборки</param> 
        /// <param name="size">Количество тикеров в запросе</param>
        [HttpGet]
        [Route("by-sector-id")]
        public async Task<ActionResult<IEnumerable<GetIndustryDto>>> GetIndustriesBySectorsId([FromQuery] List<int> SectorId)
        {
            try
            {
                return Ok(await _industryService.GetIndustriesBySectorAsync(SectorId, start: 0, size: 100));
            } catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Выгружает данные по одной индустрии, которая соответствует переданному id. Если индустрии с таким id не существует, то возвращает 404 ошибку
        /// </summary>
        /// <param name="IndustriesId">Id нужного тикера</param> 
        [HttpGet("{IndustriesId}")]
        public async Task<ActionResult<GetIndustryDto>> GetIndustry(int IndustriesId)
        {
            try
            {
                return await _industryService.GetIndustryAsync(IndustriesId);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);

            }
        }

        /// <summary>
        ///Выгружает данные по всем индустриям с пагинацией. Если индустрий нет, то возвращает 404 ошибку
        /// </summary>
        /// <param name="start">Индекс начала выборки</param> 
        /// <param name="size">Количество тикеров в запросе</param>
        [HttpGet]
        [Route("all-industries")]
        public async Task<ActionResult<IEnumerable<GetIndustryDto>>> GetAllIndustries(int start, int size)
        {
            try
            {
                return Ok(await _industryService.GetAllIndustriesAsync(start, size));
            } catch (Exception ex)
            {
                return Ok(await _industryService.GetAllIndustriesAsync(start, size)); ;
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("create-industry")]
        public async Task<IActionResult> CreateIndustry(CreateIndustryDto dto)
        {
            try
            {
                await _industryService.CreateIndustry(dto);
                return Created(); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{IndustryId}")]
        public async Task<IActionResult> PatchIndustry(int IndustryId, PatchIndustryDto dto)
        {
            try
            {
                await _industryService.PatchIndustry(IndustryId, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{IndustryId}")]
        public async Task<IActionResult> DeleteIndustry(int IndustryId)
        {
            try
            {
                await _industryService.DeleteIndustry(IndustryId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
