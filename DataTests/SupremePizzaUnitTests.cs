using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xunit;
using Size = PizzaParlor.Data.Size;

namespace PizzaParlor.Tests
{
    /// <summary>
    /// Tests the SupremePizza class
    /// </summary>
    public class SupremePizzaTests
    {
        /// <summary>
        /// Tests all the default values for a Supreme Pizza
        /// </summary>
        [Fact]
        public void DefaultPropertyValuesTest()
        {
            SupremePizza pizza = new SupremePizza();

            Assert.Equal(Crust.Original, pizza.PizzaCrust);
            Assert.Equal(Size.Medium, pizza.PizzaSize);
            Assert.Equal("Supreme Pizza", pizza.Name);
            Assert.Equal("Your standard supreme with meats and veggies", pizza.Description);
            Assert.True(pizza.ContainsTopping(Topping.Sausage));
            Assert.True(pizza.ContainsTopping(Topping.Pepperoni));
            Assert.True(pizza.ContainsTopping(Topping.Onions));
            Assert.True(pizza.ContainsTopping(Topping.Peppers));
            Assert.True(pizza.ContainsTopping(Topping.Onions));
            Assert.True(pizza.ContainsTopping(Topping.Mushrooms));
            Assert.Equal(8u, pizza.Slices);
        }

        /// <summary>
        /// Tests the price calculation
        /// </summary>
        /// <param name="size">Size of the pizza</param>
        /// <param name="isDeepDish">Is the pizza deep dish</param>
        /// <param name="expectedPrice">The expected price of the pizza</param>
        [Theory]
        [InlineData(Size.Small, false, 11.99)]
        [InlineData(Size.Small, true, 12.99)]
        [InlineData(Size.Medium, false, 13.99)]
        [InlineData(Size.Medium, true, 14.99)]
        [InlineData(Size.Large, false, 15.99)]
        [InlineData(Size.Large, true, 16.99)]
        public void PriceCalculationTest(Size size, bool isDeepDish, decimal expectedPrice)
        {
            SupremePizza pizza = new SupremePizza();
            pizza.PizzaSize = size;
            pizza.PizzaCrust = Crust.Original;
            if (isDeepDish) pizza.PizzaCrust = Crust.DeepDish;

            decimal actualPrice = pizza.Price;

            Assert.Equal(expectedPrice, actualPrice);
        }

        /// <summary>
        /// Tests the calorie calculation
        /// </summary>
        /// <param name="sausage">Does this pizza contain sausage</param>
        /// <param name="pepperoni">Does this pizza contain pepperoni</param>
        /// <param name="olives">Does this pizza contain olives</param>
        /// <param name="peppers">Does this pizza contain peppers</param>
        /// <param name="onions">Does this pizza contain onions</param>
        /// <param name="mushrooms">Does this pizza contain mushrooms</param>
        /// <param name="size">Size of the pizza</param>
        /// <param name="crust">Type of crust</param>
        /// <param name="expectedCalories">Expected calorie count</param>
        [Theory]
        [InlineData(true, true, true, true, true, true, Size.Medium, Crust.Original,
            (uint)(1.0 * (250 + 30 + 20 + 5 + 5 + 5 + 5)))]
        [InlineData(true, true, true, true, true, true, Size.Small, Crust.Thin,
            (uint)(0.75 * (150 + 30 + 20 + 5 + 5 + 5 + 5)))]
        [InlineData(false, true, false, false, false, false, Size.Large, Crust.DeepDish,
            (uint)(1.3 * (300 + 20)))]
        [InlineData(true, false, true, false, true, false, Size.Medium, Crust.Original,
            (uint)(1.0 * (250 + 30 + 5 + 5)))]
        [InlineData(false, true, false, true, false, true, Size.Small, Crust.Thin,
            (uint)(0.75 * (150 + 20 + 5 + 5)))]
        [InlineData(true, false, true, false, true, false, Size.Large, Crust.DeepDish,
            (uint)(1.3 * (300 + 30 + 5 + 5)))]
        [InlineData(true, true, false, true, false, true, Size.Medium, Crust.DeepDish,
            (uint)(1.0 * (300 + 30 + 20 + 5 + 5)))]
        [InlineData(false, false, false, false, false, false, Size.Large, Crust.Original,
            (uint)(1.3 * 250))]
        public void CaloriesPerEachTest(bool sausage, bool pepperoni, bool olives, bool peppers, bool onions, bool mushrooms, Size size, Crust crust, uint expectedCalories)
        {
            SupremePizza pizza = new SupremePizza();
            pizza.EditToppings(Topping.Sausage, sausage);
            pizza.EditToppings(Topping.Pepperoni, pepperoni);
            pizza.EditToppings(Topping.Olives, olives);
            pizza.EditToppings(Topping.Peppers, peppers);
            pizza.EditToppings(Topping.Onions, onions);
            pizza.EditToppings(Topping.Mushrooms, mushrooms);
            pizza.PizzaSize = size;
            pizza.PizzaCrust = crust;

            uint actualCalories = pizza.CaloriesPerEach;

            Assert.Equal(expectedCalories, actualCalories);
        }

