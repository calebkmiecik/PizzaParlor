using Xunit;
using PizzaParlor.Data;
using System.ComponentModel;

namespace PizzaParlor.Tests
{
    /// <summary>
    /// This tests the Pizza class
    /// </summary>
    public class PizzaTests
    {
        /// <summary>
        /// Tests the default properties for the Pizza Class
        /// </summary>
        [Fact]
        public void DefaultPropertyValuesTest()
        {
            Pizza pizza = new Pizza();

            Assert.Equal(Crust.Original, pizza.PizzaCrust);
            Assert.Equal(Size.Medium, pizza.PizzaSize);
            Assert.Equal("Build-Your-Own Pizza", pizza.Name);
            Assert.Equal("A pizza you get to build", pizza.Description);
            Assert.Equal(8u, pizza.Slices);
        }
        /// <summary>
        /// Tests price calculation
        /// </summary>
        /// <param name="size">Size of the pizza</param>
        /// <param name="isDeepDish">Is it deepdish?</param>
        /// <param name="expectedPrice">Expected price</param>
        [Theory]
        [InlineData(Size.Small, false, 7.99)]
        [InlineData(Size.Small, true, 8.99)]
        [InlineData(Size.Medium, false, 9.99)]
        [InlineData(Size.Medium, true, 10.99)]
        [InlineData(Size.Large, false, 11.99)]
        [InlineData(Size.Large, true, 12.99)]
        public void PriceCalculationTest(Size size, bool isDeepDish, decimal expectedPrice)
        {
            Pizza pizza = new Pizza();
            pizza.PizzaSize = size;
            pizza.PizzaCrust = Crust.Original;
            if (isDeepDish) pizza.PizzaCrust = Crust.DeepDish;

            decimal actualPrice = pizza.Price;

            Assert.Equal(expectedPrice, actualPrice);
        }
        /// <summary>
        /// Calories of each slice
        /// </summary>
        /// <param name="crust">Type of crust</param>
        /// <param name="size">Size of pizza</param>
        /// <param name="expectedCalories">Expected calories</param>
        [Theory]
        [InlineData(Crust.Thin, Size.Medium, 150)]
        [InlineData(Crust.DeepDish, Size.Small, (uint)(300 * .75))]
        [InlineData(Crust.Original, Size.Large, (uint)(250 * 1.3))]
        [InlineData(Crust.DeepDish, Size.Large, (uint)(300 * 1.3))]
        [InlineData(Crust.Original, Size.Small, (uint)(250 * .75))]
        public void CaloriesPerEachTest(Crust crust, Size size, uint expectedCalories)
        {
            Pizza pizza = new Pizza();
            pizza.PizzaCrust = crust;
            pizza.PizzaSize = size;

            uint actualCalories = pizza.CaloriesPerEach;

            Assert.Equal(expectedCalories, actualCalories);
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
            Pizza item = new();
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
            Pizza item = new();
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
