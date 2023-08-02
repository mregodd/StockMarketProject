using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Entities.Concrete
{
    public class UserPortfolio
    {
        public int Id { get; set; }
        public string StockName { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public string UserID { get; set; }


    }
}
