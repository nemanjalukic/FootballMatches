using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeBuilder
{
    class CappuccinoBuilder : CoffeeDrinkBuilder
    {
        public override void SetCoffe()
        {
            GetDrink().Coffee = "tea spons of coffee powder.";
            GetDrink().CoffeeAmount = 2;
            Console.WriteLine("Adding " + GetDrink().CoffeeAmount + "  " + GetDrink().Coffee);
        }

        public override void SetDrinkType()
        {
            GetDrink().Name = "Cappuccino";
            Console.WriteLine(GetDrink().Name);
        }

        public override void SetMilk()
        {
            GetDrink().Milk = "milk";
            GetDrink().MilkAmount = 50;
            Console.WriteLine("Adding " + GetDrink().MilkAmount + " ml of " + GetDrink().Milk);
        }

        public override void SetSugar()
        {
            GetDrink().Sugar = "white sugar";
            GetDrink().SugarAmount = 10;
            Console.WriteLine("Adding " + GetDrink().SugarAmount + " gr of " + GetDrink().Sugar);
        }

        public override void SetLiquid()
        {
            GetDrink().Liquid = "hot water";
            GetDrink().LiquidAmount = 100;
            Console.WriteLine("Adding " + GetDrink().LiquidAmount + " ml of " + GetDrink().Liquid);
        }
    }
}
