using System;
using System.Collections.Generic;
using System.Linq;

namespace Trades.Model
{
    public class ListofTrades
    {
        public List<TradesModel> BTC_USD { get; set; }
        public List<TradesModel> LTC_USD { get; set; }
        public List<TradesModel> ETH_USD { get; set; }
        public List<TradesModel> USDT_USD { get; set; }
    }
}
