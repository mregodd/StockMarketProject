using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Entities.Concrete
{
    public class StockTransaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Symbol { get; set; }
        public TransactionType Type { get; set; } // Enum: Buy or Sell
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime TransactionDate { get; set; }
    }

    public enum TransactionType
    {
        Buy,
        Sell
    }

}
