using Microsoft.AspNetCore.Identity;
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
    public class UserManager : IUserManager
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IBalanceDal _balanceDal;

        public UserManager(UserManager<AppUser> userManager, IUserRepository userRepository, IBalanceDal balanceDal)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _balanceDal = balanceDal;
        }

        public async Task CreateUser(string username, string password)
        {
            var user = new AppUser // AppUser kullanıldı
            {
                UserName = username,
                UserBalance = 0
            };

            var result = await _userManager.CreateAsync(user, password); // Identity UserManager kullanıldı

            if (result.Succeeded)
            {
                var userBalance = new UserBalance
                {
                    AppUserID = user.Id, // AppUserId olarak user.Id kullanıldı
                    Balance = 0
                };

                _balanceDal.AddUserBalance(userBalance);
                await _userRepository.SaveChangesAsync();
            }
        }

        public Task DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> GetUserById(int userId)
        {
            throw new NotImplementedException();
        }
        public Task UpdateUser(AppUser user)
        {
            throw new NotImplementedException();
        }
    }

}
