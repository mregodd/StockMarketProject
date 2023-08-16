using StockMarket.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Business.Abstract
{
    public interface IStockService
    {
        Task AddStockDataToDatabase(string symbol);
        void AddStock(Stock stock);
        Task UpdateStockData();
        void DeleteStock(Stock stock);
        Stock GetStockBySymbol(string symbol);
        Stock GetStockByName(string name);
    }
}
