using StockMarket.Business.Abstract;
using StockMarket.DataAccess.Abstract;
using StockMarket.Entities.Concrete;
using System;
using System.Threading.Tasks;

namespace StockMarket.Business.Concrete
{
    public class StockTransactionManager : IStockDataTransactionService
    {
        private readonly IStockDataRepository _stockRepository;
        private readonly IBalanceRepository _balanceRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IStockTransactionRepository _transactionRepository;

        public StockTransactionManager(IStockDataRepository stockRepository, IBalanceRepository balanceRepository, IStockTransactionRepository stockTransactionRepository, IPortfolioRepository portfolioRepository)
        {
            _stockRepository = stockRepository;
            _balanceRepository = balanceRepository;
            _transactionRepository = stockTransactionRepository;
            _portfolioRepository = portfolioRepository;
        }

        public async Task<bool> BuyStock(int userId, string symbol, int quantity)
        {
            var stock = _stockRepository.GetStockBySymbol(symbol);

            if (stock == null)
            {
                return false; // Hissenin verisi yoksa işlem yapılamaz
            }

            var totalPrice = stock.Price * quantity;
            var userBalance = _balanceRepository.GetUserBalance(userId);

            if (userBalance == null || userBalance.Balance < totalPrice)
            {
                return false; // Yetersiz bakiye veya kullanıcı hesabı yoksa işlem yapılamaz
            }

            // Kullanıcının bakiyesini güncelle
            userBalance.Balance -= totalPrice;
            _balanceRepository.UpdateUserBalance(userBalance);

            // İşlem kaydını oluştur
            var transaction = new StockTransaction
            {
                UserId = userId,
                Symbol = symbol,
                Type = TransactionType.Buy,
                Quantity = quantity,
                Price = stock.Price,
                TransactionDate = DateTime.UtcNow
            };
            await _transactionRepository.AddTransactionAsync(transaction);

            return true;
        }

        public async Task<bool> SellStock(int userId, string symbol, int quantity)
        {
            var stock = _stockRepository.GetStockBySymbol(symbol);

            if (stock == null)
            {
                return false; // Hissenin verisi yoksa işlem yapılamaz
            }

            var userBalance = _balanceRepository.GetUserBalance(userId);

            if (userBalance == null)
            {
                return false; // Kullanıcı hesabı yoksa işlem yapılamaz
            }

            var userStockQuantity = _portfolioRepository.GetStockQuantityForUser(userId, symbol);

            if (userStockQuantity < quantity)
            {
                return false; // Kullanıcının yeterli miktarda hissesi yoksa işlem yapılamaz
            }

            // Kullanıcının bakiyesini güncelle
            var totalPrice = stock.Price * quantity;
            userBalance.Balance += totalPrice;
            _balanceRepository.UpdateUserBalance(userBalance);

            // İşlem kaydını oluştur
            var transaction = new StockTransaction
            {
                UserId = userId,
                Symbol = symbol,
                Type = TransactionType.Sell,
                Quantity = quantity,
                Price = stock.Price,
                TransactionDate = DateTime.UtcNow
            };
            await _transactionRepository.AddTransactionAsync(transaction);

            return true;
        }
    }
}