        /// <summary>
        /// Tests the total calorie calculation
        /// </summary>
        /// <param name="sausage">Does this pizza contain sausage</param>
        /// <param name="pepperoni">Does this pizza contain pepperoni</param>
        /// <param name="olives">Does this pizza contain olives</param>
        /// <param name="peppers">Does this pizza contain peppers</param>
        /// <param name="onions">Does this pizza contain onions</param>
        /// <param name="mushrooms">Does this pizza contain mushrooms</param>
        /// <param name="size">Size of the pizza</param>
        /// <param name="crust">Type of crust</param>
        /// <param name="expectedCalories">Expected total calorie count</param>
        [Theory]
        [InlineData(true, true, true, true, true, true, Size.Medium, Crust.Original,
            (uint)(8 * 1.0 * (250 + 30 + 20 + 5 + 5 + 5 + 5)))]
        [InlineData(true, true, true, true, true, true, Size.Small, Crust.Thin,
            (uint)(8 * 0.75 * (150 + 30 + 20 + 5 + 5 + 5 + 5)))]
        [InlineData(false, true, false, false, false, false, Size.Large, Crust.DeepDish,
            (uint)(8 * 1.3 * (300 + 20)))]
        [InlineData(true, false, true, false, true, false, Size.Medium, Crust.Original,
            (uint)(8 * 1.0 * (250 + 30 + 5 + 5)))]
        [InlineData(false, true, false, true, false, true, Size.Small, Crust.Thin,
            (uint)(8 * 0.75 * (150 + 20 + 5 + 5)))]
        [InlineData(true, false, true, false, true, false, Size.Large, Crust.DeepDish,
            (uint)(8 * 1.3 * (300 + 30 + 5 + 5)))]
        [InlineData(true, true, false, true, false, true, Size.Medium, Crust.DeepDish,
            (uint)(8 * 1.0 * (300 + 30 + 20 + 5 + 5)))]
        [InlineData(false, false, false, false, false, false, Size.Large, Crust.Original,
            (uint)(8 * 1.3 * 250))]
        public void CaloriesTotalTest(bool sausage, bool pepperoni, bool olives, bool peppers, bool onions, bool mushrooms, Size size, Crust crust, uint expectedCalories)
        {
            SupremePizza pizza = new SupremePizza();
            pizza.EditToppings(Topping.Sausage, sausage);
            pizza.EditToppings(Topping.Pepperoni, pepperoni);
            pizza.EditToppings(Topping.Olives, olives);
            pizza.EditToppings(Topping.Peppers, peppers);
            pizza.EditToppings(Topping.Onions, onions);
            pizza.EditToppings(Topping.Mushrooms, mushrooms);
            pizza.PizzaSize = size;
            pizza.PizzaCrust = crust;

            uint actualCalories = pizza.CaloriesTotal;

            Assert.Equal(expectedCalories, actualCalories);
        }

