using StockMarket.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.DataAccess.Abstract
{
    public interface IBalanceRepository
    {
        UserBalance GetUserBalance(int userId);
        void AddUserBalance(UserBalance userBalance);
        void SubtractUserBalance(int userId, decimal amount);
        void UpdateUserBalance(UserBalance existingBalance);
    }


}
