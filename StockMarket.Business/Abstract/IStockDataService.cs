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
        Task AddStockAsync(StockData stockData);
        Task UpdateStockDataAsync();
        Task DeleteStockAsync(StockData stockData);
        Task<StockData> GetStockBySymbolAsync(string symbol);
        Task<StockData> GetStockByNameAsync(string name);
    }
}
