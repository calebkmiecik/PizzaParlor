using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xunit;

namespace PizzaParlor.Tests
{
    /// <summary>
    /// Tests the IcedTea class
    /// </summary>
    public class IcedTeaTests
    {
        /// <summary>
        /// Tests all the default values for IcedTea
        /// </summary>
        [Fact]
        public void DefaultPropertyValuesTest()
        {
            IcedTea icedTea = new IcedTea();

            Assert.Equal("Iced Tea", icedTea.Name);
            Assert.Equal("Freshly brewed sweet tea", icedTea.Description);
            Assert.True(icedTea.Ice);
            Assert.Equal(Size.Medium, icedTea.DrinkSize);
        }

        /// <summary>
        /// Tests the price calculation for IcedTea
        /// </summary>
        /// <param name="drinkSize">Size of the IcedTea</param>
        /// <param name="expectedPrice">The expected price of the IcedTea</param>
        [Theory]
        [InlineData(Size.Small, 2.0)]
        [InlineData(Size.Medium, 2.5)]
        [InlineData(Size.Large, 3.0)]
        public void PriceCalculationTest(Size drinkSize, decimal expectedPrice)
        {
            IcedTea icedTea = new IcedTea();
            icedTea.DrinkSize = drinkSize;

            decimal actualPrice = icedTea.Price;

            Assert.Equal(expectedPrice, actualPrice);
        }

        /// <summary>
        /// Tests the calorie calculation for IcedTea
        /// </summary>
        /// <param name="drinkSize">Size of the IcedTea</param>
        /// <param name="expectedCalories">Expected calorie count</param>
        [Theory]
        [InlineData(Size.Small, 175u)]
        [InlineData(Size.Medium, 220u)]
        [InlineData(Size.Large, 275u)]
        public void CaloriesTest(Size drinkSize, uint expectedCalories)
        {
            IcedTea icedTea = new IcedTea();
            icedTea.DrinkSize = drinkSize;

            uint actualCalories = icedTea.CaloriesTotal;

            Assert.Equal(expectedCalories, actualCalories);
        }

        /// <summary>
        /// Tests the Special Instructions for IcedTea
        /// </summary>
        /// <param name="ice">Does the IcedTea have ice</param>
        /// <param name="drinkSize">Size of the IcedTea</param>
        /// <param name="expectedInstructions">Expected special instructions</param>
        [Theory]
        [InlineData(true, Size.Small, new string[] { "Small" })]
        [InlineData(false, Size.Medium, new string[] { "Medium", "Hold ice" })]
        [InlineData(true, Size.Large, new string[] { "Large" })]
        [InlineData(false, Size.Small, new string[] { "Small", "Hold ice" })]
        [InlineData(false, Size.Large, new string[] { "Large", "Hold ice" })]
        [InlineData(true, Size.Medium, new string[] { "Medium" })]
        public void SpecialInstructionsTest(bool ice, Size drinkSize, string[] expectedInstructions)
        {
            IcedTea icedTea = new IcedTea();
            icedTea.Ice = ice;
            icedTea.DrinkSize = drinkSize;

            foreach (string instruction in expectedInstructions)
            {
                Assert.Contains(instruction, icedTea.SpecialInstructions);
            }
        }
        /// <summary>
        /// This item can be cast to it's base class type(s)
        /// </summary>
        [Fact]
        public void CastToIMenuItem()
        {
            IcedTea item = new();
            Assert.IsAssignableFrom<IMenuItem>(item);
            Assert.IsAssignableFrom<Drink>(item);
        }

        /// <summary>
        /// Does changing boolean properties notify changes
        /// </summary>
        /// <param name="_bool">Boolean variable</param>
        /// <param name="propertyName">Name of property to be tested</param>
        [Theory]

        [InlineData(true, "SpecialInstructions")]
        [InlineData(false, "SpecialInstructions")]
        public void ChangingBoolShouldNotifyOfPropertyChanges(bool _bool, string propertyName)
        {
            IcedTea item = new();
            Assert.PropertyChanged(item, propertyName, () => {
                item.Ice = _bool;
            });
        }
        [Theory]
        [InlineData(Size.Small, "DrinkSize")]
        [InlineData(Size.Medium, "DrinkSize")]
        [InlineData(Size.Large, "DrinkSize")]
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
        public void ChangingSizeShouldNotifyOfPropertyChanges(Size size, string propertyName)
        {
            Soda soda = new();
            Assert.PropertyChanged(soda, propertyName, () => {
                soda.DrinkSize = size;
            });
        }
        /// <summary>
        /// Does this implement INotifyChanged
        /// </summary>
        [Fact]
        public void ShouldImplementINotifyChanged()
        {
            Breadsticks item = new();
            Assert.IsAssignableFrom<INotifyPropertyChanged>(item);
        }
    }
}
