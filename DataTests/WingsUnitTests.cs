using System;
using System.Collections.Generic;
using Xunit;

namespace PizzaParlor.Tests
{
    /// <summary>
    /// Tests the Wings class
    /// </summary>
    public class WingsTests
    {
        /// <summary>
        /// Tests all the default values for Wings
        /// </summary>
        [Fact]
        public void DefaultPropertyValuesTest()
        {
            Wings wings = new Wings();

            Assert.Equal("Wings", wings.Name);
            Assert.Equal("Chicken wings tossed in sauce", wings.Description);
            Assert.True(wings.BoneIn);
            Assert.Equal(Wings.WingSauce.Medium, wings.Sauce);
            Assert.Equal(5u, wings.SideCount);
        }

        /// <summary>
        /// Tests the price calculation for Wings
        /// </summary>
        /// <param name="boneIn">Are the wings bone-in</param>
        /// <param name="count">Number of wings</param>
        /// <param name="expectedPrice">The expected price of the wings</param>
        [Theory]
        [InlineData(true, 5u, 7.5)]
        [InlineData(false, 5u, 8.75)]
        [InlineData(true, 12u, 18)]
        [InlineData(false, 12u, 21)]
        [InlineData(true, 7u, 10.5)]
        [InlineData(false, 9u, 15.75)]
        [InlineData(true, 15u, 18)]
        [InlineData(false, 4u, 7)]
        public void PriceCalculationTest(bool boneIn, uint count, decimal expectedPrice)
        {
            Wings wings = new Wings();
            wings.BoneIn = boneIn;
            wings.SideCount = count;

            decimal actualPrice = wings.Price;

            Assert.Equal(expectedPrice, actualPrice);
        }

        /// <summary>
        /// Tests the calorie calculation for Wings
        /// </summary>
        /// <param name="boneIn">Are the wings bone-in</param>
        /// <param name="sauce">Type of sauce</param>
        /// <param name="expectedCalories">Expected calorie count</param>
        [Theory]
        [InlineData(true, Wings.WingSauce.Mild, 125u)]
        [InlineData(false, Wings.WingSauce.HoneyBBQ, 195u)]
        [InlineData(true, Wings.WingSauce.Hot, 125u)]
        [InlineData(false, Wings.WingSauce.Medium, 175u)]
        [InlineData(false, Wings.WingSauce.Mild, 175u)]
        [InlineData(true, Wings.WingSauce.HoneyBBQ, 145u)]
        [InlineData(false, Wings.WingSauce.Hot, 175u)]
        [InlineData(true, Wings.WingSauce.Medium, 125u)]
        public void CaloriesPerEachTest(bool boneIn, Wings.WingSauce sauce, uint expectedCalories)
        {
            Wings wings = new Wings();
            wings.BoneIn = boneIn;
            wings.Sauce = sauce;

            uint actualCalories = wings.CaloriesPerEach;

            Assert.Equal(expectedCalories, actualCalories);
        }

        /// <summary>
        /// Tests the total calorie calculation for Wings
        /// </summary>
        /// <param name="boneIn">Are the wings bone-in</param>
        /// <param name="sauce">Type of sauce</param>
        /// <param name="count">Number of wings</param>
        /// <param name="expectedCalories">Expected total calorie count</param>
        [Theory]
        [InlineData(true, Wings.WingSauce.Mild, 5u, 625u)]
        [InlineData(false, Wings.WingSauce.HoneyBBQ, 12u, 2340u)]
        [InlineData(true, Wings.WingSauce.Hot, 5u, 625u)]
        [InlineData(false, Wings.WingSauce.Medium, 12u, 2100u)]
        [InlineData(false, Wings.WingSauce.Mild, 12u, 2100u)]
        [InlineData(true, Wings.WingSauce.HoneyBBQ, 4u, 580u)]
        [InlineData(false, Wings.WingSauce.Hot, 9u, 1575u)]
        [InlineData(true, Wings.WingSauce.Medium, 6u, 750u)]

        public void CaloriesTotalTest(bool boneIn, Wings.WingSauce sauce, uint count, uint expectedCalories)
        {
            Wings wings = new Wings();
            wings.BoneIn = boneIn;
            wings.Sauce = sauce;
            wings.SideCount = count;

            uint actualCalories = wings.CaloriesTotal;

            Assert.Equal(expectedCalories, actualCalories);
        }

        /// <summary>
        /// Tests the Special Instructions for Wings
        /// </summary>
        /// <param name="boneIn">Are the wings bone-in</param>
        /// <param name="sauce">Type of sauce</param>
        /// <param name="count">Number of wings</param>
        /// <param name="expectedInstructions">Expected special instructions</param>
        [Theory]
        [InlineData(true, Wings.WingSauce.Mild, 5u, new string[] { "5", "Mild", "Bone-In" })]
        [InlineData(false, Wings.WingSauce.HoneyBBQ, 12u, new string[] { "12", "HoneyBBQ", "Boneless" })]
        [InlineData(true, Wings.WingSauce.Hot, 5u, new string[] { "5", "Hot", "Bone-In" })]
        [InlineData(false, Wings.WingSauce.Medium, 12u, new string[] { "12", "Medium", "Boneless" })]
        [InlineData(true, Wings.WingSauce.Mild, 7u, new string[] { "7", "Mild", "Bone-In" })]
        [InlineData(true, Wings.WingSauce.HoneyBBQ, 12u, new string[] { "12", "HoneyBBQ", "Bone-In" })]
        [InlineData(true, Wings.WingSauce.Mild, 9u, new string[] { "9", "Mild", "Bone-In" })]
        [InlineData(false, Wings.WingSauce.Hot, 12u, new string[] { "12", "Hot", "Boneless" })]
        public void SpecialInstructionsTest(bool boneIn, Wings.WingSauce sauce, uint count, string[] expectedInstructions)
        {
            Wings wings = new Wings();
            wings.BoneIn = boneIn;
            wings.Sauce = sauce;
            wings.SideCount = count;

            foreach (string instruction in expectedInstructions)
            {
                Assert.Contains(instruction, wings.SpecialInstructions);
            }
        }
        /// <summary>
        /// This item can be cast to it's base class type(s)
        /// </summary>
        [Fact]
        public void CastToIMenuItem()
        {
            Wings item = new();
            Assert.IsAssignableFrom<IMenuItem>(item);
            Assert.IsAssignableFrom<Side>(item);
        }
    }
}
