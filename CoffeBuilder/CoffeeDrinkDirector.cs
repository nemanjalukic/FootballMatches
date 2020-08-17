using CoffeBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeBuilder
{
    class CoffeeDrinkDirector
    {
        public Drink MakeDrink(CoffeeDrinkBuilder drinkBuilder)
        {
            drinkBuilder.CreateDrink();
            drinkBuilder.SetDrinkType();
            drinkBuilder.SetSugar();
            drinkBuilder.SetCoffe();
            drinkBuilder.SetLiquid();
            drinkBuilder.SetMilk();

            return drinkBuilder.GetDrink();
        }
    
}
}
