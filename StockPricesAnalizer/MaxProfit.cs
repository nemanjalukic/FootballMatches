using System;
using System.Collections.Generic;
using System.Text;

namespace StockPricesAnalizer
{
    class MaxProfit
    {
        public int Id { get; set; }
        public double Profit { get; set; }
        public DateTime BuyingTime { get; set; }
        public DateTime SellingTime { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }

        public override string ToString()
        {
            string s = Profit + ", " + Id + ", " + MinPrice + ", " + MaxPrice + ", " + BuyingTime.ToString() + ", " + BuyingTime.ToString();
            return s;
        }
    }
}
