using CoffeeBuilder;
using System;

namespace CoffeBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            Drink drink;
            CoffeeDrinkDirector drinkDirector = new CoffeeDrinkDirector();

            CappuccinoBuilder cappuccino = new CappuccinoBuilder();
            drink = drinkDirector.MakeDrink(cappuccino);
            Console.WriteLine(drink.ShowDrink());
            IrisCoffeeBuilder irisCoffee = new IrisCoffeeBuilder();
            drink = drinkDirector.MakeDrink(irisCoffee);
            Console.WriteLine(drink.ShowDrink());

            Console.ReadKey();
        }
    }
}
