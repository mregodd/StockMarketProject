using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Business.Abstract
{
    public interface IStockDataTransactionService
    {
        Task<bool> BuyStock(int userId, string symbol, int quantity);
        Task<bool> SellStock(int userId, string symbol, int quantity);
    }

}
