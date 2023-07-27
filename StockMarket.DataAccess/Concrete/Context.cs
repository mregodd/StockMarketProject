using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockMarket.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.DataAccess.Concrete
{
    public class Context : IdentityDbContext<AppUser, AppRole, int> // context nesnemizde appuser ve approle sınıfına bağlantı oluşturduk ve integer olmasını sağladık
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProcess> UserProcesses { get; set; }
    }

    public class User // kullanıcı bilgileri
    {
        public int UserID { get; set; }
        public string UserNumber { get; set; }
        public decimal UserBalance { get; set; }
    }

}
