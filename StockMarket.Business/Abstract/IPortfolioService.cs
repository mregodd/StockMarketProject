using StockMarket.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Business.Abstract
{
    public interface IPortfolioService
    {
        Task AddPortfolioAsync(UserPortfolio portfolio);
        Task<UserPortfolio> GetPortfolioByUserIdAsync(int userId);
        Task UpdatePortfolioAsync(UserPortfolio portfolio);
        Task DeletePortfolioAsync(int portfolioId);
        Task<UserPortfolio> GetPortfolioByUserIdAndStockAsync(int userId, string stockName);
        Task<UserPortfolio> GetPortfolioByIdAsync(int id);
        Task<int> GetStockQuantityForUserAsync(int userId, string symbol);
    }
}
