using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockLens.Mappers;
using StockLens.Queries;
using StockLens.Repositories.Tickers;
using StockLens.Services.Analytics.GeneralAnalytics;
using StockLens.Services.Analytics.Heatmap;
using StockLens.Services.Analytics.Portfolio;
using StockLens.Services.Analytics.TopTen;
using StockLens.Services.Tickers;

namespace StockLens.Controllers
{

    /// <summary>
    /// Аналитические эндпоинты по акциям, секторам и индустриям
    /// </summary>
    [Route("api/analytics")]
    [ApiController]
    public class AnalyticsController: ControllerBase
    {
        private readonly IGeneralAnalyticsFacade _generalAnalyticsFacade;
        private readonly IHeatmapFacade _heatmapFacade;
        private readonly ITopTenFacade _topTenFacade;
        private readonly IPortfolioService _portfolioService;
        public AnalyticsController(IGeneralAnalyticsFacade generalAnalyticsFacade, 
                                   IHeatmapFacade heatmapFacade,
                                   ITopTenFacade topTenFacade,
                                   IPortfolioService portfolioService)
        {
            _generalAnalyticsFacade = generalAnalyticsFacade;
            _heatmapFacade = heatmapFacade;
            _topTenFacade = topTenFacade;
            _portfolioService = portfolioService;
        }

        [HttpGet]
        [Route("tickers-general-analytics")]
        public async Task<IActionResult> GetTickersAnalytics([FromQuery] int SectorId)
        {
            try
            {
                string json = await _generalAnalyticsFacade.GetTickersGeneralAnalytics(SectorId);
                return Ok(json);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet]
        [Route("industries-general-analytics")]
        public async Task<IActionResult> GetIndustryAnalytics([FromQuery] int IndustryId)
        {
            try
            {
                string json = await _generalAnalyticsFacade.GetIndustriesGeneralAnalytics(IndustryId);
                return Ok(json);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }


        }

        [HttpGet]
        [Route("sectors-general-analytics")]
        public async Task<IActionResult> GetSectorAnalytics([FromQuery] int TickerId)
        {
            try
            {
                string json = await _generalAnalyticsFacade.GetSectorsGeneralAnalytics(TickerId);
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
            try
            {
                string json = await _heatmapFacade.GetTickersHeatmap();
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
            try
            {
                string json = await _topTenFacade.GetTickersTopTen();
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
            try
            {
                string json = await _portfolioService.GetPorfolioMetrics(tickersAndPercantages);
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
            try
            {
                string json = await _portfolioService.GetOptimizedPortfolio(tickersId);
                return Ok(json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
