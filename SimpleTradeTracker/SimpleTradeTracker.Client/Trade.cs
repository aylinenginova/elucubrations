using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTradeTracker.Client
{
    public enum TradeType
    {
       Buy,Sell
    }
    public class Trade
    {
        public int TradeId { get; set; }
        public Trader Trader { get; set; }
        public Product Product { get; set; }
        public double Price { get; set; }
        public Int32 Quantity { get; set; }
        public TradeType type { get; set;
        }
    }
}
