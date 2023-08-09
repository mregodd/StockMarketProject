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
        [Key]
        public int Id { get; set; } // UserPortfolio için bir benzersiz kimlik sütunu ekledik
        public string StockName { get; set; }
        public int Quantity { get; set; }
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Value { get; set; }
        public int AppUserId { get; set; } // Bu özellik AppUser ile ilişkilendirilmiş kullanıcının kimliğini temsil edecek
        public AppUser AppUser { get; set; } // Kullanıcının ait olduğu AppUser nesnesi
    }
}
