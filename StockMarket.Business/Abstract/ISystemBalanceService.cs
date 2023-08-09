using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Business.Abstract
{
    public interface ISystemBalanceService
    {
        decimal GetSystemBalance();
        void UpdateSystemBalance(decimal newBalance);
    }

}
