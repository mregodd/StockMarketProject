using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Entities.Concrete
{
    public class UserBalance
    {
        [Key]
        public int AppUserID { get; set; }
        public decimal Balance { get; set; }
        
    }
}
