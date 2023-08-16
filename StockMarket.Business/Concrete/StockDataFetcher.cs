using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StockMarket.Business.Abstract;
using StockMarket.Entities.Concrete;

namespace StockMarket.Business.Concrete
{
    public class StockDataFetcher : IStockDataFetcher
    {
        public async Task<Stock> FetchStockData(string symbol)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://yahoo-finance15.p.rapidapi.com/api/yahoo/stock/{symbol}"),
                    Headers =
                    {
                        { "X-RapidAPI-Key", "3e89b3b45amsh277eb57ca06e20dp14c15ajsndbc08622f03e" },
                        { "X-RapidAPI-Host", "yahoo-finance15.p.rapidapi.com" },
                    },
                };

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    var stockData = JsonConvert.DeserializeObject<Stock>(content);
                    return stockData;
                }
            }
        }
    }
}
