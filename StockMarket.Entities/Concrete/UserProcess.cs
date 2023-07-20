using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Entities.Concrete
{
    public class UserProcess //kullanıcıların hesap bilgileri
    {
        public int UserProcessID { get; set; }
        public string ProcessType { get; set; }
        public int Amount { get; set; }
        public DateTime ProcessDate { get; set; }
    }
}