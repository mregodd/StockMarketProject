using StockMarket.DataAccess.Abstract;
using StockMarket.DataAccess.Concrete;
using StockMarket.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.DataAccess.Repositories
{
    public class BalanceRepository : IBalanceDal
    {
        private readonly Context _context;

        public BalanceRepository(Context context)
        {
            _context = context;
        }

        public UserBalance GetUserBalance(int userId)
        {
            // Kullanıcı bakiye bilgisini veritabanından çekme işlemi
            var userBalance = _context.UserBalances.FirstOrDefault(b => b.UserID == userId);
            return userBalance;
        }

        public SystemBalance GetSystemBalance()
        {
            // Sistem bakiye bilgisini veritabanından çekme işlemi veya başka bir kaynaktan alabilirsiniz
            var systemBalance = new SystemBalance();
            return systemBalance;
        }

        public void AddUserBalance(UserBalance userBalance)
        {
            _context.UserBalances.Add(userBalance);
            _context.SaveChanges();
        }

        public void UpdateUserBalance(UserBalance userBalance)
        {
            // Kullanıcı bakiyesini veritabanında güncelleme işlemi
            var existingBalance = _context.UserBalances.FirstOrDefault(b => b.UserID == userBalance.UserID);
            if (existingBalance != null)
            {
                existingBalance.Balance = userBalance.Balance;
                _context.SaveChanges();
            }
        }

        public void SubtractUserBalance(int userId, decimal amount)
        {
            // Kullanıcı bakiyesini veritabanında güncelleme işlemi
            var userBalance = _context.UserBalances.FirstOrDefault(b => b.UserID == userId);
            if (userBalance != null)
            {
                userBalance.Balance -= amount;
                _context.SaveChanges();
            }
        }
    }
}
