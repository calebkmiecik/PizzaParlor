using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xunit;

namespace PizzaParlor.Tests
{
    /// <summary>
    /// Tests the CinnamonSticks class
    /// </summary>
    public class CinnamonSticksTests
    {
        /// <summary>
        /// Tests all the default values for CinnamonSticks
        /// </summary>
        [Fact]
        public void DefaultPropertyValuesTest()
        {
            CinnamonSticks cinnamonSticks = new CinnamonSticks();

            Assert.Equal("Cinnamon Sticks", cinnamonSticks.Name);
            Assert.Equal("Like breadsticks but for dessert", cinnamonSticks.Description);
            Assert.True(cinnamonSticks.Frosting);
            Assert.Equal(8u, cinnamonSticks.SideCount);
        }

        /// <summary>
        /// Tests the number of Cinnamon Sticks
        /// </summary>
        /// <param name="givenCount">Number of Cinnamon Sticks entered</param>
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
            CinnamonSticks cinnamonSticks = new CinnamonSticks();
            cinnamonSticks.SideCount = givenCount;

            Assert.Equal(expectedCount, cinnamonSticks.SideCount);
        }

        /// <summary>
        /// Tests the price calculation for Cinnamon Sticks
        /// </summary>
        /// <param name="frosting">Does this Cinnamon Stick contain frosting</param>
        /// <param name="count">Number of Cinnamon Sticks</param>
        /// <param name="expectedPrice">The expected price of the Cinnamon Sticks</param>
        [Theory]
        [InlineData(true, 8u, 7.20)]
        [InlineData(false, 8u, 6.00)]
        [InlineData(true, 4u, 3.60)]
        [InlineData(false, 4u, 3.00)]
        [InlineData(true, 13u, 11.70)]
        [InlineData(false, 13u, 9.75)]
        public void PriceCalculationTest(bool frosting, uint count, decimal expectedPrice)
        {
            CinnamonSticks cinnamonSticks = new CinnamonSticks();
            cinnamonSticks.Frosting = frosting;
            cinnamonSticks.SideCount = count;

            decimal actualPrice = cinnamonSticks.Price;

            Assert.Equal(expectedPrice, actualPrice);
        }

        /// <summary>
        /// Tests the calorie calculation for Cinnamon Sticks
        /// </summary>
        /// <param name="frosting">Does this Cinnamon Stick contain frosting</param>
        /// <param name="expectedCalories">Expected calorie count</param>
        [Theory]
        [InlineData(true, 190u)]
        [InlineData(false, 160u)]
        public void CaloriesPerEachTest(bool frosting, uint expectedCalories)
        {
            CinnamonSticks cinnamonSticks = new CinnamonSticks();
            cinnamonSticks.Frosting = frosting;

            uint actualCalories = cinnamonSticks.CaloriesPerEach;

            Assert.Equal(expectedCalories, actualCalories);
        }

        /// <summary>
        /// Tests the total calorie calculation for Cinnamon Sticks
        /// </summary>
        /// <param name="frosting">Does this Cinnamon Stick contain frosting</param>
        /// <param name="count">Number of Cinnamon Sticks</param>
        /// <param name="expectedCalories">Expected total calorie count</param>
        [Theory]
        [InlineData(true, 8u, 1520u)]
        [InlineData(false, 8u, 1280u)]
        [InlineData(true, 4u, 760u)]
        [InlineData(false, 4u, 640u)]
        [InlineData(true, 13u, 2470u)]
        [InlineData(false, 13u, 2080u)]
        public void CaloriesTotalTest(bool frosting, uint count, uint expectedCalories)
        {
            CinnamonSticks cinnamonSticks = new CinnamonSticks();
            cinnamonSticks.Frosting = frosting;
            cinnamonSticks.SideCount = count;

            uint actualCalories = cinnamonSticks.CaloriesTotal;

            Assert.Equal(expectedCalories, actualCalories);
        }

        /// <summary>
        /// Tests the Special Instructions for Cinnamon Sticks
        /// </summary>
        /// <param name="frosting">Does this Cinnamon Stick contain frosting</param>
        /// <param name="count">Number of Cinnamon Sticks</param>
        /// <param name="expectedInstructions">Expected special instructions</param>
        [Theory]
        [InlineData(true, 8u, new string[] { "8 Cinnamon Sticks" })]
        [InlineData(false, 8u, new string[] { "8 Cinnamon Sticks", "Hold Frosting" })]
        [InlineData(true, 4u, new string[] { "4 Cinnamon Sticks" })]
        [InlineData(false, 4u, new string[] { "4 Cinnamon Sticks", "Hold Frosting" })]
        [InlineData(true, 13u, new string[] { "13 Cinnamon Sticks" })]
        [InlineData(false, 13u, new string[] { "13 Cinnamon Sticks", "Hold Frosting" })]
        public void SpecialInstructionsTest(bool frosting, uint count, string[] expectedInstructions)
        {
            CinnamonSticks cinnamonSticks = new CinnamonSticks();
            cinnamonSticks.Frosting = frosting;
            cinnamonSticks.SideCount = count;

            foreach (string instruction in expectedInstructions)
            {
                Assert.Contains(instruction, cinnamonSticks.SpecialInstructions);
            }
        }
        /// <summary>
        /// This item can be cast to it's base class type(s)
        /// </summary>
        [Fact]
        public void CastToIMenuItem()
        {
            CinnamonSticks item = new();
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
            CinnamonSticks item = new();
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
            CinnamonSticks item = new();
            Assert.PropertyChanged(item, propertyName, () => {
                item.Frosting = _bool;
            });
        }
        [Fact]
        public void ShouldImplementINotifyChanged()
        {
            CinnamonSticks item = new();
            Assert.IsAssignableFrom<INotifyPropertyChanged>(item);
        }
    }
}
