using StockMarket.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Business.Abstract
{
    public interface IPortfolioManager
    {
        void AddPortfolio(UserPortfolio portfolio);
        UserPortfolio GetPortfolioByUserId(int userId);
        void UpdatePortfolio(UserPortfolio portfolio);
        void DeletePortfolio(int portfolioId);
    }
}
