using StockMarket.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Business.Abstract
{
    public interface IBalanceManager
    {
        UserBalance GetUserBalance(int userId);
        SystemBalance GetSystemBalance();
        void AddUserBalance(int userId, decimal amount);
        void SubtractUserBalance(int userId, decimal amount);
        void UpdateUserBalance(UserBalance userBalance);
    }

}
