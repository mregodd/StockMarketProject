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
        private readonly IBalanceRepository _balanceDal;

        public UserManager(UserManager<AppUser> userManager, IUserRepository userRepository, IBalanceRepository balanceDal)
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

        public async Task DeleteUser(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task<AppUser> GetUserById(int userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }

        public async Task UpdateUser(AppUser user)
        {
            await _userManager.UpdateAsync(user);
        }
    }

}
