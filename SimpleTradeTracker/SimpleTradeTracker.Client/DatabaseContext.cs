using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SimpleTradeTracker.Client
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Trade> Trade { get; set; }
        public DbSet<Trader> Trader { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}
