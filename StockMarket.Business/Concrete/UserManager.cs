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
    public class UserManager : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IBalanceRepository _balanceRepository;

        public UserManager(UserManager<AppUser> userManager, IUserRepository userRepository, IBalanceRepository balanceRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
            _balanceRepository = balanceRepository;
        }

        public async Task CreateUser(string username, string password)
        {
            var user = new AppUser
            {
                UserName = username,
                UserBalance = 0
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var userBalance = new UserBalance
                {
                    AppUserId = user.Id,
                    Balance = 0
                };

                _balanceRepository.AddUserBalance(userBalance);
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
            var existingUser = await _userManager.FindByIdAsync(user.Id.ToString());

            if (existingUser != null)
            {
                existingUser.UserName = user.UserName;
                // Diğer özelliklerini de güncelleyebilirsiniz

                await _userManager.UpdateAsync(existingUser);
            }
        }
    }
}
