using CoffeBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeBuilder
{
        public abstract class CoffeeDrinkBuilder
        {
        protected Drink Drink;

        public void CreateDrink()
        {
            Drink = new Drink();
        }
        public Drink GetDrink()
        {
            return Drink;
        }

        public abstract void SetDrinkType();
        public abstract void SetLiquid();
        public abstract void SetMilk();
        public abstract void SetSugar();
        public abstract void SetCoffe();
    }
}
