using StockMarket.Business.Abstract;
using StockMarket.DataAccess.Abstract;
using StockMarket.DataAccess.Concrete;
using StockMarket.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Business.Concrete
{
    public class UserManager : IUser
    {
        private readonly Context _context;

        public UserManager(Context context)
        {
            _context = context;
        }

        public async Task CreateUser(string username, string email, string password)
        {
            var user = new User
            {
                UserName = username,
                UserBalance = 0 // Varsayılan olarak sıfır bakiye atıyoruz
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }

}
