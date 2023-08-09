using StockMarket.Entities.Concrete;

namespace StockMarket.Business.Concrete
{
    public interface IUserService
    {
        Task CreateUser(string username, string password);
        Task UpdateUser(AppUser user);
        Task DeleteUser(int userId);
        Task<AppUser> GetUserById(int userId);
        // Diğer kullanıcı işlemleri için gerekli metotları buraya ekleyebilirsiniz.
    }
}