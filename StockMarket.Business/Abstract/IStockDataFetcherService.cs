﻿using StockMarket.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Business.Abstract
{
    public interface IStockDataFetcherService
    {
        Task<StockData> FetchStockData(string symbol);
    }

}
