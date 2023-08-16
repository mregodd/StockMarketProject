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
        void AddStock(Stock stock);
        void UpdateStock(Stock stock);
        void DeleteStock(Stock stock);
        Stock GetStockBySymbol(string symbol);
        Stock GetStockByName(string name);
    }
}
