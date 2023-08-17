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
    public class BalanceManager : IBalanceService
    {
        private readonly IBalanceRepository _balanceRepository;

        public BalanceManager(IBalanceRepository balanceRepository)
        {
            _balanceRepository = balanceRepository;
        }

        public UserBalance GetUserBalance(int userId)
        {
            // Kullanıcının bakiyesini almak için veri erişim katmanını kullanın
            return _balanceRepository.GetUserBalance(userId);
        }

        public void AddUserBalance(int userId, decimal amount)
        {
            // Kullanıcının bakiyesine amount kadar para eklemek için veri erişim katmanını kullanın
            var userBalance = _balanceRepository.GetUserBalance(userId);
            if (userBalance == null)
            {
                userBalance = new UserBalance
                {
                    AppUserId = userId,
                    Balance = amount
                };
                _balanceRepository.AddUserBalance(userBalance); // Yeni bakiye ekleyin
            }
            else
            {
                userBalance.Balance += amount;
                _balanceRepository.UpdateUserBalance(userBalance);
            }
        }

        public void SubtractUserBalance(int userId, decimal amount)
        {
            // Kullanıcının bakiyesinden amount kadar para düşürmek için veri erişim katmanını kullanın
            var userBalance = _balanceRepository.GetUserBalance(userId);
            userBalance.Balance -= amount;
            _balanceRepository.UpdateUserBalance(userBalance);
        }
        public void UpdateUserBalance(UserBalance userBalance)
        {

            // Önce kullanıcının mevcut bakiyesini alın
            UserBalance existingBalance = _balanceRepository.GetUserBalance(userBalance.AppUserId);

            // Güncellenmiş bakiyeyi hesaplayın
            decimal updatedBalance = existingBalance.Balance + userBalance.Balance;

            // Yeni bakiyeyi UserBalance nesnesine atayın
            existingBalance.Balance = updatedBalance;

            _balanceRepository.UpdateUserBalance(existingBalance); // Veritabanına kaydedin


        }
    }

}
