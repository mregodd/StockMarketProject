using StockMarket.DataAccess.Concrete;
using StockMarket.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.DataAccess.Repositories
{
    public class PortfolioManager
    {
        private readonly Context _context;

        public PortfolioManager(Context context)
        {
            _context = context;
        }

        // Hisse senedi bilgilerini eklemek için metot
        public void AddPortfolio(UserPortfolio portfolio)
        {
            _context.UserPortfolios.Add(portfolio);
            _context.SaveChanges();
        }

        public object GetPortfolioByUserId(int ıd)
        {
            throw new NotImplementedException();
        }

        // Diğer portföy işlemleri için metotlar buraya eklenebilir
    }

}

