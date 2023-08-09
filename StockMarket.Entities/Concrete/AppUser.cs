using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StockMarket.Entities.Concrete
{
    public class AppUser : IdentityUser<int> //aspnetuser ile bağlantı kurup düzenlemek için oluşturduk
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal UserBalance { get; set; }
    }
}
