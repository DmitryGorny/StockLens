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
