using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StockMarket.API.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockMarketController : ControllerBase
    {
        [HttpGet("news")]
        public async Task<IActionResult> GetNews()
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://yahoo-finance15.p.rapidapi.com/api/yahoo/ne/news"),
                    Headers =
                {
                    { "X-RapidAPI-Key", "3e89b3b45amsh277eb57ca06e20dp14c15ajsndbc08622f03e" },
                    { "X-RapidAPI-Host", "yahoo-finance15.p.rapidapi.com" },
                },
                };

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    return Ok(body);
                }
            }
        }
    }

}
