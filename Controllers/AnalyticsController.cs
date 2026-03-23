using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockLens.Dtos.QuotesDtos.Analytics.Responses;
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
    /// Контроллер, предоставляющий аналитические эндпоинты по акциям, секторам, индустриям и портфелям.
    /// Все методы требуют авторизации и роли "User".
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

        /// <summary>
        /// Получение общей аналитики для конкретного тикера
        /// </summary>
        /// <param name="TickerId">ID тикера (целое число).</param>
        /// <param name="daysNumber">Количество дней за которое будет выгрузка для анализа</param>
        /// <response code="200">Возвращает координаты графика (x = date: string, y = normalized: float)</response>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /tickers-general-analytics?TickerId=1
        ///     
        /// </remarks>
        [HttpGet]
        [Route("tickers-general-analytics")]
        [ProducesResponseType(typeof(StockItemResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTickersAnalytics([FromQuery] int TickerId, int daysNumber)
        {

            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var characteristics = await _authService.GetUsersMetrics(email);
            try
            { 
                string json = await _generalAnalyticsFacade.GetTickersGeneralAnalytics(TickerId, daysNumber, characteristics);
                return Ok(json);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>
        /// Получение общей аналитики для тикеров конкретного города
        /// </summary>
        /// <param name="CityId">ID города (целое число).</param>
        /// <param name="daysNumber">Количество дней за которое будет выгрузка для анализа</param>
        /// <response code="200">Возвращает координаты графика (x = date: string, y = normalized: float)</response>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /city-general-analytics?CityId=1
        ///     
        /// </remarks>
        [HttpGet]
        [Route("city-general-analytics")]
        [ProducesResponseType(typeof(StockItemResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCityAnalytics([FromQuery] int CityId, int daysNumber)
        {

            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var characteristics = await _authService.GetUsersMetrics(email);
            try
            {
                string json = await _generalAnalyticsFacade.GetCityGeneralAnalytics(CityId, daysNumber, characteristics);
                return Ok(json);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>
        /// Получение общей аналитики для тикеров конкретной индустрии
        /// </summary>
        /// <param name="IndustryId">ID индустрии (целое число).</param>
        /// <param name="daysNumber">Количество дней за которое будет выгрузка для анализа</param>
        /// <response code="200">Возвращает координаты графика (x = date: string, y = normalized: float)</response>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /industries-general-analytics?IndustryId=1
        ///     
        /// </remarks>
        [HttpGet]
        [Route("industries-general-analytics")]
        [ProducesResponseType(typeof(StockItemResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetIndustryAnalytics([FromQuery] int IndustryId, int daysNumber)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var characteristics = await _authService.GetUsersMetrics(email);
            try
            {
                string json = await _generalAnalyticsFacade.GetIndustriesGeneralAnalytics(IndustryId, daysNumber, characteristics);
                return Ok(json);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }


        }

        /// <summary>
        /// Получение общей аналитики для тикеров индустрий конкретного сектора
        /// </summary>
        /// <param name="SectorId">ID сектора (целое число).</param>
        /// <param name="daysNumber">Количество дней за которое будет выгрузка для анализа</param>
        /// <response code="200">Возвращает координаты графика (x = date: string, y = normalized: float)</response>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /industries-general-analytics?IndustryId=1
        ///     
        /// </remarks>
        [HttpGet]
        [Route("sectors-general-analytics")]
        [ProducesResponseType(typeof(StockItemResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSectorAnalytics([FromQuery] int SectorId, int daysNumber)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var characteristics = await _authService.GetUsersMetrics(email);
            try
            {
                string json = await _generalAnalyticsFacade.GetSectorsGeneralAnalytics(SectorId, daysNumber, characteristics);
                return Ok(json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Получение корреляции между всеми секторами
        /// </summary>
        /// <response code="200">
        /// Возвращает массив названий секторов (sectors[]: string)<br></br>
        /// Матрица для построения тепловой карты (matrix[sectors[] length]: decimal)<br></br>
        /// Количество компаний в каждом секторе (stocks_Per_sector {string: int})<br></br>
        /// </response>
        /// 
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /tickers-heatmap
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("tickers-heatmap")]
        [ProducesResponseType(typeof(SectorCorrelationResponse), StatusCodes.Status200OK)]
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

        /// <summary>
        /// Возвращает топ 10 компаний, которые в дни падения всего рынка падают меньше рынка или растут
        /// </summary>
        /// <response code="200">
        /// Возвращает статус выполнения (success: bool)<br></br>
        /// Массив полезных данных для отрисовки рейтингового списка (data []: Dictionary (string: string || decimal || int))<br></br>
        /// </response>
        /// 
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /tickers-top-ten
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("tickers-top-ten")]
        [ProducesResponseType(typeof(AntiCrisisResponse), StatusCodes.Status200OK)]
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

        /// <summary>
        /// Расчитывает метрики для готового портфеля с имеющимся весами по компаниям 
        /// </summary>
        /// <param name="tickersAndPercantages">Словарь с ID тикеров и процентами в портфеле (в сумме должны дать 1)</param>
        /// <response code="200">
        /// Отображение коэфициентов<br></br>
        /// Возвращает статус ожидаемая доходность (expected_return: double)<br></br>
        /// Коэфициент Шарпа (sharpe_ratio: double)<br></br>
        /// Волатильность (volatility: double)<br></br>
        /// </response>
        /// 
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /portfolio-metrics?tickersAndPercantages[2862]=0.46&amp;tickersAndPercantages[2863]=0.54
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("portfolio-metrics")]
        [ProducesResponseType(typeof(OwnWeightsResponse), StatusCodes.Status200OK)]
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

        /// <summary>
        /// Расчитывает веса компаний в портфеле и метрики для портфеля 
        /// </summary>
        /// <param name="tickersId">Список с Id тикеров</param>
        /// <response code="200">
        /// Возвращение коэфцииентов для отображения<br></br>
        /// Вовзаращает веса компаний (Dictionary(string: decimal))<br></br>
        /// Возвращает статус ожидаемая доходность (expected_return: double)<br></br>
        /// Коэфициент Шарпа (sharpe_ratio: double)<br></br>
        /// Волатильность (volatility: double)<br></br>
        /// Поясняющий текст (text: string)<br></br>
        /// Характериситика портфеля (riskProfile: string)<br></br>
        /// </response>
        /// 
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /portfolio-metrics?tickersId = 28624&amp;tickersId = 2863
        ///     
        /// </remarks>
        /// 
        [HttpGet]
        [Route("optimized-portfolio")]
        [ProducesResponseType(typeof(OptimizeResponse), StatusCodes.Status200OK)]
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
