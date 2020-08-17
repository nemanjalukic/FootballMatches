using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeBuilder
{
    class IrisCoffeeBuilder:CoffeeDrinkBuilder
    {
        public override void SetCoffe()
        {
            GetDrink().Coffee = "cup of esspreso coffee.";
            GetDrink().CoffeeAmount = 1;
            Console.WriteLine("Adding " + GetDrink().CoffeeAmount + "  " + GetDrink().Coffee);
        }

        public override void SetDrinkType()
        {
            GetDrink().Name = "Irish coffee";
            Console.WriteLine(GetDrink().Name);
        }

        public override void SetMilk()
        {
            GetDrink().Milk = "fresh cream";
            GetDrink().MilkAmount = 60;
            Console.WriteLine("Adding " + GetDrink().MilkAmount + " ml of " + GetDrink().Milk);
        }

        public override void SetSugar()
        {
            GetDrink().Sugar = "brown sugar";
            GetDrink().SugarAmount = 15;
            Console.WriteLine("Adding " + GetDrink().SugarAmount + " gr of " + GetDrink().Sugar);
        }

        public override void SetLiquid()
        {
            GetDrink().Liquid = " Irish whiskey";
            GetDrink().LiquidAmount = 50;
            Console.WriteLine("Adding " + GetDrink().LiquidAmount + " ml of " + GetDrink().Liquid);
        }
    }
}

