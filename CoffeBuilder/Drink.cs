using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeBuilder
{
    public class Drink
    {
        public string Coffee { get; set; }
        public string Liquid { get; set; }
        public string Milk { get; set; }
        public string Sugar { get; set; }
        public int CoffeeAmount { get; set; }
        public int LiquidAmount { get; set; }
        public int MilkAmount { get; set; }
        public int SugarAmount { get; set; }

        public string Name { get; set; }

        public string ShowDrink()
        {
            return  Name + "=>" + LiquidAmount + " ml of " +Liquid+", " + MilkAmount + " ml of " +Milk +", "
                + SugarAmount + " gm of " +Sugar +", "+ CoffeeAmount + " " + Coffee +"\n";
        }
    }
}
