using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace StockPricesAnalizer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the current directory.
            string path = Directory.GetCurrentDirectory();
            string filePath = path + '\\' + "stock.csv";
            string resultPath = path + '\\' + "result.csv";



            stockAnalyser(filePath, resultPath);


        }

        public static void stockAnalyser(string filePath, string resultPath)
        {
            List<string> lines = File.ReadAllLines(filePath).ToList();
            List<Stock> stocks = new List<Stock>();
            foreach (var item in lines)
            {
                string[] stock = item.Split(',');
                stocks.Add(new Stock
                {
                    Id = Int32.Parse(stock[0]),
                    Price = Double.Parse(stock[1]),
                    Time = DateTime.Parse(stock[2])

                });
            }
            var query = stocks.GroupBy(s => s.Id, s => s, (id, st) =>
                new
                {
                    Id = id,
                    Mp = Stock.getMaxProfit(st)
                });
            List<string> output = new List<string>();

            foreach (var result in query)
            {
                output.Add(result.Mp.ToString());
            }

            File.WriteAllLines(resultPath, output);

        }
    }
}
