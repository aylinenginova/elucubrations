using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTradeTracker.Client
{
    public class Trader
    {
        public int TraderId { get; set; }
        public string Name { get; set; }
    }
}
