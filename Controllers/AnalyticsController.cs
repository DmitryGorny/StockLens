using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockLens.Queries;
using StockLens.Repositories.Tickers;
using StockLens.Services.Analytics.GeneralAnalytics;
using StockLens.Services.Analytics.Heatmap;
using StockLens.Services.Analytics.TopTen;
using StockLens.Services.Tickers;

namespace StockLens.Controllers
{

    /// <summary>
    /// Аналитические эндпоинты по акциям, секторам и индустриям
    /// </summary>
    [Route("api/analytics")]
    [ApiController]
    [Authorize]
    public class AnalyticsController: ControllerBase
    {
        private readonly IGeneralAnalyticsFacade _generalAnalyticsFacade;
        private readonly IHeatmapFacade _heatmapFacade;
        private readonly ITopTenFacade _topTenFacade;
        public AnalyticsController(IGeneralAnalyticsFacade generalAnalyticsFacade, 
                                   IHeatmapFacade heatmapFacade,
                                   ITopTenFacade topTenFacade)
        {
            _generalAnalyticsFacade = generalAnalyticsFacade;
            _heatmapFacade = heatmapFacade;
            _topTenFacade = topTenFacade;
        }

        [HttpGet]
        [Route("tickers-general-analytics")]
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
        [Route("industries-general-analytics")]
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
        [Route("sectors-general-analytics")]
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


        /// <summary>
        /// Возвращает топ-10 акций по устойчивости к кризисам
        /// </summary>
        /// <param name="query">Параметры фильтрации тикеров</param>
        /// <returns>Список тикеров с метриками</returns>
        /// <response code="200">Успешный ответ</response>
        /// <response code="400">Ошибка валидации</response>
        [HttpGet]
        [Route("tickers-top-ten-custom")]
        public async Task<IActionResult> GetCustomTickersTopTen([FromQuery] TickersQuery query)
        {
            try
            {
                string json = await _topTenFacade.GetCustomTickersTopTen(query);
                return Ok(json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
