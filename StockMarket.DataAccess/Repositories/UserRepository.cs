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

        public async Task<AppUser> GetUserById(int userID)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userID);
        }

        public async Task CreateUser(string username, string password)
        {
            var user = new AppUser
            {
                UserName = username,
                UserBalance = 0 // Varsayılan olarak sıfır bakiye atıyoruz
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(AppUser user)
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
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
    