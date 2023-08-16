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
    public class StockDataRepository : IStockDataRepository
    {
        private readonly Context _context; // DbContext nesnesi

        public StockDataRepository(Context context)
        {
            _context = context;
        }

        public void AddStock(StockData stockData)
        {
            _context.StockDatas.Add(stockData);
            _context.SaveChanges();
        }

        public void UpdateStock(StockData stockData)
        {
            _context.StockDatas.Update(stockData);
            _context.SaveChanges();
        }
        public void DeleteStock(StockData stockData)
        {
            _context.StockDatas.Remove(stockData);
            _context.SaveChanges();
        }

        public StockData GetStockBySymbol(string symbol)
        {
            return _context.StockDatas.FirstOrDefault(s => s.Symbol == symbol);
        }
        public StockData GetStockByName(string name)
        {
            return _context.StockDatas.FirstOrDefault(s => s.StockName == name);
        }
    }

}
