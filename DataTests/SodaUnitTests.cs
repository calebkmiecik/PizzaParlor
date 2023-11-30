using System;
using System.Collections.Generic;
using Xunit;
using PizzaParlor.Data;
using System.Collections.Specialized;
using System.ComponentModel;
namespace PizzaParlor.Tests
{
    /// <summary>
    /// Tests the Soda class
    /// </summary>
    public class SodaTests
    {
        /// <summary>
        /// Tests all the default values for Soda
        /// </summary>
        [Fact]
        public void DefaultPropertyValuesTest()
        {
            Soda soda = new Soda();

            Assert.Equal("Soda", soda.Name);
            Assert.Equal("Standard fountain drink", soda.Description);
            Assert.True(soda.Ice);
            Assert.Equal(Size.Medium, soda.DrinkSize);
            Assert.Equal(Soda.SodaFlavor.Coke, soda.DrinkType);
        }

        /// <summary>
        /// Tests the price calculation for Soda
        /// </summary>
        /// <param name="drinkSize">Size of the Soda</param>
        /// <param name="expectedPrice">The expected price of the Soda</param>
        [Theory]
        [InlineData(Size.Small, 1.5)]
        [InlineData(Size.Medium, 2.0)]
        [InlineData(Size.Large, 2.5)]
        public void PriceCalculationTest(Size drinkSize, decimal expectedPrice)
        {
            Soda soda = new Soda();
            soda.DrinkSize = drinkSize;

            decimal actualPrice = soda.Price;

            Assert.Equal(expectedPrice, actualPrice);
        }

        /// <summary>
        /// Tests the calorie calculation for Soda
        /// </summary>
        /// <param name="drinkSize">Size of the Soda</param>
        /// <param name="drinkType">Flavor of the Soda</param>
        /// <param name="expectedCalories">Expected calorie count</param>
        [Theory]
        [InlineData(Size.Small, Soda.SodaFlavor.DietCoke, 0u)]
        [InlineData(Size.Medium, Soda.SodaFlavor.DietCoke, 0u)]
        [InlineData(Size.Large, Soda.SodaFlavor.DietCoke, 0u)]
        [InlineData(Size.Small, Soda.SodaFlavor.Coke, 150u)]
        [InlineData(Size.Large, Soda.SodaFlavor.Sprite, 250u)]
        [InlineData(Size.Medium, Soda.SodaFlavor.Sprite, 200u)]
        public void CaloriesTest(Size drinkSize, Soda.SodaFlavor drinkType, uint expectedCalories)
        {
            Soda soda = new Soda();
            soda.DrinkSize = drinkSize;
            soda.DrinkType = drinkType;

            uint actualCalories = soda.CaloriesTotal;

            Assert.Equal(expectedCalories, actualCalories);
        }

        /// <summary>
        /// Tests the Special Instructions for Soda
        /// </summary>
        /// <param name="ice">Does the Soda have ice</param>
        /// <param name="drinkSize">Size of the Soda</param>
        /// <param name="drinkType">Flavor of the Soda</param>
        /// <param name="expectedInstructions">Expected special instructions</param>
        [Theory]
        [InlineData(true, Size.Small, Soda.SodaFlavor.Coke, new string[] { "Small", "Coke" })]
        [InlineData(false, Size.Medium, Soda.SodaFlavor.DietCoke, new string[] { "Medium", "DietCoke", "Hold ice" })]
        [InlineData(true, Size.Large, Soda.SodaFlavor.Sprite, new string[] { "Large", "Sprite" })]
        [InlineData(true, Size.Small, Soda.SodaFlavor.Sprite, new string[] { "Small", "Sprite" })]
        [InlineData(false, Size.Large, Soda.SodaFlavor.DietCoke, new string[] { "Large", "DietCoke", "Hold ice" })]
        [InlineData(true, Size.Medium, Soda.SodaFlavor.Sprite, new string[] { "Medium", "Sprite" })]
        [InlineData(true, Size.Large, Soda.SodaFlavor.Coke, new string[] { "Large", "Coke" })]
        [InlineData(false, Size.Medium, Soda.SodaFlavor.Sprite, new string[] { "Medium", "Sprite", "Hold ice" })]
        public void SpecialInstructionsTest(bool ice, Size drinkSize, Soda.SodaFlavor drinkType, string[] expectedInstructions)
        {
            Soda soda = new Soda();
            soda.Ice = ice;
            soda.DrinkSize = drinkSize;
            soda.DrinkType = drinkType;

            foreach (string instruction in expectedInstructions)
            {
                Assert.Contains(instruction, soda.SpecialInstructions);
            }
        }
        /// <summary>
        /// This item can be cast to it's base class type(s)
        /// </summary>
        [Fact]
        public void CastToIMenuItem()
        {
            Soda item = new();
            Assert.IsAssignableFrom<IMenuItem>(item);
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

        [Theory]
        [InlineData(Soda.SodaFlavor.DietCoke, "CaloriesTotal")]
        [InlineData(Soda.SodaFlavor.Coke, "CaloriesTotal")]
        [InlineData(Soda.SodaFlavor.DietCoke, "CaloriesPerEach")]
        [InlineData(Soda.SodaFlavor.Coke, "CaloriesPerEach")]
        [InlineData(Soda.SodaFlavor.DietCoke, "SpecialInstructions")]
        [InlineData(Soda.SodaFlavor.Coke, "SpecialInstructions")]
        public void ChangingTypeShouldNotifyOfPropertyChanges(Soda.SodaFlavor flavor, string propertyName)
        {
            Soda soda = new();
            Assert.PropertyChanged(soda, propertyName, () => {
                soda.DrinkType = flavor;
            });
        }
        [Fact]
        public void ShouldImplementINotifyChanged()
        {
            Soda soda = new();
            Assert.IsAssignableFrom<INotifyPropertyChanged>(soda);
        }
    }
}
