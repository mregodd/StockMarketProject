using StockMarket.Business.Abstract;
using StockMarket.DataAccess.Abstract;
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
        private readonly IUserDal _UserDal; //dependency injection uyguladık

        public UserManager(IUserDal userDal)
        {
            _UserDal = userDal;
        }

        public void TDelete(User t)
        {
            _UserDal.Delete(t);
        }

        public User TGetById(int id)
        {
            return _UserDal.GetById(id);
        }

        public List<User> TGetList()
        {
            return _UserDal.GetList();
        }

        public void TInsert(User t)
        {
            _UserDal.Insert(t);
        }

        public void TUpdate(User t)
        {
            _UserDal.Update(t); 
        }
    }
}
