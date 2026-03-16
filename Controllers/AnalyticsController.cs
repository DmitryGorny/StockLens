using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockLens.Mappers;
using StockLens.Queries;
using StockLens.Repositories.Tickers;
using StockLens.Services.Analytics.GeneralAnalytics;
using StockLens.Services.Analytics.Heatmap;
using StockLens.Services.Analytics.Portfolio;
using StockLens.Services.Analytics.TopTen;
using StockLens.Services.Auth.AuthService;
using StockLens.Services.Tickers;
using System.Security.Claims;

namespace StockLens.Controllers
{

    /// <summary>
    /// Аналитические эндпоинты по акциям, секторам и индустриям
    /// </summary>
    [Route("api/analytics")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class AnalyticsController: ControllerBase
    {
        private readonly IGeneralAnalyticsFacade _generalAnalyticsFacade;
        private readonly IHeatmapFacade _heatmapFacade;
        private readonly ITopTenFacade _topTenFacade;
        private readonly IPortfolioService _portfolioService;
        private readonly IAuthService _authService;
        public AnalyticsController(IGeneralAnalyticsFacade generalAnalyticsFacade, 
                                   IHeatmapFacade heatmapFacade,
                                   ITopTenFacade topTenFacade,
                                   IPortfolioService portfolioService,
                                   IAuthService authService)
        {
            _generalAnalyticsFacade = generalAnalyticsFacade;
            _heatmapFacade = heatmapFacade;
            _topTenFacade = topTenFacade;
            _portfolioService = portfolioService;
            _authService = authService;
        }

        [HttpGet]
        [Route("tickers-general-analytics")]
        public async Task<IActionResult> GetTickersAnalytics([FromQuery] int TickerId)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var characteristics = await _authService.GetUsersMetrics(email);
            try
            { 
                string json = await _generalAnalyticsFacade.GetTickersGeneralAnalytics(TickerId, characteristics);
                return Ok(json);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet]
        [Route("industries-general-analytics")]
        public async Task<IActionResult> GetIndustryAnalytics([FromQuery] int IndustryId)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var characteristics = await _authService.GetUsersMetrics(email);
            try
            {
                string json = await _generalAnalyticsFacade.GetIndustriesGeneralAnalytics(IndustryId, characteristics);
                return Ok(json);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }


        }

        [HttpGet]
        [Route("sectors-general-analytics")]
        public async Task<IActionResult> GetSectorAnalytics([FromQuery] int SectorId)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var characteristics = await _authService.GetUsersMetrics(email);
            try
            {
                string json = await _generalAnalyticsFacade.GetSectorsGeneralAnalytics(SectorId, characteristics);
                return Ok(json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("tickers-heatmap")]
        public async Task<IActionResult> GetTickersHeatmap()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var characteristics = await _authService.GetUsersMetrics(email);
            try
            {
                string json = await _heatmapFacade.GetTickersHeatmap(characteristics);
                return Ok(json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet]
        [Route("tickers-top-ten")]
        public async Task<IActionResult> GetTickersTopTen()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var characteristics = await _authService.GetUsersMetrics(email);
            try
            {
                string json = await _topTenFacade.GetTickersTopTen(characteristics);
                return Ok(json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("portfolio-metrics")]
        public async Task<IActionResult> GetPortfolioMetrics([FromQuery] Dictionary<int, decimal> tickersAndPercantages)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var characteristics = await _authService.GetUsersMetrics(email);
            try
            {
                string json = await _portfolioService.GetPorfolioMetrics(tickersAndPercantages, characteristics);
                return Ok(json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("optimized-portfolio")]
        public async Task<IActionResult> GetPortfolioOptimized([FromQuery] List<int> tickersId)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var characteristics = await _authService.GetUsersMetrics(email);
            try
            {
                string json = await _portfolioService.GetOptimizedPortfolio(tickersId, characteristics);
                return Ok(json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
