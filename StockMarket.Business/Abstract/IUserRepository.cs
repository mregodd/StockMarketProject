using StockMarket.Entities.Concrete;

namespace StockMarket.Business.Concrete
{
    public interface IUserRepository
    {
       
        
        Task<AppUser> GetUserById(int userId);
        Task CreateUser(AppUser user);

        Task UpdateUser(AppUser user);

        Task DeleteUser(int userId);
        Task SaveChangesAsync();
        void AddUser(AppUser user);
    }
}