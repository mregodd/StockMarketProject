using StockMarket.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.DataAccess.Abstract
{
    public interface IStockDataRepository
    {
        void AddStock(StockData stock);
        void UpdateStock(StockData stock);
        void DeleteStock(StockData stock);
        StockData GetStockBySymbol(string symbol);
        StockData GetStockByName(string name);
    }
}
