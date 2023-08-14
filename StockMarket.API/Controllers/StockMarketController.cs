using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace StockMarket.API.Controllers
{
    [ApiController]
    [Route("api/stock")]
    [Authorize]
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

                // Kullanıcı JWT tokenını Authorization başlığında gönder
                var token = HttpContext.Request.Headers["Authorization"];
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://yahoo-finance15.p.rapidapi.com/api/yahoo/hi/history/AAPL/15m?diffandsplits=false"),
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
                    Console.WriteLine(body); // İsteğin sonucunu konsola yazdırabilirsiniz.

                    // Eğer API'den gelen veriyi JSON olarak çözmek isterseniz aşağıdaki gibi yapabilirsiniz:
                    // var parsedData = JsonConvert.DeserializeObject<YourModelClass>(body);

                    return Ok(body); // İsteğin sonucunu HTTP yanıtı olarak döndürün
                }
            }
        }


    }
}
