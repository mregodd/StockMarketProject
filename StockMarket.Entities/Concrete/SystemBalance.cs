﻿using Microsoft.EntityFrameworkCore;
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
    public class SystemBalance
    {
        public int Id { get; set; }
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; } = 0; 
    }
}
