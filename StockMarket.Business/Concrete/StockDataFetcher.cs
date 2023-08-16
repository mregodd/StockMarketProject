using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StockMarket.Business.Abstract;
using StockMarket.Entities.Concrete;

namespace StockMarket.Business.Concrete
{
    public class StockDataFetcher : IStockDataFetcher
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StockDataFetcher(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<StockData> FetchStockData(string symbol)
        {
            var apiKey = "3e89b3b45amsh277eb57ca06e20dp14c15ajsndbc08622f03e"; // RapidAPI'den aldığınız API anahtarı
            var httpClient = _httpClientFactory.CreateClient();

            var endpointUrl = $"https://yahoo-finance15.p.rapidapi.com/api/yahoo/qu/quote?symbol={symbol}"; // RapidAPI endpoint'i
            httpClient.DefaultRequestHeaders.Add("3e89b3b45amsh277eb57ca06e20dp14c15ajsndbc08622f03e", apiKey);

            var response = await httpClient.GetAsync(endpointUrl);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var stockData = JsonConvert.DeserializeObject<StockData>(responseContent);
                return stockData;
            }
            else
            {
                Console.WriteLine($"Sembol için stok verileri getirilemedi {symbol}. StatusCode: {response.StatusCode}");
                return null;
            }
        }
    }
}
