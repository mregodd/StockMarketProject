﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Entities.Concrete
{
    public class User //kullanıcı bilgileri
    {
        public int UserID { get; set; }
        public string UserNumber { get; set; }
        public decimal UserBalance { get; set; }
        

    }
}
