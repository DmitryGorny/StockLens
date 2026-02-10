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
        [Route("/general-analytics")]
        public async Task<IActionResult> GetSectorAnalytics([FromQuery] SectorQuery query)
        {
            try
            {
                string json = await _sectorService.GetSectorAnalyticsData(query);
                return Ok(json);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message); 
            }
            
        }
    }
}
