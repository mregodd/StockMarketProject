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
        private readonly IBalanceManager _BalanceManager;
        private readonly ISystemBalanceManager _systemBalanceManager;

        public BalanceManager(IBalanceManager BalanceManager, ISystemBalanceManager systemBalanceManager)
        {
            _BalanceManager = BalanceManager;
            _systemBalanceManager = systemBalanceManager;
        }

        public UserBalance GetUserBalance(int userId)
        {
            // Kullanıcının bakiyesini almak için veri erişim katmanını kullanın
            return _BalanceManager.GetUserBalance(userId);
        }

        public SystemBalance GetSystemBalance()
        {
            // Sistem bakiyesini almak için manager sınıfını kullanın
            return _BalanceManager.GetSystemBalance();
        }

        public void AddUserBalance(int userId, decimal amount)
        {
            // Kullanıcının bakiyesine amount kadar para eklemek için veri erişim katmanını kullanın
            var userBalance = _BalanceManager.GetUserBalance(userId);
            userBalance.Balance += amount;
            _BalanceManager.UpdateUserBalance(userBalance);
        }

        public void SubtractUserBalance(int userId, decimal amount)
        {
            // Kullanıcının bakiyesinden amount kadar para düşürmek için veri erişim katmanını kullanın
            var userBalance = _BalanceManager.GetUserBalance(userId);
            userBalance.Balance -= amount;
            _BalanceManager.UpdateUserBalance(userBalance);
        }
        public void UpdateUserBalance(UserBalance userBalance)
        {

            // Önce kullanıcının mevcut bakiyesini alın
            UserBalance existingBalance = GetUserBalance(userBalance.UserID);

            // Güncellenmiş bakiyeyi hesaplayın
            decimal updatedBalance = existingBalance.Balance + userBalance.Balance;

            // Yeni bakiyeyi UserBalance nesnesine atayın
            existingBalance.Balance = updatedBalance;


        }
    }

}
