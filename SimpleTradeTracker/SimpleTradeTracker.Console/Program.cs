﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTradeTracker.Client;
using System.Data.Entity;

namespace SimpleTradeTracker.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new DatabaseContext())
            {

                var traders = db.Trader.Select(t => t).ToList();
                var products = db.Product.Select(t => t).ToList();

                string selectedProductName = "Mdma";
                string selectedTraderName = "Murat Bayan";

                var trade = new Trade { Price = 10.3, Quantity = 4, Product = products.FirstOrDefault(e => e.Name == selectedProductName), Trader = traders.FirstOrDefault(e => e.Name == selectedTraderName), type = TradeType.Buy };
                db.Trade.Add(trade);

                db.SaveChanges();
                //var traders = from b in db.Trader
                //            orderby b.Name
                //            select b;
                //var product = from b in db.Trader
                //            orderby b.Name
                //            select b;
                // Create and save a new Trader 
                //System.Console.Write("Enter a name for a new trader: ");
                //var name = System.Console.ReadLine();

                //var traderList = new List<string>()
                //{
                //    "Murat Bayan","Aylin Enginova","Xalbat Urtizverea","Antonio Sbraga"
                //};

                //var productList = new List<string>()
                //{
                //    "Cocaine", "Mdma", "Acid", "Ketamine"
                //};

                //foreach(var traderName in traderList)
                //{
                //    var trader = new Trader { Name = traderName };
                //    db.Trader.Add(trader);
                //}

                //var products = productList.Select(p => new Product() { Name = p });

                //db.Product.AddRange(products);
                //db.SaveChanges();
                //foreach(var productName in productList)
                //{
                //    var product = new Product { Name = productName };
                //    db.Product.Add(product);
                //}
                //var trader = new Trader { Name = name };
                //db.Trader.Add(trader);
                //db.SaveChanges();



                // Display all Blogs from the database 
                //var query = from b in db.Trader
                //            orderby b.Name
                //            select b;

                //System.Console.WriteLine("All blogs in the database:");
                //foreach (var item in query)
                //{
                //    System.Console.WriteLine(item.Name);
                //    if (item.Name == "Bob")
                //    {
                //        db.Trader.Remove(item);
                //    }
                //}

                System.Console.WriteLine("Press any key to exit...");
                System.Console.ReadKey();
            }
        }
    }
}
