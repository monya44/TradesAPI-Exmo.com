using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trades.Model
{
    public class TradesModel
    {
        [DisplayName("Trade_id")]
        public int trade_id { get; set; }
        [DisplayName("Date")]
        public int date { get; set; }
        [DisplayName("Type")]
        public string type { get; set; }
        [DisplayName("Quantity")]
        public string quantity { get; set; }
        [DisplayName("Price")]
        public string price { get; set; }
        [DisplayName("Amount")]
        public string amount { get; set; }
    }
}
