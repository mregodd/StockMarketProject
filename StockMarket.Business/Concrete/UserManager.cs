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
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IBalanceDal _balanceDal;

        public UserManager(IUserRepository userRepository, IBalanceDal balanceDal)
        {
            _userRepository = userRepository;
            _balanceDal = balanceDal;
        }

        public async Task CreateUser(string username, string password)
        {
            var user = new User
            {
                UserName = username,
                UserBalance = 0 // Varsayılan olarak sıfır bakiye atıyoruz
            };

            _userRepository.CreateUser(user); // Burada kullanıcıyı veritabanına ekleyin

            var userBalance = new UserBalance
            {
                UserID = int.Parse(user.Id),
                Balance = 0 // Varsayılan olarak sıfır bakiye atıyoruz
            };

            _balanceDal.AddUserBalance(userBalance);
            await _userRepository.SaveChangesAsync();
        }

        public Task DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(int userId)
        {
            throw new NotImplementedException();
        }
        public Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }

}
