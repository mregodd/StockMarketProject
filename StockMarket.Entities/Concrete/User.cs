using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Entities.Concrete
{
    public class User : IdentityUser
    {
        public int UserID { get; set; }
        public string UserNumber { get; set; }
        public decimal UserBalance { get; set; } = 0; // Varsayılan olarak sıfır bakiye atıyoruz


    }
}
