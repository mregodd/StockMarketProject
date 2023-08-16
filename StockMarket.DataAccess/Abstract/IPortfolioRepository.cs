using StockMarket.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.DataAccess.Abstract
{
    public interface IPortfolioRepository
    {
        void AddPortfolio(UserPortfolio portfolio);
        UserPortfolio GetPortfolioByUserId(int userId);
        void UpdatePortfolio(UserPortfolio portfolio);
        void DeletePortfolio(int portfolioId);
        UserPortfolio GetPortfolioByUserIdAndStock(int userId, string stockName);
        UserPortfolio GetPortfolioById(int id);
        int GetStockQuantityForUser(int userId, string symbol);
    }
}
