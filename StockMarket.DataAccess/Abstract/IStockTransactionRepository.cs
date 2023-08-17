using StockMarket.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.DataAccess.Abstract
{
    public interface IStockTransactionRepository
    {
        Task AddTransactionAsync(StockTransaction transaction);
        Task<bool> BuyStock(int userId, string symbol, int quantity);
        Task<bool> SellStock(int userId, string symbol, int quantity);
    }
}
