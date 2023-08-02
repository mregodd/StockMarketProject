using StockMarket.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.DataAccess.Abstract
{
    public interface IUserRepository
    {
        Task CreateUser(string usernumber, string password);
        Task<User> GetUserById(int userId);
        Task UpdateUser(User user);

        Task DeleteUser(int userId);
    }

}
