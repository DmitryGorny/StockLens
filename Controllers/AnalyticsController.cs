using Microsoft.AspNetCore.Mvc;
using StockLens.Queries;
using StockLens.Repositories.Tickers;
using StockLens.Services.Analytics.GeneralAnalytics;
using StockLens.Services.Analytics.Heatmap;
using StockLens.Services.Tickers;

namespace StockLens.Controllers
{
    [Route("api/analytics")]
    [ApiController]
    public class AnalyticsController: ControllerBase
    {
        private readonly IGeneralAnalyticsFacade _generalAnalyticsFacade;
        private readonly IHeatmapFacade _heatmapFacade;
        public AnalyticsController(IGeneralAnalyticsFacade generalAnalyticsFacade, IHeatmapFacade heatmapFacade)
        {
            _generalAnalyticsFacade = generalAnalyticsFacade;
            _heatmapFacade = heatmapFacade;
        }

        [HttpGet]
        [Route("/tickers-general-analytics")]
        public async Task<IActionResult> GetTickersAnalytics([FromQuery] TickersQuery query)
        {
            try
            {
                string json = await _generalAnalyticsFacade.GetTickersGeneralAnalytics(query);
                return Ok(json);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet]
        [Route("/industries-general-analytics")]
        public async Task<IActionResult> GetIndustryAnalytics([FromQuery] IndustriesQuery query)
        {
            try
            {
                string json = await _generalAnalyticsFacade.GetIndustriesGeneralAnalytics(query);
                return Ok(json);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }


        }

        [HttpGet]
        [Route("/sectors-general-analytics")]
        public async Task<IActionResult> GetSectorAnalytics([FromQuery] SectorQuery query)
        {
            try
            {
                string json = await _generalAnalyticsFacade.GetSectorsGeneralAnalytics(query);
                return Ok(json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("/tickers-heatmap")]
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

    }
}
