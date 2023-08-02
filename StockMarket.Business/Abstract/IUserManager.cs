using StockMarket.Entities.Concrete;

namespace StockMarket.Business.Concrete
{
    public interface IUserManager
    {
        Task CreateUser(string username, string password);
        Task UpdateUser(User user);
        Task DeleteUser(int userId);
        Task<User> GetUserById(int userId);
        // Diğer kullanıcı işlemleri için gerekli metotları buraya ekleyebilirsiniz.
    }
}