using StockMarket.Business.Abstract;
using StockMarket.DataAccess.Abstract;
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
        private readonly IBalanceDal _balanceDal;
        private readonly ISystemBalanceManager _systemBalanceManager;

        public BalanceManager(IBalanceDal balanceDal, ISystemBalanceManager systemBalanceManager)
        {
            _balanceDal = balanceDal;
            _systemBalanceManager = systemBalanceManager;
        }

        public UserBalance GetUserBalance(int userId)
        {
            // Kullanıcının bakiyesini almak için veri erişim katmanını kullanın
            return _balanceDal.GetUserBalance(userId);
        }

        public SystemBalance GetSystemBalance()
        {
            // Sistem bakiyesini almak için manager sınıfını kullanın
            return _balanceDal.GetSystemBalance();
        }

        public void AddUserBalance(int userId, decimal amount)
        {
            // Kullanıcının bakiyesine amount kadar para eklemek için veri erişim katmanını kullanın
            var userBalance = _balanceDal.GetUserBalance(userId);
            userBalance.Balance += amount;
            _balanceDal.UpdateUserBalance(userBalance);
        }

        public void SubtractUserBalance(int userId, decimal amount)
        {
            // Kullanıcının bakiyesinden amount kadar para düşürmek için veri erişim katmanını kullanın
            var userBalance = _balanceDal.GetUserBalance(userId);
            userBalance.Balance -= amount;
            _balanceDal.UpdateUserBalance(userBalance);
        }
        public void UpdateUserBalance(UserBalance userBalance)
        {

            // Önce kullanıcının mevcut bakiyesini alın
            UserBalance existingBalance = _balanceDal.GetUserBalance(userBalance.UserID);

            // Güncellenmiş bakiyeyi hesaplayın
            decimal updatedBalance = existingBalance.Balance + userBalance.Balance;

            // Yeni bakiyeyi UserBalance nesnesine atayın
            existingBalance.Balance = updatedBalance;

            // _BalanceManager.UpdateUserBalance(userBalance);  // Gerek yok, bu satırı kaldırın
            _balanceDal.UpdateUserBalance(existingBalance); // Veritabanına kaydedin


        }
    }

}
