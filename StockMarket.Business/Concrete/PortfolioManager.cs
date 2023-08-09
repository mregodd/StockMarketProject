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
    public class PortfolioManager : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioManager(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public void AddPortfolio(UserPortfolio portfolio)
        {
            var existingPortfolio = _portfolioRepository.GetPortfolioByUserIdAndStock(portfolio.AppUserId, portfolio.StockName);
            if (existingPortfolio != null)
            {
                throw new InvalidOperationException("Aynı hisse senedi zaten portföyde.");
            }

            _portfolioRepository.AddPortfolio(portfolio);
        }

        public UserPortfolio GetPortfolioByUserId(int userId)
        {
            var portfolio = _portfolioRepository.GetPortfolioByUserId(userId);
            if (portfolio == null)
            {
                throw new InvalidOperationException("Kullanıcının portföyü bulunmamaktadır.");
            }

            return portfolio;
        }

        public void UpdatePortfolio(UserPortfolio portfolio)
        {
            var existingPortfolio = _portfolioRepository.GetPortfolioById(portfolio.Id);
            if (existingPortfolio == null)
            {
                throw new InvalidOperationException("Portföy bulunamadı.");
            }

            if (existingPortfolio.AppUserId != portfolio.AppUserId)
            {
                throw new InvalidOperationException("Bu portföy kullanıcısına ait değil.");
            }

            _portfolioRepository.UpdatePortfolio(portfolio);
        }

        public void DeletePortfolio(int portfolioId)
        {
            var existingPortfolio = _portfolioRepository.GetPortfolioById(portfolioId);
            if (existingPortfolio == null)
            {
                throw new InvalidOperationException("Portföy bulunamadı.");
            }

            _portfolioRepository.DeletePortfolio(portfolioId);
        }

        public UserPortfolio GetPortfolioByUserIdAndStock(int userId, string stockName)
        {
            var portfolio = _portfolioRepository.GetPortfolioByUserIdAndStock(userId, stockName);
            if (portfolio == null)
            {
                throw new InvalidOperationException("Kullanıcının belirtilen hisse senedi portföyü bulunmamaktadır.");
            }

            return portfolio;
        }

        public UserPortfolio GetPortfolioById(int id)
        {
            var portfolio = _portfolioRepository.GetPortfolioById(id);
            if (portfolio == null)
            {
                throw new InvalidOperationException("Portföy bulunamadı.");
            }

            return portfolio;
        }
    }
}
