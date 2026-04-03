using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockLens.Dtos.BriefcasesDtos;
using StockLens.Services.BriefcasesTickers;
using System.Security.Claims;

namespace StockLens.Controllers
{
    [Route("api/briefcases")]
    [ApiController]
    [Authorize]
    public class BriefcasesController : ControllerBase
    {
        private readonly IBriefcasesTickersService _briefcasesTickersService;
        public BriefcasesController(IBriefcasesTickersService briefcasesTickersService) 
        {
            _briefcasesTickersService = briefcasesTickersService;
        }

        [HttpGet]
        [Route("users-briefcases")]
        public async Task<ActionResult<IEnumerable<GetBrifcasesListDto>>> GetBriefcasesList([FromQuery] int start, int size)
        {

            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;
                var list = await _briefcasesTickersService.GetBrifcasesListAsync(email, start, size);
                return Ok(list);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("create-briefcase")]
        public async Task<ActionResult> CreateBriefcases([FromBody] CreateBriefcaseDto dto)
        {

            try
            {
                await _briefcasesTickersService.CreateBriefcase(dto);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
