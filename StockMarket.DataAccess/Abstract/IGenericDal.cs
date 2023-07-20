using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.DataAccess.Abstract
{
    public interface IGenericDal<T> where T : class //T değerine class yollayacağımızı söyledik, CRUD sistemini kullandık
    {
        void Insert(T t);
        void Delete(T t);   
        void Update(T t);   
        T GetById(int id);
        List<T> GetList();
    }
}
