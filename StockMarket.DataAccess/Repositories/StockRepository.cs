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
    public class StockRepository : IStockRepository
    {
        private readonly Context _context; // DbContext nesnesi

        public StockRepository(Context context)
        {
            _context = context;
        }

        public void AddStock(Stock stock)
        {
            _context.Stocks.Add(stock);
            _context.SaveChanges();
        }

        public void UpdateStock(Stock stock)
        {
            _context.Stocks.Update(stock);
            _context.SaveChanges();
        }
        public void DeleteStock(Stock stock)
        {
            _context.Stocks.Remove(stock);
            _context.SaveChanges();
        }

        public Stock GetStockBySymbol(string symbol)
        {
            return _context.Stocks.FirstOrDefault(s => s.Symbol == symbol);
        }
        public Stock GetStockByName(string name)
        {
            return _context.Stocks.FirstOrDefault(s => s.StockName == name);
        }
    }

}
