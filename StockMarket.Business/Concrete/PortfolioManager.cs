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
    public class PortfolioManager : IPortfolioManager
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioManager(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public void AddPortfolio(UserPortfolio portfolio)
        {
            _portfolioRepository.AddPortfolio(portfolio);
        }

        public UserPortfolio GetPortfolioByUserId(int userId)
        {
            return _portfolioRepository.GetPortfolioByUserId(userId);
        }

        public void UpdatePortfolio(UserPortfolio portfolio)
        {
            _portfolioRepository.UpdatePortfolio(portfolio);
        }

        public void DeletePortfolio(int portfolioId)
        {
            _portfolioRepository.DeletePortfolio(portfolioId);
        }
    }
}
