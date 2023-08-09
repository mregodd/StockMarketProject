using StockMarket.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Business.Abstract
{
    public interface IBalanceService
    {
        UserBalance GetUserBalance(int userId);
        void AddUserBalance(int userId, decimal amount);
        void SubtractUserBalance(int userId, decimal amount);
        void UpdateUserBalance(UserBalance userBalance);
    }

}
