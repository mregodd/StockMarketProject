using StockMarket.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.DataAccess.Abstract
{
    public interface IStockRepository
    {
        void AddStock(Stock stock);
        void UpdateStock(Stock stock);
        void DeleteStock(Stock stock);
        Stock GetStockBySymbol(string symbol);
        Stock GetStockByName(string name);
    }
}
