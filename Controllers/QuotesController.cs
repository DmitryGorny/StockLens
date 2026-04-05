using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Types;
using StockLens.Repositories.Quotes;
using StockLens.Services.Moex;
using StockLens.Services.QuotesService;

namespace StockLens.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly IMoexService _moexService;
        private readonly IQuotesService _quotesService;
        public QuotesController(IMoexService moexService, IQuotesService quotesService)
        {
            _moexService = moexService;
            _quotesService = quotesService;
        }

        /// <summary>
        /// Запрашивает исторические котировки для тикера и сохраняет их в базе (bulk).
        /// Доступно только пользователям с ролью Admin.
        /// Возвращает 201 Created при успешной загрузке, 400 BadRequest при ошибке.
        /// </summary>
        /// <param name="TickerId">Идентификатор тикера в базе (внешний ключ).</param>
        /// <param name="TickerSymbol">Торговый символ тикера (например, "GAZP").</param>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("request-quotes")]
        public async Task<IActionResult> RequestQuotes(int TickerId, string TickerSymbol)
        {
            try
            {
                var quotes = await _moexService.RequestQuotesByYears(TickerSymbol, TickerId, 5);
                await _quotesService.CreateQuotesBulk(quotes);
                return Created();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Удаляет котировки для указанного тикера в заданном диапазоне дат (жёсткое удаление).
        /// Доступно только пользователям с ролью Admin.
        /// Возвращает 201 Created при успешном выполнении удаления, 400 BadRequest при ошибке.
        /// </summary>
        /// <param name="TickerId">Идентификатор тикера в базе (внешний ключ).</param>
        /// <param name="startDate">Дата начала интервала удаления (включительно).</param>
        /// <param name="endDate">Дата конца интервала удаления (включительно).</param>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("delete-quotes")]
        public async Task<IActionResult> DeleteQuotes(int TickerId, DateTime startDate, DateTime endDate)
        {
            try
            {
                await _quotesService.DeleteQuotesHard(TickerId, startDate, endDate);
                return Created();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
