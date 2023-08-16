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

        public Task<bool> BuyStock(string userId, string symbol, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SellStock(string userId, string symbol, int quantity)
        {
            throw new NotImplementedException();
        }

    }

}
