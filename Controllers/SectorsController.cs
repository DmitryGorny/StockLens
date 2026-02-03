using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetPaginatedSectors([FromQuery] SectorQuery query)
        {
            return Ok(query);
        }
    }
}
