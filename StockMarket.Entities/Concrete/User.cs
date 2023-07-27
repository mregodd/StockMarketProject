using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Entities.Concrete
{
    public class User //kullanıcı bilgileri
    {
        public int UserID { get; set; }
        public string UserNumber { get; set; }
        public decimal UserBalance { get; set; }
        public int AppUserID { get; set; }
        public AppUser AppUser { get; set; } //appuser ile bağlantı kurduk
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
