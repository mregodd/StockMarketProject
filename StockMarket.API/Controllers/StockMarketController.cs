using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockMarket.API.Security;
using StockMarket.Business.Abstract;
using StockMarket.Entities.Concrete;
using System.Net.Http.Headers;

namespace StockMarket.API.Controllers
{
    [ApiController]
    [Route("api/stock")]
    [Authorize]
    public class StockMarketController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IStockDataFetcherService _stockDataFetcher;
        private readonly IStockDataService _stockDataService;

        public StockMarketController(IHttpClientFactory httpClientFactory, IStockDataFetcherService stockDataFetcher, IStockDataService stockDataService)
        {
            _httpClientFactory = httpClientFactory;
            _stockDataFetcher = stockDataFetcher;
            _stockDataService = stockDataService;
        }

        [HttpGet("news")]
        public async Task<IActionResult> GetNews()
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://mboum-finance.p.rapidapi.com/ne/news"),
                };

                // Kullanıcı JWT tokenını Authorization başlığında gönder
                var token = HttpContext.Request.Headers["Authorization"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "3e89b3b45amsh277eb57ca06e20dp14c15ajsndbc08622f03e");
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "mboum-finance.p.rapidapi.com");

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    return Ok(body);
                }
            }
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://yahoo-finance15.p.rapidapi.com/api/yahoo/hi/history/AAPL/15m?diffandsplits=false"),
                };

                var token = HttpContext.Request.Headers["Authorization"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "3e89b3b45amsh277eb57ca06e20dp14c15ajsndbc08622f03e");
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "mboum-finance.p.rapidapi.com");

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    return Ok(body); // İsteğin sonucunu HTTP yanıtı olarak döndürün
                }
            }
        }

        [HttpGet("fetch-and-add")]
        public async Task<IActionResult> FetchAndAddStockData(string symbol)
        {
            var stockData = await _stockDataFetcher.FetchStockData(symbol);

            if (stockData != null)
            {
                var stock = new StockData
                {
                    Symbol = stockData.Symbol,
                    StockName = stockData.StockName,
                    Price = stockData.Price,
                    Quantity = stockData.Quantity
                };

                // Veriyi veritabanına kaydet
                await _stockDataService.AddStockAsync(stock);

                return Ok("Stock data fetched and added to the database.");
            }
            else
            {
                return BadRequest("Failed to fetch stock data.");
            }

        }

        [HttpGet("{symbol}")]
        public async Task<IActionResult> GetStockBySymbol(string symbol)
        {
            var stock = await _stockDataService.GetStockBySymbolAsync(symbol);
            if (stock == null)
                return NotFound();

            return Ok(stock);
        }

        [HttpGet("StockName/{name}")]
        public async Task<IActionResult> GetStockByName(string name)
        {
            var stock = await _stockDataService.GetStockByNameAsync(name);
            if (stock == null)
                return NotFound();

            return Ok(stock);
        }

        [Authorize("AdminOnly")]
        [HttpDelete("{symbol}")]
        public async Task<IActionResult> DeleteStock(string symbol)
        {
            var stock = await _stockDataService.GetStockBySymbolAsync(symbol);
            if (stock == null)
                return NotFound();

            await _stockDataService.DeleteStockAsync(stock);
            return Ok("Stock başarıyla silindi.");
        }

    }
}

