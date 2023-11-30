using System;
using System.ComponentModel;
using System.Drawing;
using Xunit;
using Size = PizzaParlor.Data.Size;

namespace PizzaParlor.Tests
{
    /// <summary>
    /// Tests the Hawaiian Pizza class
    /// </summary>
    public class HawaiianPizzaTests
    {
        /// <summary>
        /// Tests all the default values for a Hawaiian Pizza
        /// </summary>
        [Fact]
        public void DefaultPropertyValuesTest()
        {
            HawaiianPizza pizza = new HawaiianPizza();

            Assert.Equal(Crust.Original, pizza.PizzaCrust);
            Assert.Equal(Size.Medium, pizza.PizzaSize);
            Assert.Equal("Hawaiian Pizza", pizza.Name);
            Assert.Equal("The pizza with pineapple", pizza.Description);
            Assert.Equal(8u, pizza.Slices);
            Assert.True(pizza.ContainsTopping(Topping.Pineapple));
            Assert.True(pizza.ContainsTopping(Topping.Ham));
            Assert.True(pizza.ContainsTopping(Topping.Onions));
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
            HawaiianPizza pizza = new HawaiianPizza();
            pizza.PizzaSize = size;
            pizza.PizzaCrust = Crust.Original;
            if (isDeepDish) pizza.PizzaCrust = Crust.DeepDish;

            decimal actualPrice = pizza.Price;

            Assert.Equal(expectedPrice, actualPrice);
        }

        /// <summary>
        /// Tests the calorie calculation
        /// </summary>
        /// <param name="ham">Does this pizza contain ham</param>
        /// <param name="pineapple">Does this pizza contain pineapple</param>
        /// <param name="onions">Does this pizza contain onions</param>
        /// <param name="size">Size of the pizza</param>
        /// <param name="c">Type of crust</param>
        /// <param name="cals">Expected calorie count</param>
        [Theory]
        [InlineData(true, true, true, Size.Medium, Crust.Original,
            (uint)(1.0 * (250 + 10 + 20 + 5)))]
        [InlineData(true, true, true, Size.Small, Crust.Thin,
            (uint)(0.75 * (150 + 10 + 20 + 5)))]
        [InlineData(false, true, false, Size.Large, Crust.DeepDish,
            (uint)(1.3 * (300 + 10)))]

        [InlineData(true, false, true, Size.Medium, Crust.Original,
            (uint)(1.0 * (250 + 20 + 5)))]
        [InlineData(false, true, false, Size.Small, Crust.Thin,
            (uint)(0.75 * (150 + 10)))]
        [InlineData(true, false, true, Size.Large, Crust.DeepDish,
            (uint)(1.3 * (300 + 20 + 5)))]

        [InlineData(true, true, false, Size.Medium, Crust.DeepDish,
            (uint)(1.0 * (300 + 10 + 20)))]
        [InlineData(false, false, false, Size.Large, Crust.Original,
            (uint)(1.3 * 250))]
        public void CaloriesPerEachTest(bool ham, bool pineapple, bool onions, Size size, Crust c, uint cals)
        {
            HawaiianPizza pizza = new HawaiianPizza();
            pizza.EditToppings(Topping.Ham, ham);
            pizza.EditToppings(Topping.Pineapple, pineapple);
            pizza.EditToppings(Topping.Onions, onions);
            pizza.PizzaSize = size;
            pizza.PizzaCrust = c;

            uint actualCalories = pizza.CaloriesPerEach;

            Assert.Equal(cals, actualCalories);
        }

        /// <summary>
        /// Tests the Special Instructions
        /// </summary>
        /// <param name="ham">Does this pizza contain ham</param>
        /// <param name="pineapple">Does this pizza contain pineapple</param>
        /// <param name="onions">Does this pizza contain onions</param>
        /// <param name="size">Size of the pizza</param>
        /// <param name="c">Type of crust</param>
        /// <param name="expectedInstructions">Expected instructions</param>
        [Theory]
        [InlineData(true, true, true, Size.Medium, Crust.Original,
            new string[] { "Medium", "Original" })]
        [InlineData(false, false, false, Size.Large, Crust.DeepDish,
            new string[] { "Large", "DeepDish", "Hold Ham", "Hold Pineapple", "Hold Onions" })]
        [InlineData(true, false, false, Size.Small, Crust.Thin,
            new string[] { "Small", "Thin", "Hold Pineapple", "Hold Onions" })]
        [InlineData(true, true, true, Size.Large, Crust.Original,
            new string[] { "Large", "Original" })]
        [InlineData(false, true, true, Size.Medium, Crust.Original,
            new string[] { "Medium", "Original", "Hold Ham" })]
        [InlineData(false, false, false, Size.Large, Crust.Thin,
            new string[] { "Large", "Thin", "Hold Ham", "Hold Pineapple", "Hold Onions" })]
        [InlineData(true, false, true, Size.Small, Crust.Thin,
            new string[] { "Small", "Thin", "Hold Pineapple" })]
        [InlineData(true, false, true, Size.Large, Crust.Original,
            new string[] { "Large", "Original", "Hold Pineapple" })]
        public void SpecialInstructionsTest(bool ham, bool pineapple, bool onions, Size size, Crust c, string[] expectedInstructions)
        {
            HawaiianPizza pizza = new HawaiianPizza();
            pizza.EditToppings(Topping.Ham, ham);
            pizza.EditToppings(Topping.Pineapple, pineapple);
            pizza.EditToppings(Topping.Onions, onions);
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
            HawaiianPizza item = new();
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
            HawaiianPizza item = new();
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
            HawaiianPizza item = new();
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