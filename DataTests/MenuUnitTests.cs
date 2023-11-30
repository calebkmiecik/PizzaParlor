using System;
using System.Collections.Generic;
using Xunit;
using PizzaParlor.Data;
using System.Collections.Specialized;
using System.ComponentModel;

namespace PizzaParlor.DataTests
{
    /// <summary>
    /// Menu tests
    /// </summary>
    public class MenuUnitTests
    {
        /// <summary>
        /// The number of pizza items is correct
        /// </summary>
        [Fact]
        public void PizzaItemsNumberIsCorrect()
        {
            Menu menu = new Menu();

            int count = 0;
            foreach (IMenuItem item in menu.Pizzas)
            {
                count++;
            }

            Assert.Equal(45, count);
        }

        /// <summary>
        /// The number of drink items is correct
        /// </summary>
        [Fact]
        public void DrinkItemsNumberIsCorrect()
        {
            Menu menu = new Menu();

            int count = 0;
            foreach (IMenuItem item in menu.Drinks)
            {
                count++;
            }

            Assert.Equal(18, count);
        }

        /// <summary>
        /// The number of side items is correct
        /// </summary>
        [Fact]
        public void SideItemsNumberIsCorrect()
        {
            Menu menu = new Menu();

            int count = 0;
            foreach (IMenuItem item in menu.Sides)
            {
                count++;
            }

            Assert.Equal(13, count);
        }

        /// <summary>
        /// Returns correct amount of items
        /// </summary>
        [Fact]
        public void SearchAndFilterPizzaItemsReturnsCorrectResults()
        {
            Menu menu = new Menu();

            var filteredPizzas = menu.GetFilteredPizzas("Supreme", "300", "500", "10", "20");

            Assert.Equal(4, filteredPizzas.Count);
        }

        /// <summary>
        /// Returns correct amount of items
        /// </summary>
        [Fact]
        public void SearchAndFilterSideItemsReturnsCorrectResults()
        {
            Menu menu = new Menu();

            var filteredSides = menu.GetFilteredSides("knots", "0", "300", "5", "10");

            Assert.Single(filteredSides);
        }

        /// <summary>
        /// Returns correct amount of items
        /// </summary>
        [Fact]
        public void SearchAndFilterDrinkItemsReturnsCorrectResults()
        {
            Menu menu = new Menu();

            var filteredDrinks = menu.GetFilteredDrinks("Coke", "100", "300", "1", "5");

            Assert.Equal(3, filteredDrinks.Count);
        }

        /// <summary>
        /// Returns correct amount of items
        /// </summary>
        [Fact]
        public void SearchAndFilterWithNullValuesReturnsAllItems()
        {
            Menu menu = new Menu();

            var allPizzas = menu.GetFilteredPizzas(null, null, null, null, null);
            var allSides = menu.GetFilteredSides(null, null, null, null, null);
            var allDrinks = menu.GetFilteredDrinks(null, null, null, null, null);
            
            Assert.Equal(45, allPizzas.Count);
            Assert.Equal(13, allSides.Count);
            Assert.Equal(18, allDrinks.Count);
        }

        /// <summary>
        /// Returns correct amount of items
        /// </summary>
        [Fact]
        public void SearchAndFilterCaseInsensitiveReturnsCorrectResults()
        {
            Menu menu = new Menu();

            var filteredPizzas = menu.GetFilteredPizzas("supreme", null, null, null, null);
            var filteredSides = menu.GetFilteredSides("garlic knots", null, null, null, null);
            var filteredDrinks = menu.GetFilteredDrinks("sprite", null, null, null, null);

            Assert.Equal(9, filteredPizzas.Count);
            Assert.Single(filteredSides);
            Assert.Equal(3, filteredDrinks.Count);
        }
    }
}
