using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xunit;

namespace PizzaParlor.Tests
{
    /// <summary>
    /// Tests the Garlic Knots class
    /// </summary>
    public class GarlicKnotsTests
    {
        /// <summary>
        /// Tests all the default values for Garlic Knots
        /// </summary>
        [Fact]
        public void DefaultPropertyValuesTest()
        {
            GarlicKnots garlicKnots = new GarlicKnots();

            Assert.Equal("Garlic Knots", garlicKnots.Name);
            Assert.Equal("Twisted rolls with garlic and butter", garlicKnots.Description);
            Assert.Equal(8u, garlicKnots.SideCount);
        }

        /// <summary>
        /// Tests the count property for Garlic Knots
        /// </summary>
        [Theory]
        [InlineData(1, 4)]
        [InlineData(15, 13)]
        [InlineData(6, 6)]
        [InlineData(4, 4)]
        [InlineData(13, 13)]
        [InlineData(8, 8)]
        [InlineData(10, 10)]
        [InlineData(5, 5)]
        public void CountTest(uint givenCount, uint expectedCount)
        {
            GarlicKnots garlicKnots = new GarlicKnots();
            garlicKnots.SideCount = givenCount;

            Assert.Equal(expectedCount, garlicKnots.SideCount);
        }

        /// <summary>
        /// Tests the price calculation for Garlic Knots
        /// </summary>
        /// <param name="count">Number of Garlic Knots</param>
        /// <param name="expectedPrice">The expected price of the Garlic Knots</param>
        [Theory]
        [InlineData(8u, 6.00)]
        [InlineData(4u, 3.00)]
        [InlineData(13u, 9.75)]
        [InlineData(10u, 7.50)]
        [InlineData(2u, 3.00)]
        [InlineData(15u, 9.75)]
        public void PriceCalculationTest(uint count, decimal expectedPrice)
        {
            GarlicKnots garlicKnots = new GarlicKnots();
            garlicKnots.SideCount = count;

            decimal actualPrice = garlicKnots.Price;

            Assert.Equal(expectedPrice, actualPrice);
        }

        /// <summary>
        /// Tests the calorie calculation for Garlic Knots
        /// </summary>
        [Fact]
        public void CaloriesPerEachTest()
        {
            GarlicKnots garlicKnots = new GarlicKnots();

            uint actualCalories = garlicKnots.CaloriesPerEach;

            Assert.Equal(175u, actualCalories);
        }

        /// <summary>
        /// Tests the total calorie calculation for Garlic Knots
        /// </summary>
        /// <param name="count">Number of Garlic Knots</param>
        /// <param name="expectedCalories">Expected total calorie count</param>
        [Theory]
        [InlineData(8u, 1400u)]
        [InlineData(4u, 700u)]
        [InlineData(13u, 2275u)]
        [InlineData(10u, 1750u)]
        [InlineData(1u, 700u)]
        [InlineData(15u, 2275u)]
        public void CaloriesTotalTest(uint count, uint expectedCalories)
        {
            GarlicKnots garlicKnots = new GarlicKnots();
            garlicKnots.SideCount = count;

            uint actualCalories = garlicKnots.CaloriesTotal;

            Assert.Equal(expectedCalories, actualCalories);
        }

        /// <summary>
        /// Tests the Special Instructions for Garlic Knots
        /// </summary>
        /// <param name="count">Number of Garlic Knots</param>
        /// <param name="expectedInstructions">Expected special instructions</param>
        [Theory]
        [InlineData(8u, new string[] { "8 Garlic Knots" })]
        [InlineData(4u, new string[] { "4 Garlic Knots" })]
        [InlineData(13u, new string[] { "13 Garlic Knots" })]
        [InlineData(10u, new string[] { "10 Garlic Knots" })]
        public void SpecialInstructionsTest(uint count, string[] expectedInstructions)
        {
            GarlicKnots garlicKnots = new GarlicKnots();
            garlicKnots.SideCount = count;

            foreach (string instruction in expectedInstructions)
            {
                Assert.Contains(instruction, garlicKnots.SpecialInstructions);
            }
        }
        /// <summary>
        /// This item can be cast to it's base class type(s)
        /// </summary>
        [Fact]
        public void CastToIMenuItem()
        {
            GarlicKnots item = new();
            Assert.IsAssignableFrom<IMenuItem>(item);
            Assert.IsAssignableFrom<Side>(item);
        }
        [Theory]

        [InlineData(6, "Price")]
        [InlineData(8, "Price")]
        [InlineData(6, "CaloriesTotal")]
        [InlineData(8, "CaloriesTotal")]
        [InlineData(6, "CaloriesPerEach")]
        [InlineData(8, "CaloriesPerEach")]
        [InlineData(6, "SpecialInstructions")]
        [InlineData(8, "SpecialInstructions")]
        public void ChangingCountShouldNotifyOfPropertyChanges(uint count, string propertyName)
        {
            GarlicKnots item = new();
            Assert.PropertyChanged(item, propertyName, () => {
                item.SideCount = count;
            });
        }

        [Fact]
        public void ShouldImplementINotifyChanged()
        {
            GarlicKnots item = new();
            Assert.IsAssignableFrom<INotifyPropertyChanged>(item);
        }
    }
}
