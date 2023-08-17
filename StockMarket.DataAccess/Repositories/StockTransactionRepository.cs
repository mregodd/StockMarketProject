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
    public class StockTransactionRepository : IStockTransactionRepository
    {
        private readonly Context _context;

        public StockTransactionRepository(Context context)
        {
            _context = context;
        }

        public async Task AddTransactionAsync(StockTransaction transaction)
        {
            _context.StockTransactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> BuyStock(int userId, string symbol, int quantity)
        {
            var stock = await _context.StockDatas.FirstOrDefaultAsync(s => s.Symbol == symbol);

            if (stock == null)
            {
                return false; // Hissenin verisi yoksa işlem yapılamaz
            }

            var totalPrice = stock.Price * quantity;
            var userBalance = await _context.UserBalances.FirstOrDefaultAsync(b => b.AppUserId == userId);

            if (userBalance == null || userBalance.Balance < totalPrice)
            {
                return false; // Yetersiz bakiye veya kullanıcı hesabı yoksa işlem yapılamaz
            }

            // Kullanıcının bakiyesini güncelle
            userBalance.Balance -= totalPrice;

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

            _context.StockTransactions.Add(transaction);
            await _context.SaveChangesAsync();
            var userPortfolio = await _context.UserPortfolios
            .Include(up => up.StockData)
            .FirstOrDefaultAsync(p => p.AppUserId == userId);

            userPortfolio.StockData.Add(stock); 
            await _context.SaveChangesAsync();


            return true;
        }

        public async Task<bool> SellStock(int userId, string symbol, int quantity)
        {
            var stock = await _context.StockDatas.FirstOrDefaultAsync(s => s.Symbol == symbol);

            if (stock == null)
            {
                return false; // Hissenin verisi yoksa işlem yapılamaz
            }

            var userBalance = await _context.UserBalances.FirstOrDefaultAsync(b => b.AppUserId == userId);

            if (userBalance == null)
            {
                return false; // Kullanıcı hesabı yoksa işlem yapılamaz
            }

            var userStockQuantity = await _context.UserPortfolios
                .Where(p => p.AppUserId == userId && p.StockData.Any(sd => sd.Symbol == symbol))
                .Select(p => p.Quantity)
                .FirstOrDefaultAsync();

            if (userStockQuantity < quantity)
            {
                return false; // Kullanıcının yeterli miktarda hissesi yoksa işlem yapılamaz
            }

            // Kullanıcının bakiyesini güncelle
            var totalPrice = stock.Price * quantity;
            userBalance.Balance += totalPrice;

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

            _context.StockTransactions.Add(transaction);
            await _context.SaveChangesAsync();

            var userPortfolio = await _context.UserPortfolios
            .Include(up => up.StockData)
            .FirstOrDefaultAsync(p => p.AppUserId == userId);

            userPortfolio.StockData.Add(stock);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
