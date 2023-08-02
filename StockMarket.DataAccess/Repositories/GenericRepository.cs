using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public async Task<User> GetUserById(int userID)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserID == userID);
        }

        public async Task CreateUser(string username, string password)
        {
            var user = new User
            {
                UserName = username,
                UserBalance = 0 // Varsayılan olarak sıfır bakiye atıyoruz
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }

    public class PortfolioService
    {
        private readonly Context _context;

        public PortfolioService(Context context)
        {
            _context = context;
        }

        // Hisse senedi bilgilerini eklemek için metot
        public void AddPortfolio(UserPortfolio portfolio)
        {
            _context.UserPortfolios.Add(portfolio);
            _context.SaveChanges();
        }

        // Diğer portföy işlemleri için metotlar buraya eklenebilir
    }

    public class BalanceManager : IBalanceDal
    {
        private readonly Context _context;

        public BalanceManager(Context context)
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
