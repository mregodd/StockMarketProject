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
    public class StockManager : IStockService
    {

        private readonly IStockRepository _stockRepository;
        private readonly IStockDataFetcher _stockDataFetcher;
        public StockManager(IStockRepository stockRepository, IStockDataFetcher stockDataFetcher)
        {
            _stockRepository = stockRepository;
            _stockDataFetcher = stockDataFetcher;
        }

        public async Task AddStockDataToDatabase(string symbol)
        {
            var stockData = await _stockDataFetcher.FetchStockData(symbol);
            _stockRepository.AddStock(stockData);
        }
        public void AddStock(Stock stock)
        {
            _stockRepository.AddStock(stock);
        }

        public void DeleteStock(Stock stock)
        {
            _stockRepository.DeleteStock(stock);
        }

        public Stock GetStockByName(string name)
        {
            return _stockRepository.GetStockByName(name);
        }

        public Stock GetStockBySymbol(string symbol)
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
