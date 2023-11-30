using System;
using System.ComponentModel;
using System.Drawing;
using Xunit;
using Size = PizzaParlor.Data.Size;

namespace PizzaParlor.Tests
{
    /// <summary>
    /// Tests the Meats Pizza class
    /// </summary>
    public class MeatsPizzaTests
    {
        /// <summary>
        /// Tests all the default values for a Meats Pizza
        /// </summary>
        [Fact]
        public void DefaultPropertyValuesTest()
        {
            MeatsPizza pizza = new MeatsPizza();

            Assert.Equal(Crust.Original, pizza.PizzaCrust);
            Assert.Equal(Size.Medium, pizza.PizzaSize);
            Assert.Equal("Meats Pizza", pizza.Name);
            Assert.Equal("All the meats", pizza.Description);
            Assert.True(pizza.ContainsTopping(Topping.Sausage));
            Assert.True(pizza.ContainsTopping(Topping.Pepperoni));
            Assert.True(pizza.ContainsTopping(Topping.Ham));
            Assert.True(pizza.ContainsTopping(Topping.Bacon));
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
            MeatsPizza pizza = new MeatsPizza();
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
        /// <param name="ham">Does this pizza contain ham</param>
        /// <param name="bacon">Does this pizza contain bacon</param>
        /// <param name="size">Size of the pizza</param>
        /// <param name="c">Type of crust</param>
        /// <param name="cals">Expected calorie count</param>
        [Theory]
        [InlineData(true, true, true, true, Size.Medium, Crust.Original,
            (uint)(1.0 * (250 + 30 + 20 + 20 + 20)))]
        [InlineData(true, true, true, true, Size.Small, Crust.Thin,
            (uint)(0.75 * (150 + 30 + 20 + 20 + 20)))]
        [InlineData(false, true, false, false, Size.Large, Crust.DeepDish,
            (uint)(1.3 * (300 + 20)))]

        [InlineData(true, false, true, false, Size.Medium, Crust.Original,
            (uint)(1.0 * (250 + 30 + 20)))]
        [InlineData(false, true, false, true, Size.Small, Crust.Thin,
            (uint)(0.75 * (150 + 20 + 20)))]
        [InlineData(true, false, true, false, Size.Large, Crust.DeepDish,
            (uint)(1.3 * (300 + 30 + 20)))]

        [InlineData(true, true, false, true, Size.Medium, Crust.DeepDish,
            (uint)(1.0 * (300 + 30 + 20 + 20)))]
        [InlineData(false, false, false, false, Size.Large, Crust.Original,
            (uint)(1.3 * 250))]
        public void CaloriesPerEachTest(bool sausage, bool pepperoni, bool ham, bool bacon, Size size, Crust c, uint cals)
        {
            MeatsPizza pizza = new MeatsPizza();
            pizza.EditToppings(Topping.Sausage, sausage);
            pizza.EditToppings(Topping.Pepperoni, pepperoni);
            pizza.EditToppings(Topping.Ham, ham);
            pizza.EditToppings(Topping.Bacon, bacon);
            pizza.PizzaSize = size;
            pizza.PizzaCrust = c;

            uint actualCalories = pizza.CaloriesPerEach;

            Assert.Equal(cals, actualCalories);
        }

        /// <summary>
        /// Tests the Special Instructions
        /// </summary>
        /// <param name="sausage">Does this pizza contain sausage</param>
        /// <param name="pepperoni">Does this pizza contain pepperoni</param>
        /// <param name="ham">Does this pizza contain ham</param>
        /// <param name="bacon">Does this pizza contain bacon</param>
        /// <param name="size">Size of the pizza</param>
        /// <param name="c">Type of crust</param>
        /// <param name="expectedInstructions">Expected instructions</param>
        [Theory]
        [InlineData(true, true, true, true, Size.Medium, Crust.Original,
            new string[] { "Medium", "Original" })]
        [InlineData(false, false, false, false, Size.Large, Crust.DeepDish,
            new string[] { "Large", "DeepDish", "Hold Sausage", "Hold Pepperoni", "Hold Ham", "Hold Bacon" })]
        [InlineData(true, false, false, false, Size.Small, Crust.Thin,
            new string[] { "Small", "Thin", "Hold Pepperoni", "Hold Ham", "Hold Bacon" })]
        [InlineData(true, true, true, false, Size.Small, Crust.Original,
            new string[] { "Small", "Original", "Hold Bacon" })]
        [InlineData(false, true, true, true, Size.Medium, Crust.Original,
            new string[] { "Medium", "Original", "Hold Sausage" })]
        [InlineData(false, false, false, false, Size.Large, Crust.Thin,
            new string[] { "Large", "Thin", "Hold Sausage", "Hold Pepperoni", "Hold Ham", "Hold Bacon" })]
        [InlineData(true, false, true, false, Size.Small, Crust.Thin,
            new string[] { "Small", "Thin", "Hold Pepperoni", "Hold Bacon" })]
        [InlineData(true, false, true, false, Size.Large, Crust.Original,
            new string[] { "Large", "Original", "Hold Pepperoni", "Hold Bacon" })]
        public void SpecialInstructionsTest(bool sausage, bool pepperoni, bool ham, bool bacon, Size size, Crust c, string[] expectedInstructions)
        {
            MeatsPizza pizza = new MeatsPizza();
            pizza.EditToppings(Topping.Sausage, sausage);
            pizza.EditToppings(Topping.Pepperoni, pepperoni);
            pizza.EditToppings(Topping.Ham, ham);
            pizza.EditToppings(Topping.Bacon, bacon);
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
            MeatsPizza item = new();
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
            MeatsPizza item = new();
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
            MeatsPizza item = new();
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