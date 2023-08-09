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
        private readonly IBalanceRepository _balanceDal;

        public BalanceManager(IBalanceRepository balanceDal)
        {
            _balanceDal = balanceDal;
        }

        public UserBalance GetUserBalance(int userId)
        {
            // Kullanıcının bakiyesini almak için veri erişim katmanını kullanın
            return _balanceDal.GetUserBalance(userId);
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
            UserBalance existingBalance = _balanceDal.GetUserBalance(userBalance.AppUserID);

            // Güncellenmiş bakiyeyi hesaplayın
            decimal updatedBalance = existingBalance.Balance + userBalance.Balance;

            // Yeni bakiyeyi UserBalance nesnesine atayın
            existingBalance.Balance = updatedBalance;

            // _BalanceManager.UpdateUserBalance(userBalance);  // Gerek yok, bu satırı kaldırın
            _balanceDal.UpdateUserBalance(existingBalance); // Veritabanına kaydedin


        }
    }

}
