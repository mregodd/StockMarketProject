using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Entities.Concrete
{ 
    public class SystemBalance
    {
    public int Id { get; set; }
    public decimal Balance { get; set; } = 0; // Varsayılan olarak sıfır bakiye atıyoruz
    }
}