        /// <summary>
        /// Tests the Special Instructions
        /// </summary>
        /// <param name="sausage">Does this pizza contain sausage</param>
        /// <param name="pepperoni">Does this pizza contain pepperoni</param>
        /// <param name="olives">Does this pizza contain olives</param>
        /// <param name="peppers">Does this pizza contain peppers</param>
        /// <param name="onions">Does this pizza contain onions</param>
        /// <param name="mushrooms">Does this pizza contain mushrooms</param>
        /// <param name="size">Size of the pizza</param>
        /// <param name="crust">Type of crust</param>
        /// <param name="expectedInstructions">Expected instructions</param>
        [Theory]
        [InlineData(true, true, true, true, true, true, Size.Medium, Crust.Original,
            new string[] { "Medium", "Original" })]
        [InlineData(false, false, false, false, false, false, Size.Large, Crust.DeepDish,
            new string[] { "Large", "DeepDish", "Hold Sausage", "Hold Pepperoni", "Hold Olives", "Hold Peppers", "Hold Onions", "Hold Mushrooms" })]
        [InlineData(true, true, false, false, true, false, Size.Small, Crust.Thin,
            new string[] { "Small", "Thin", "Hold Olives", "Hold Peppers", "Hold Mushrooms" })]
        [InlineData(true, true, true, false, false, false, Size.Large, Crust.Original,
            new string[] { "Large", "Original", "Hold Peppers", "Hold Onions", "Hold Mushrooms" })]
        [InlineData(false, true, true, true, false, true, Size.Medium, Crust.Original,
            new string[] { "Medium", "Original", "Hold Sausage", "Hold Onions" })]
        [InlineData(false, false, false, false, false, false, Size.Large, Crust.Thin,
            new string[] { "Large", "Thin", "Hold Sausage", "Hold Pepperoni", "Hold Olives", "Hold Peppers", "Hold Onions", "Hold Mushrooms" })]
        [InlineData(true, true, true, false, false, true, Size.Small, Crust.Thin,
            new string[] { "Small", "Thin", "Hold Onions", "Hold Peppers" })]
        [InlineData(true, false, true, false, true, false, Size.Large, Crust.Original,
            new string[] { "Large", "Original", "Hold Pepperoni", "Hold Peppers", "Hold Mushrooms" })]
        public void SpecialInstructionsTest(bool sausage, bool pepperoni, bool olives, bool peppers, bool onions, bool mushrooms, Size size, Crust crust, string[] expectedInstructions)
        {
            SupremePizza pizza = new SupremePizza();
            pizza.EditToppings(Topping.Sausage, sausage);
            pizza.EditToppings(Topping.Pepperoni, pepperoni);
            pizza.EditToppings(Topping.Olives, olives);
            pizza.EditToppings(Topping.Peppers, peppers);
            pizza.EditToppings(Topping.Onions, onions);
            pizza.EditToppings(Topping.Mushrooms, mushrooms);
            pizza.PizzaSize = size;
            pizza.PizzaCrust = crust;

            foreach (string instruction in expectedInstructions)
            {
                Assert.Contains(instruction, pizza.SpecialInstructions);
            }
        }
        /// <summary>
        /// This item can be cast to it's base class type(s)
        /// </summary>
        [Fact]
        public void CastToIMenuItem()
        {
            SupremePizza item = new();
            Assert.IsAssignableFrom<IMenuItem>(item);
            Assert.IsAssignableFrom<Pizza>(item);
        }
        /// <summary>
        /// Tests if changing the size of the pizza notifies relevant variables
        /// </summary>
        /// <param name="size">Size of pizza</param>
        /// <param name="propertyName">Name of the property being tested</param>
        [Theory]

        [InlineData(Size.Small, "Price")]
        [InlineData(Size.Medium, "Price")]
        [InlineData(Size.Large, "Price")]

        [InlineData(Size.Small, "CaloriesTotal")]
        [InlineData(Size.Medium, "CaloriesTotal")]
        [InlineData(Size.Large, "CaloriesTotal")]

        [InlineData(Size.Small, "CaloriesPerEach")]
        [InlineData(Size.Medium, "CaloriesPerEach")]
        [InlineData(Size.Large, "CaloriesPerEach")]

        [InlineData(Size.Small, "SpecialInstructions")]
        [InlineData(Size.Medium, "SpecialInstructions")]
        [InlineData(Size.Large, "SpecialInstructions")]
        public void SizeChangeNotifyChanged(Size size, string propertyName)
        {
            SupremePizza item = new();
            Assert.PropertyChanged(item, propertyName, () => {
                item.PizzaSize = size;
            });
        }

        /// <summary>
        /// Tests if changing the crust of the pizza notifies relevant variables
        /// </summary>
        /// <param name="crust">Crust of pizza</param>
        /// <param name="propertyName">Name of the property being tested</param>
        [Theory]

        [InlineData(Crust.Thin, "Price")]
        [InlineData(Crust.DeepDish, "Price")]
        [InlineData(Crust.Original, "Price")]

        [InlineData(Crust.Thin, "CaloriesTotal")]
        [InlineData(Crust.DeepDish, "CaloriesTotal")]
        [InlineData(Crust.Original, "CaloriesTotal")]

        [InlineData(Crust.Thin, "CaloriesPerEach")]
        [InlineData(Crust.DeepDish, "CaloriesPerEach")]
        [InlineData(Crust.Original, "CaloriesPerEach")]

        [InlineData(Crust.Thin, "SpecialInstructions")]
        [InlineData(Crust.DeepDish, "SpecialInstructions")]
        [InlineData(Crust.Original, "SpecialInstructions")]
        public void CrustChangeNotifyChanged(Crust crust, string propertyName)
        {
            SupremePizza item = new();
            Assert.PropertyChanged(item, propertyName, () => {
                item.PizzaCrust = crust;
            });
        }

        /// <summary>
        /// Does it implement INotifyChanged
        /// </summary>
        [Fact]
        public void ShouldImplementINotifyChanged()
        {
            HawaiianPizza item = new();
            Assert.IsAssignableFrom<INotifyPropertyChanged>(item);
        }
    }
}
