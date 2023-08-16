using StockMarket.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Business.Abstract
{
    public interface IStockDataService
    {
        Task AddStockDataToDatabase(string symbol);
        void AddStock(StockData stockData);
        Task UpdateStockData();
        void DeleteStock(StockData stockData);
        StockData GetStockBySymbol(string symbol);
        StockData GetStockByName(string name);
    }
}
