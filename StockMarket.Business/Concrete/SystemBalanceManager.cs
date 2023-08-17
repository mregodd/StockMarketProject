using StockMarket.Business.Abstract;
using StockMarket.DataAccess.Concrete;
using StockMarket.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Business.Concrete
{
    public class SystemBalanceManager : ISystemBalanceService
    {
        private decimal _systemBalance;

        public SystemBalanceManager()
        {
            _systemBalance = 0; // Varsayılan olarak sıfır bakiye atıyoruz
        }

        public decimal GetSystemBalance()
        {
            return _systemBalance;
        }

        public void UpdateSystemBalance(decimal newBalance)
        {
            _systemBalance = newBalance;
        }
    }
}
