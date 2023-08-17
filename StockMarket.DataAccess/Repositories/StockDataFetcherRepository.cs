using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StockMarket.DataAccess.Abstract;
using StockMarket.Entities.Concrete;


namespace StockMarket.DataAccess.Repositories
{
    public class StockDataFetcherRepository : IStockDataFetcherRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public StockDataFetcherRepository(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<StockData> FetchStockData(string symbol)
        {
            var apiKey = _configuration["RapidAPI:ApiKey"];

            var httpClient = _httpClientFactory.CreateClient();

            var endpointUrl = $"https://mboum-finance.p.rapidapi.com/qu/quote/financial-data?symbol={symbol}";
            httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", apiKey);

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

