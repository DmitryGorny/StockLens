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
