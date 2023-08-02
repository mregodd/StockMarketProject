using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StockMarket.Entities.Concrete
{
    public class AppUser : IdentityUser<int> //aspnetuser ile bağlantı kurup düzenlemek için oluşturduk
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<User> Users { get; set; }
        public decimal UserBalance { get; set; }



    }
}
