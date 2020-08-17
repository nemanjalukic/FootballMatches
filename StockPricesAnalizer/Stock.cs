using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockPricesAnalizer
{
    class Stock
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return (Id.ToString() + " " + Time.ToString() + " "+ Price.ToString());
        }

        public static MaxProfit getMaxProfit(IEnumerable<Stock> s)
        {
            List<Stock> stocks = s.ToList();
            stocks = stocks.OrderBy(s => s.Time).ToList();
            int byingIndex=0;
            int sellingIndex = 0;
            double maxProfit = 0;
            int byingIndexTmp = 0;
            double lowestPrice = stocks.ElementAt(0).Price;

            for (int  i = 1; i < stocks.Count; i++)
            {

                double  price = stocks.ElementAt(i).Price;
                double tmp = lowestPrice;
                if (price < lowestPrice)
                {
                    lowestPrice = price;
                    byingIndexTmp = i;
                }

                double profit = price - lowestPrice;
                if (profit > maxProfit)
                {
                    maxProfit = profit;
                    sellingIndex = i;
                    byingIndex = byingIndexTmp;

                }
                    


            }
            MaxProfit mp = new MaxProfit { Profit = maxProfit, MaxPrice = stocks.ElementAt(sellingIndex).Price, MinPrice = stocks.ElementAt(byingIndex).Price , SellingTime = stocks.ElementAt(sellingIndex).Time, BuyingTime = stocks.ElementAt(byingIndex).Time, Id=stocks.ElementAt(0).Id };
            return mp;
        }
    }
}
