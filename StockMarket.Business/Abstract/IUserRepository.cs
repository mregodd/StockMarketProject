using StockMarket.Entities.Concrete;

namespace StockMarket.Business.Concrete
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int userId);
        Task CreateUser(User user);

        Task UpdateUser(User user);

        Task DeleteUser(int userId);
    }
}