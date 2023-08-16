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
        private readonly IStockDataFetcher _stockDataFetcher;
        private readonly IStockDataService _stockService;

        public StockMarketController(IHttpClientFactory httpClientFactory, IStockDataFetcher stockDataFetcher, IStockDataService stockService)
        {
            _httpClientFactory = httpClientFactory;
            _stockDataFetcher = stockDataFetcher;
            _stockService = stockService;
        }

        [HttpGet("news")]
        public async Task<IActionResult> GetNews()
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://yahoo-finance15.p.rapidapi.com/api/yahoo/ne/news"),
                };

                // Kullanıcı JWT tokenını Authorization başlığında gönder
                var token = HttpContext.Request.Headers["Authorization"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "3e89b3b45amsh277eb57ca06e20dp14c15ajsndbc08622f03e");
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "yahoo-finance15.p.rapidapi.com");

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
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "yahoo-finance15.p.rapidapi.com");

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
                    // Diğer gerekli özellikleri doldurabilirsiniz
                };

                // Veriyi veritabanına kaydet
                _stockService.AddStock(stock);

                return Ok("Stock data fetched and added to the database.");
            }
            else
            {
                return BadRequest("Failed to fetch stock data.");
            }

        }

    }
}

