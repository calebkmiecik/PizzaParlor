using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xunit;

namespace PizzaParlor.Tests
{
    /// <summary>
    /// Tests the Breadsticks class
    /// </summary>
    public class BreadsticksTests
    {
        /// <summary>
        /// Tests all the default values for Breadsticks
        /// </summary>
        [Fact]
        public void DefaultPropertyValuesTest()
        {
            Breadsticks breadsticks = new Breadsticks();

            Assert.Equal("Breadsticks", breadsticks.Name);
            Assert.Equal("Soft, buttery breadsticks", breadsticks.Description);
            Assert.False(breadsticks.Cheese);
            Assert.Equal(8u, breadsticks.SideCount);
        }
        /// <summary>
        /// The number of breadsticks
        /// </summary>
        /// <param name="givenCount">Number of breadsticks entered</param>
        /// <param name="expectedCount">Expected count</param>
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
            Breadsticks breadstick = new Breadsticks();
            breadstick.SideCount = givenCount;

            Assert.Equal(expectedCount, breadstick.SideCount);
        }

        /// <summary>
        /// Tests the price calculation for Breadsticks
        /// </summary>
        /// <param name="cheese">Does this breadstick contain cheese</param>
        /// <param name="count">Number of breadsticks</param>
        /// <param name="expectedPrice">The expected price of the breadsticks</param>
        [Theory]
        [InlineData(false, 8u, 6.00)]
        [InlineData(true, 8u, 8.00)]
        [InlineData(false, 4u, 3.00)]
        [InlineData(true, 4u, 4.00)]
        [InlineData(false, 13u, 9.75)]
        [InlineData(true, 13u, 13.00)]
        public void PriceCalculationTest(bool cheese, uint count, decimal expectedPrice)
        {
            Breadsticks breadsticks = new Breadsticks();
            breadsticks.Cheese = cheese;
            breadsticks.SideCount = count;

            decimal actualPrice = breadsticks.Price;

            Assert.Equal(expectedPrice, actualPrice);
        }

        /// <summary>
        /// Tests the calorie calculation for Breadsticks
        /// </summary>
        /// <param name="cheese">Does this breadstick contain cheese</param>
        /// <param name="expectedCalories">Expected calorie count</param>
        [Theory]
        [InlineData(false, 150u)]
        [InlineData(true, 200u)]
        public void CaloriesPerEachTest(bool cheese, uint expectedCalories)
        {
            Breadsticks breadsticks = new Breadsticks();
            breadsticks.Cheese = cheese;

            uint actualCalories = breadsticks.CaloriesPerEach;

            Assert.Equal(expectedCalories, actualCalories);
        }

        /// <summary>
        /// Tests the total calorie calculation for Breadsticks
        /// </summary>
        /// <param name="cheese">Does this breadstick contain cheese</param>
        /// <param name="count">Number of breadsticks</param>
        /// <param name="expectedCalories">Expected total calorie count</param>
        [Theory]
        [InlineData(false, 8u, 1200u)]
        [InlineData(true, 8u, 1600u)]
        [InlineData(false, 4u, 600u)]
        [InlineData(true, 4u, 800u)]
        [InlineData(false, 13u, 1950u)]
        [InlineData(true, 13u, 2600u)]
        public void CaloriesTotalTest(bool cheese, uint count, uint expectedCalories)
        {
            Breadsticks breadsticks = new Breadsticks();
            breadsticks.Cheese = cheese;
            breadsticks.SideCount = count;

            uint actualCalories = breadsticks.CaloriesTotal;

            Assert.Equal(expectedCalories, actualCalories);
        }

        /// <summary>
        /// Tests the Special Instructions for Breadsticks
        /// </summary>
        /// <param name="cheese">Does this breadstick contain cheese</param>
        /// <param name="count">Number of breadsticks</param>
        /// <param name="expectedInstructions">Expected special instructions</param>
        [Theory]
        [InlineData(false, 8u, new string[] { "8 Breadsticks" })]
        [InlineData(true, 8u, new string[] { "8 Cheesesticks" })]
        [InlineData(false, 4u, new string[] { "4 Breadsticks" })]
        [InlineData(true, 4u, new string[] { "4 Cheesesticks" })]
        [InlineData(false, 13u, new string[] { "13 Breadsticks" })]
        [InlineData(true, 13u, new string[] { "13 Cheesesticks" })]
        public void SpecialInstructionsTest(bool cheese, uint count, string[] expectedInstructions)
        {
            Breadsticks breadsticks = new Breadsticks();
            breadsticks.Cheese = cheese;
            breadsticks.SideCount = count;

            foreach (string instruction in expectedInstructions)
            {
                Assert.Contains(instruction, breadsticks.SpecialInstructions);
            }
        }
        /// <summary>
        /// This item can be cast to it's base class type(s)
        /// </summary>
        [Fact]
        public void CastToIMenuItem() 
        {
            Breadsticks breadsticks = new();
            Assert.IsAssignableFrom<IMenuItem>(breadsticks);
            Assert.IsAssignableFrom<Side>(breadsticks);
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
            Breadsticks item = new();
            Assert.PropertyChanged(item, propertyName, () => {
                item.SideCount = count;
            });
        }

        [Theory]

        [InlineData(true, "Price")]
        [InlineData(false, "Price")]
        [InlineData(true, "CaloriesTotal")]
        [InlineData(false, "CaloriesTotal")]
        [InlineData(true, "CaloriesPerEach")]
        [InlineData(false, "CaloriesPerEach")]
        [InlineData(true, "SpecialInstructions")]
        [InlineData(false, "SpecialInstructions")]
        public void ChangingBoolShouldNotifyOfPropertyChanges(bool _bool, string propertyName)
        {
            Breadsticks item = new();
            Assert.PropertyChanged(item, propertyName, () => {
                item.Cheese = _bool;
            });
        }
        [Fact]
        public void ShouldImplementINotifyChanged()
        {
            Breadsticks item = new();
            Assert.IsAssignableFrom<INotifyPropertyChanged>(item);
        }
    }
}