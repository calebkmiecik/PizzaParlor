using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xunit;
using Size = PizzaParlor.Data.Size;

namespace PizzaParlor.Tests
{
    /// <summary>
    /// Tests the Veggie Pizza class
    /// </summary>
    public class VeggiePizzaTests
    {
        /// <summary>
        /// Tests all the default values for a Veggie Pizza
        /// </summary>
        [Fact]
        public void DefaultPropertyValuesTest()
        {
            VeggiePizza pizza = new VeggiePizza();

            Assert.Equal(Crust.Original, pizza.PizzaCrust);
            Assert.Equal(Size.Medium, pizza.PizzaSize);
            Assert.Equal("Veggie Pizza", pizza.Name);
            Assert.Equal("All the veggies", pizza.Description);
            Assert.True(pizza.ContainsTopping(Topping.Olives));
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
            VeggiePizza pizza = new VeggiePizza();
            pizza.PizzaSize = size;
            pizza.PizzaCrust = Crust.Original;
            if (isDeepDish) pizza.PizzaCrust = Crust.DeepDish;

            decimal actualPrice = pizza.Price;

            Assert.Equal(expectedPrice, actualPrice);
        }

        /// <summary>
        /// Tests the calorie calculation
        /// </summary>
        /// <param name="olives">Does this pizza contain olives</param>
        /// <param name="peppers">Does this pizza contain peppers</param>
        /// <param name="onions">Does this pizza contain onions</param>
        /// <param name="mushrooms">Does this pizza contain mushrooms</param>
        /// <param name="size">Size of the pizza</param>
        /// <param name="c">Type of crust</param>
        /// <param name="cals">Expected calorie count</param>
        [Theory]
        [InlineData(true, true, true, true, Size.Medium, Crust.Original,
            (uint)(1.0 * (250 + 5 + 5 + 5 + 5)))]
        [InlineData(true, true, true, true, Size.Small, Crust.Thin,
            (uint)(0.75 * (150 + 5 + 5 + 5 + 5)))]
        [InlineData(false, true, false, false, Size.Large, Crust.DeepDish,
            (uint)(1.3 * (300 + 5)))]

        [InlineData(true, false, true, false, Size.Medium, Crust.Original,
            (uint)(1.0 * (250 + 5 + 5)))]
        [InlineData(false, true, false, true, Size.Small, Crust.Thin,
            (uint)(0.75 * (150 + 5 + 5)))]
        [InlineData(true, false, true, false, Size.Large, Crust.DeepDish,
            (uint)(1.3 * (300 + 5 + 5)))]

        [InlineData(true, true, false, true, Size.Medium, Crust.DeepDish,
            (uint)(1.0 * (300 + 5 + 5 + 5)))]
        [InlineData(false, false, false, false, Size.Large, Crust.Original,
            (uint)(1.3 * 250))]
        public void CaloriesPerEachTest(bool olives, bool peppers, bool onions, bool mushrooms, Size size, Crust c, uint cals)
        {
            VeggiePizza pizza = new VeggiePizza();
            pizza.EditToppings(Topping.Olives, olives);
            pizza.EditToppings(Topping.Peppers, peppers);
            pizza.EditToppings(Topping.Onions, onions);
            pizza.EditToppings(Topping.Mushrooms, mushrooms);
            pizza.PizzaSize = size;
            pizza.PizzaCrust = c;

            uint actualCalories = pizza.CaloriesPerEach;

            Assert.Equal(cals, actualCalories);
        }

        /// <summary>
        /// Tests the Special Instructions
        /// </summary>
        /// <param name="olives">Does this pizza contain olives</param>
        /// <param name="peppers">Does this pizza contain peppers</param>
        /// <param name="onions">Does this pizza contain onions</param>
        /// <param name="mushrooms">Does this pizza contain mushrooms</param>
        /// <param name="size">Size of the pizza</param>
        /// <param name="c">Type of crust</param>
        /// <param name="expectedInstructions">Expected instructions</param>
        [Theory]
        [InlineData(true, true, true, true, Size.Medium, Crust.Original,
            new string[] { "Medium", "Original" })]
        [InlineData(false, false, false, false, Size.Large, Crust.DeepDish,
            new string[] { "Large", "DeepDish", "Hold Olives", "Hold Onions", "Hold Peppers", "Hold Mushrooms" })]
        [InlineData(true, false, false, false, Size.Small, Crust.Thin,
            new string[] { "Small", "Thin", "Hold Peppers", "Hold Onions", "Hold Mushrooms" })]
        [InlineData(true, true, true, false, Size.Small, Crust.Original,
            new string[] { "Small", "Original", "Hold Mushrooms" })]
        [InlineData(false, true, true, true, Size.Medium, Crust.Original,
            new string[] { "Medium", "Original", "Hold Olives" })]
        [InlineData(false, false, false, false, Size.Large, Crust.Thin,
            new string[] { "Large", "Thin", "Hold Olives", "Hold Peppers", "Hold Onions", "Hold Mushrooms" })]
        [InlineData(true, false, true, false, Size.Small, Crust.Thin,
            new string[] { "Small", "Thin", "Hold Peppers", "Hold Mushrooms" })]
        [InlineData(true, false, true, false, Size.Large, Crust.Original,
            new string[] { "Large", "Original", "Hold Peppers", "Hold Mushrooms" })]
        public void SpecialInstructionsTest(bool olives, bool peppers, bool onions, bool mushrooms, Size size, Crust c, string[] expectedInstructions)
        {
            VeggiePizza pizza = new VeggiePizza();
            pizza.EditToppings(Topping.Olives, olives);
            pizza.EditToppings(Topping.Peppers, peppers);
            pizza.EditToppings(Topping.Onions, onions);
            pizza.EditToppings(Topping.Mushrooms, mushrooms);
            pizza.PizzaSize = size;
            pizza.PizzaCrust = c;

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
            VeggiePizza item = new();
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
            VeggiePizza item = new();
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
            VeggiePizza item = new();
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