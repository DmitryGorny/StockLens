using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Index.Strtree;
using StockLens.Dtos.IndustriesDtos;
using StockLens.Queries;
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

        [HttpGet]
        [Route("all-industries")]
        public Task<ActionResult<IEnumerable<GetIndustryDto>>> GetAllIndustries(int start, int size)
        {
            try
            {
                return Task.FromResult<ActionResult<IEnumerable<GetIndustryDto>>>(Ok(_industryService.GetAllIndustriesAsync(start, size)));
            } catch (Exception ex)
            {
                return Task.FromResult<ActionResult<IEnumerable<GetIndustryDto>>>(NotFound(ex.Message));
            }
        }
    }
}
