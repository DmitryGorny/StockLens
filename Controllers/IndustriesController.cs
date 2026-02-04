using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetIndustriesBySector([FromQuery] IndustriesQuery query)
        {
            return Ok();
        }
    }
}
