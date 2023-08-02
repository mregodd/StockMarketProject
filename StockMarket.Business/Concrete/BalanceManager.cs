using StockMarket.Business.Abstract;
using StockMarket.DataAccess.Concrete;
using StockMarket.DataAccess.Repositories;
using StockMarket.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Business.Concrete
{
    // Bakiye işlemlerini gerçekleştiren sınıf
    public class BalanceManager : IBalanceManager
    {
        private readonly IBalanceManager _balanceManager;

        public BalanceManager(IBalanceManager balanceManager)
        {
            _balanceManager = balanceManager;
        }

        public UserBalance GetUserBalance(int userId)
        {
            // Kullanıcının bakiyesini almak için BalanceService'i kullanın
            return _balanceManager.GetUserBalance(userId);
        }

        public SystemBalance GetSystemBalance()
        {
            // Sistem bakiyesini almak için BalanceService'i kullanın
            return _balanceManager.GetSystemBalance();
        }

        public void AddUserBalance(int userId, decimal amount)
        {
            // Kullanıcının bakiyesine amount kadar para eklemek için BalanceService'i kullanın
            _balanceManager.AddUserBalance(userId, amount);
        }

        public void SubtractUserBalance(int userId, decimal amount)
        {
            // Kullanıcının bakiyesinden amount kadar para düşürmek için BalanceService'i kullanın
            _balanceManager.SubtractUserBalance(userId, amount);
        }
    }


}
