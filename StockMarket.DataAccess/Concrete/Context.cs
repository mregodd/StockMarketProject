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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Admin rolü tanımı
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "ADMIN"
            });
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPortfolio> UserPortfolios { get; set; }    
        public DbSet<UserBalance> UserBalances { get; set; }   
        public DbSet<SystemBalance> SystemBalances { get; set; }    
    }

}
