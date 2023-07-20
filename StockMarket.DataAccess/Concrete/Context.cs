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
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = DERBEDEK; Database = StockMarketDB; User Id = DBTest; Password = 112233; TrustServerCertificate = True;");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProcess> UserProcesses { get; set; }
    }
}
