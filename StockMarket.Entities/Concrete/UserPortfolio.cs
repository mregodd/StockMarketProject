using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Value { get; set; }

        // Navigasyon özellikleri
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<StockData> Stocks { get; set; }
    }

}
