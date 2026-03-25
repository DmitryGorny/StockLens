using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using StockLens.Dtos.QuotationsDtos;
using StockLens.Models;
using StockLens.Services.HttpRequester;
using StockLens.Services.HttpRequester.MoexHttpRequester;
using System.Numerics;
using System.Text.Json;

namespace StockLens.Services.Moex
{
    public class MoexService : IMoexService
    {
        private readonly IHttpRequester _httpRequester;

        public MoexService(IHttpRequester requester)
        {
            _httpRequester = requester;
        }
        public async Task<List<CreateQuotesDto>> RequestQuotesByYears(string TickerSymbol, int TickerId, int yearsDelta=5)
        {
            List<CreateQuotesDto> dtos = new List<CreateQuotesDto>();
            int start = 0;
            while (true) {
                DateTime today = DateTime.Today;
                DateTime fiveYearsAgo = today.AddYears(-yearsDelta);
                Root? root = await _httpRequester.GetJsonAsync<Root>("https://iss.moex.com/iss/history/engines/stock/markets/shares/boards/TQBR/securities/" +
                    $"{TickerSymbol}.json" +
                    $"?from={fiveYearsAgo.ToString("yyyy-MM-dd")}&till={today.ToString("yyyy-MM-dd")}" +
                    $"&start={start}" +
                    "&history.columns=TRADEDATE,OPEN,CLOSE,LOW,HIGH,VOLUME,VALUE,NUMTRADES,WAPRICE");
                
                if (root.history.data.Count == 0) 
                    break;

                foreach (var item in root.history.data)
                {
                    try
                    {
                        dtos.Add(QuoteValidator(item, TickerId));
                    } catch(InvalidDataException e) { continue; }                  
                    catch(InvalidOperationException e) { continue; }
                }

                start += 100;
            }
            return dtos;
        }

        public async Task<IEnumerable<CreateQuotesDto>> RequestQuotesByDays(string TickerSymbol, int TickerId, int daysDelta)
        {
            List<CreateQuotesDto> dtos = new List<CreateQuotesDto>();
            int start = 0;
            while (true)
            {
                DateTime today = DateTime.Today;
                DateTime from = today.AddDays(daysDelta);

                Root? root = await _httpRequester.GetJsonAsync<Root>("https://iss.moex.com/iss/history/engines/stock/markets/shares/boards/TQBR/securities/" +
                    $"{TickerSymbol}.json" +
                    $"?from={from.ToString("yyyy-MM-dd")}&till={today.ToString("yyyy-MM-dd")}" +
                     $"&start={start}" +
                    "&history.columns=TRADEDATE,OPEN,CLOSE,LOW,HIGH,VOLUME,VALUE,NUMTRADES,WAPRICE");

                if (root.history.data.Count == 0)
                    break;

                foreach (var item in root.history.data)
                {
                    try
                    {
                        dtos.Add(QuoteValidator(item, TickerId));
                    }
                    catch (InvalidDataException e) { continue; }
                    catch (InvalidOperationException e) { continue; }
                }
                start += 100;
            }
            return dtos;
        }

        public async Task<int?> RequesTickersListLevel(string TickerSymbol)
        {
            var l = await _httpRequester.GetJsonAsync<RootLevel>($"https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities/{TickerSymbol}.json?iss.only=securities&securities.columns=LISTLEVEL");
            
            if (l != null && l.securities.data.First().Length == 0)
                return null;

            int.TryParse(l!.securities.data.First().First().ToString(), out int result);
            return result;
        }

        private CreateQuotesDto QuoteValidator(JsonElement[] data, int TickerId)
        {
            if (!data[1].TryGetDecimal(out var open))
                throw new InvalidDataException("open is null");

            if (!data[2].TryGetDecimal(out var close))
                throw new InvalidDataException("close is null");

            if (!data[3].TryGetDecimal(out var low))
                throw new InvalidDataException("low is null");

            if (!data[4].TryGetDecimal(out var high))
                throw new InvalidDataException("high is null");

            if (!data[5].TryGetDecimal(out var volume))
                throw new InvalidDataException("volume is null");

            if (!data[6].TryGetDecimal(out var value))
                throw new InvalidDataException("value is null");

            if (!data[7].TryGetDecimal(out var numtrades))
                throw new InvalidDataException("numtrades is null");

            if (!data[8].TryGetDecimal(out var waprice))
                throw new InvalidDataException("waprice is null");

            return new CreateQuotesDto
            {
                ts = DateFromString(RequireString(data[0], "date")),
                open = open,
                close = close,
                low = low,
                high = high,
                volume = BigIntegerFromDecimal(volume),
                value = BigIntegerFromDecimal(value),
                numtrades = BigIntegerFromDecimal(numtrades),
                waprice = waprice,
                TickerId = TickerId
            };
        }

        private class ListLevel
        {
            public List<JsonElement[]> data { get; set; } = new();
        }
        private class RootLevel
        {
            public ListLevel securities { get; set; }
        }

        private class Root
        {
            public QuotesHistory history {  get; set; }

        }

        private class QuotesHistory
        {
            public List<JsonElement[]> data { get; set; }
        }

        private DateTime DateFromString(string? str)
        {
            if (str == null || !DateTime.TryParse(str, out DateTime dt)) 
                throw new InvalidDataException($"Невозможно преобразовать {str} в DateTime");

            return DateTime.SpecifyKind(
                        dt,
                        DateTimeKind.Utc
                    );
        }

        private BigInteger BigIntegerFromDecimal(decimal dc)
        {
            if (dc == null)
                throw new InvalidDataException($"Невозможно преобразовать {dc} в Decimal");

            BigInteger dt = new BigInteger(decimal.Round(dc));
            return dt;
        }


        private string RequireString(JsonElement el, string name)
        {
            if (el.ValueKind != JsonValueKind.String)
                throw new InvalidDataException($"{name} is null or not a string");

            return el.GetString()!;
        }


    }
}
