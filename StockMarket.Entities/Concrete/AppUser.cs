using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Entities.Concrete
{
    public class AppUser : IdentityUser<int> //aspnetuser ile bağlantı kurup düzenlemek için oluşturduk
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }    
        public string District { get; set; }
        public List<User> Users { get; set; } //user ile bağlantı kurduk



    }
}
