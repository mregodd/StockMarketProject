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
    public class UserProcessManager : IUserProcess 
    {
        private readonly IUserProcessDal _userProcessDal;   //dependency injection uyguladık

        public UserProcessManager(IUserProcessDal userProcessDal)
        {
            _userProcessDal = userProcessDal;   
        }
        public void TDelete(UserProcess t)
        {
            _userProcessDal.Delete(t);
        }

        public UserProcess TGetById(int id)
        {
            return _userProcessDal.GetById(id); 
        }

        public List<UserProcess> TGetList()
        {
            return _userProcessDal.GetList();
        }

        public void TInsert(UserProcess t)
        {
            _userProcessDal.Insert(t);  
        }

        public void TUpdate(UserProcess t)
        {
            _userProcessDal.Update(t);
        }
    }
}
