using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using StockLens.Dtos.SectorDtos;
using StockLens.Queries;
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
    }
}
