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
    public class UserBalance
    {
        [Key]
        public int AppUserId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }
        public AppUser AppUser { get; set; } // AppUser ile ilişkili navigasyon özelliği

    }
}
