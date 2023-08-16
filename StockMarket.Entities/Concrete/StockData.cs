using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Entities.Concrete
{
    public class StockData
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string StockName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

}
