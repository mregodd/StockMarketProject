using Microsoft.EntityFrameworkCore;
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

        public async Task AddPortfolioAsync(UserPortfolio portfolio)
        {
            _context.UserPortfolios.Add(portfolio);
            await _context.SaveChangesAsync();
        }

        public async Task<UserPortfolio> GetPortfolioByUserIdAsync(int userId)
        {
            return await Task.FromResult(_context.UserPortfolios.FirstOrDefault(p => p.AppUserId == userId));
        }

        public async Task DeletePortfolioAsync(int portfolioId)
        {
            var portfolio = await _context.UserPortfolios.FindAsync(portfolioId);
            if (portfolio != null)
            {
                _context.UserPortfolios.Remove(portfolio);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdatePortfolioAsync(UserPortfolio updatedPortfolio)
        {
            var portfolio = await _context.UserPortfolios.FindAsync(updatedPortfolio.Id);
            if (portfolio != null)
            {
                portfolio.StockName = updatedPortfolio.StockName;
                portfolio.Quantity = updatedPortfolio.Quantity;
                portfolio.Value = updatedPortfolio.Value;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<UserPortfolio> GetPortfolioByUserIdAndStockAsync(int userId, string stockName)
        {
            return await Task.FromResult(_context.UserPortfolios.FirstOrDefault(p => p.AppUserId == userId && p.StockName == stockName));
        }

        public async Task<UserPortfolio> GetPortfolioByIdAsync(int id)
        {
            return await Task.FromResult(_context.UserPortfolios.FirstOrDefault(p => p.Id == id));
        }

        public async Task<int> GetStockQuantityForUserAsync(int userId, string symbol)
        {
            var userPortfolio = await _context.UserPortfolios.FirstOrDefaultAsync(p => p.AppUserId == userId);
            if (userPortfolio == null)
            {
                return 0; // Kullanıcının portfolyosu yoksa hissesi de yok demektir.
            }

            var stock = userPortfolio.StockData.FirstOrDefault(s => s.StockName == symbol);
            if (stock == null)
            {
                return 0; // Kullanıcının belirtilen sembolde hisse senedi yoksa miktar 0'dır.
            }

            var stockQuantity = stock.Quantity;
            return stockQuantity;
        }
    }
}
