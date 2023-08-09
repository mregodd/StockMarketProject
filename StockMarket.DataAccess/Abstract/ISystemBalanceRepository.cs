using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.DataAccess.Abstract
{
    public interface ISystemBalanceRepository
    {
        decimal GetSystemBalance();
        void UpdateSystemBalance(decimal newBalance);
    }
}
