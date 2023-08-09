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
    public class SystemBalanceRepository : ISystemBalanceRepository
    {
        private readonly Context _context;

        public SystemBalanceRepository(Context context)
        {
            _context = context;
        }

        public decimal GetSystemBalance()
        {
            // Veritabanından sistem bakiyesini çekme işlemi
            var systemBalance = _context.SystemBalances.FirstOrDefault(); 
            if (systemBalance != null)
            {
                return systemBalance.Balance;
            }
            return 0; // Varsayılan değer
        }

        public void UpdateSystemBalance(decimal newBalance)
        {
            // Veritabanında sistem bakiyesini güncelleme işlemi
            var systemBalance = _context.SystemBalances.FirstOrDefault(); 
            if (systemBalance != null)
            {
                systemBalance.Balance = newBalance;
                _context.SaveChanges();
            }
            else
            {
                // Tablo boşsa, yeni bir kayıt oluşturabilirsiniz
                systemBalance = new SystemBalance { Balance = newBalance }; 
                _context.SystemBalances.Add(systemBalance);
                _context.SaveChanges();
            }
        }
    }
}
