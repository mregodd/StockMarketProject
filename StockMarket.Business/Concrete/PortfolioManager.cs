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

        public async Task AddPortfolioAsync(UserPortfolio portfolio)
        {
            var existingPortfolio = await _portfolioRepository.GetPortfolioByUserIdAndStockAsync(portfolio.AppUserId, portfolio.StockName);
            if (existingPortfolio != null)
            {
                throw new InvalidOperationException("Aynı hisse senedi zaten portföyde.");
            }

            await _portfolioRepository.AddPortfolioAsync(portfolio);
        }

        public async Task<UserPortfolio> GetPortfolioByUserIdAsync(int userId)
        {
            var portfolio = await _portfolioRepository.GetPortfolioByUserIdAsync(userId);
            if (portfolio == null)
            {
                throw new InvalidOperationException("Kullanıcının portföyü bulunmamaktadır.");
            }

            return portfolio;
        }

        public async Task UpdatePortfolioAsync(UserPortfolio portfolio)
        {
            var existingPortfolio = await _portfolioRepository.GetPortfolioByIdAsync(portfolio.Id);
            if (existingPortfolio == null)
            {
                throw new InvalidOperationException("Portföy bulunamadı.");
            }

            if (existingPortfolio.AppUserId != portfolio.AppUserId)
            {
                throw new InvalidOperationException("Bu portföy kullanıcısına ait değil.");
            }

            await _portfolioRepository.UpdatePortfolioAsync(portfolio);
        }

        public async Task DeletePortfolioAsync(int portfolioId)
        {
            var existingPortfolio = await _portfolioRepository.GetPortfolioByIdAsync(portfolioId);
            if (existingPortfolio == null)
            {
                throw new InvalidOperationException("Portföy bulunamadı.");
            }

            await _portfolioRepository.DeletePortfolioAsync(portfolioId);
        }

        public async Task<UserPortfolio> GetPortfolioByUserIdAndStockAsync(int userId, string stockName)
        {
            var portfolio = await _portfolioRepository.GetPortfolioByUserIdAndStockAsync(userId, stockName);
            if (portfolio == null)
            {
                throw new InvalidOperationException("Kullanıcının belirtilen hisse senedi portföyü bulunmamaktadır.");
            }

            return portfolio;
        }

        public async Task<UserPortfolio> GetPortfolioByIdAsync(int id)
        {
            var portfolio = await _portfolioRepository.GetPortfolioByIdAsync(id);
            if (portfolio == null)
            {
                throw new InvalidOperationException("Portföy bulunamadı.");
            }

            return portfolio;
        }

        public async Task<int> GetStockQuantityForUserAsync(int userId, string symbol)
        {
            var userPortfolio = await _portfolioRepository.GetPortfolioByUserIdAsync(userId);
            if (userPortfolio == null)
            {
                return 0; // Kullanıcının portfolyosu yoksa hissesi de yok demektir.
            }

            var stock = userPortfolio.StockData.FirstOrDefault(s => s.Symbol == symbol);
            if (stock == null)
            {
                return 0; // Kullanıcının belirtilen sembolde hisse senedi yoksa miktar 0'dır.
            }

            var stockQuantity = stock.Quantity;
            return stockQuantity;
        }
    }
}
