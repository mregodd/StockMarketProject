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
        public StockManager(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
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

        public void UpdateStock(Stock stock)
        {
            _stockRepository.UpdateStock(stock);
        }
    }

}