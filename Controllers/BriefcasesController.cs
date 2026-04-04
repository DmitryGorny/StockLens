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
        private readonly IBriefcasesService _briefcasesTickersService;
        public BriefcasesController(IBriefcasesService briefcasesTickersService)
        {
            _briefcasesTickersService = briefcasesTickersService;
        }

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
