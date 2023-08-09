using StockMarket.DataAccess.Abstract;
using StockMarket.DataAccess.Concrete;
using StockMarket.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.DataAccess.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly Context _context;

        public PortfolioRepository(Context context)
        {
            _context = context;
        }

        // Hisse senedi bilgilerini eklemek için metot
        public void AddPortfolio(UserPortfolio portfolio)
        {
            _context.UserPortfolios.Add(portfolio);
            _context.SaveChanges();
        }

        public UserPortfolio GetPortfolioByUserId(int id)
        {
            var portfolio = _context.UserPortfolios.FirstOrDefault(p => p.AppUserId == id);
            return portfolio;
        }
        public void DeletePortfolio(int id)
        {
            var portfolio = _context.UserPortfolios.FirstOrDefault(p => p.Id == id);
            if (portfolio != null)
            {
                _context.UserPortfolios.Remove(portfolio);
                _context.SaveChanges();
            }
        }

        public void UpdatePortfolio(UserPortfolio updatedPortfolio)
        {
            var portfolio = _context.UserPortfolios.FirstOrDefault(p => p.Id == updatedPortfolio.Id);
            if (portfolio != null)
            {
                portfolio.StockName = updatedPortfolio.StockName;
                portfolio.Quantity = updatedPortfolio.Quantity;
                portfolio.Value = updatedPortfolio.Value;
                _context.SaveChanges();
            }

        }

    }
}

