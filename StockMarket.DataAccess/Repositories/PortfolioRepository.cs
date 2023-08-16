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

        public void AddPortfolio(UserPortfolio portfolio)
        {
            _context.UserPortfolios.Add(portfolio);
            _context.SaveChanges();
        }

        public UserPortfolio GetPortfolioByUserId(int userId)
        {
            return _context.UserPortfolios.FirstOrDefault(p => p.AppUserId == userId);
        }

        public void DeletePortfolio(int portfolioId)
        {
            var portfolio = _context.UserPortfolios.FirstOrDefault(p => p.Id == portfolioId);
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

        public UserPortfolio GetPortfolioByUserIdAndStock(int userId, string stockName)
        {
            return _context.UserPortfolios.FirstOrDefault(p => p.AppUserId == userId && p.StockName == stockName);
        }

        public UserPortfolio GetPortfolioById(int id)
        {
            return _context.UserPortfolios.FirstOrDefault(p => p.Id == id);
        }

        public int GetStockQuantityForUser(int userId, string symbol)
        {
            var userPortfolio = _context.UserPortfolios.FirstOrDefault(p => p.AppUserId == userId);
            if (userPortfolio == null)
            {
                return 0; // Kullanıcının portfolyosu yoksa hissesi de yok demektir.
            }

            var stock = userPortfolio.Stocks.FirstOrDefault(s => s.StockName == symbol);
            if (stock == null)
            {
                return 0; // Kullanıcının belirtilen sembolde hisse senedi yoksa miktar 0'dır.
            }

            var stockQuantity = stock.Quantity;
            return stockQuantity;
        }
    }
}
