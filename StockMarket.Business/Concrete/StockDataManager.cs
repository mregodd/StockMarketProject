using StockMarket.Business.Abstract;
using StockMarket.DataAccess.Abstract;
using StockMarket.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Business.Concrete
{
    public class StockDataManager : IStockDataService
    {

        private readonly IStockDataRepository _stockRepository;
        private readonly IStockDataFetcher _stockDataFetcher;
        public StockDataManager(IStockDataRepository stockRepository, IStockDataFetcher stockDataFetcher)
        {
            _stockRepository = stockRepository;
            _stockDataFetcher = stockDataFetcher;
        }

        public async Task AddStockDataToDatabase(string symbol)
        {
            var stockData = await _stockDataFetcher.FetchStockData(symbol);
            _stockRepository.AddStock(stockData);
        }
        public void AddStock(StockData stockData)
        {
            _stockRepository.AddStock(stockData);
        }

        public void DeleteStock(StockData stockData)
        {
            _stockRepository.DeleteStock(stockData);
        }

        public StockData GetStockByName(string name)
        {
            return _stockRepository.GetStockByName(name);
        }

        public StockData GetStockBySymbol(string symbol)
        {
            return _stockRepository.GetStockBySymbol(symbol);
        }

        public async Task UpdateStockData()
        {
            var symbols = new List<string> { "AAPL", "GOOGL", "MSFT" }; // Güncellenecek semboller

            foreach (var symbol in symbols)
            {
                var stockData = await _stockDataFetcher.FetchStockData(symbol);

                if (stockData != null)
                {
                    var existingStock = _stockRepository.GetStockBySymbol(symbol);

                    if (existingStock != null)
                    {
                        existingStock.Price = stockData.Price; // Örnek olarak sadece fiyat güncelleniyor
                        _stockRepository.UpdateStock(existingStock);
                    }
                }
            }
        }

    }

}
