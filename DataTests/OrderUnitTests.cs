using System;
using System.Collections.Generic;
using Xunit;
using PizzaParlor.Data;
using System.Collections.Specialized;
using System.ComponentModel;

namespace PizzaParlor.DataTests
{
    /// <summary>
    /// Order tests
    /// </summary>
    public class OrderUnitTests
    {
        /// <summary>
        /// Mock menu item for tests
        /// </summary>
        internal class MockMenuItem : IMenuItem
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public uint CaloriesPerEach { get; set; }
            public uint CaloriesTotal { get; set; }
            public IEnumerable<string> SpecialInstructions { get; set; }
        }

        /// <summary>
        /// When a second order was added, the second one's order number is one greater
        /// </summary>
        [Fact]
        public void OrderNumberShouldIncrement()
        {
            Order order1 = new Order();
            Order order2 = new Order();

            Assert.Equal(order2.Number-1, order1.Number);
        }

        /// <summary>
        /// Tests if subtotal calculates subtotal
        /// </summary>
        [Fact]
        public void SubtotalShouldReflectItemPrices()
        {
            Order order = new Order();
            order.Add(new MockMenuItem() { Price = 1.00m });
            order.Add(new MockMenuItem() { Price = 2.50m });
            order.Add(new MockMenuItem() { Price = 3.00m });

            Assert.Equal(6.50m, order.Subtotal);
        }

        /// <summary>
        /// Tests if taxes are done correctly
        /// </summary>
        [Fact]
        public void TaxShouldBeCalculatedCorrectly()
        {
            Order order = new Order();
            order.Add(new MockMenuItem() { Price = 10.00m });
            order.Add(new MockMenuItem() { Price = 20.00m });

            decimal expectedTax = 2.745m;
            Assert.Equal(expectedTax, order.Tax);
        }

        /// <summary>
        /// Does total include tax
        /// </summary>
        [Fact]
        public void TotalShouldIncludeTax()
        {
            Order order = new Order();
            order.Add(new MockMenuItem() { Price = 15.00m });
            order.Add(new MockMenuItem() { Price = 25.00m });

            decimal expectedTotal = 43.66m;
            Assert.Equal(expectedTotal, order.Total);
        }

        /// <summary>
        /// Does adding an item increase the count
        /// </summary>
        [Fact]
        public void AddMenuItemIncreasesCount()
        {
            Order order = new Order();
            MockMenuItem menuItem = new MockMenuItem();
            order.Add(menuItem);

            Assert.Equal(1, order.OrderItems.Count);
        }

        /// <summary>
        /// Does removing an item decrease the count
        /// </summary>
        [Fact]
        public void RemoveMenuItemDecreasesCount()
        {
            Order order = new Order();
            MockMenuItem menuItem = new MockMenuItem();
            order.Add(menuItem);
            order.Clear();

            Assert.Equal(0, order.OrderItems.Count);
        }

        /// <summary>
        /// Changing tax notifies property change
        /// </summary>
        [Fact]
        public void ChangingTaxRateShouldNotifyOfPropertyChange()
        {
            Order order = new Order();
            Assert.PropertyChanged(order, "TaxRate", () =>
            {
                order.TaxRate = 0.15m;
            });
        }

        /// <summary>
        /// Changing tax notifies tax property change
        /// </summary>
        [Fact]
        public void ChangingTaxRateShouldNotifyOfTaxPropertyChange()
        {
            Order order = new Order();
            Assert.PropertyChanged(order, "Tax", () =>
            {
                order.TaxRate = 0.15m;
            });
        }

        /// <summary>
        /// Changing tax notifies total property change
        /// </summary>
        [Fact]
        public void ChangingTaxRateShouldNotifyOfTotalPropertyChange()
        {
            Order order = new Order();
            Assert.PropertyChanged(order, "Total", () =>
            {
                order.TaxRate = 0.15m;
            });
        }
         
        /// <summary>
        /// INotify test
        /// </summary>
        [Fact]
        public void ShouldImplementINotifyPropertyChanged()
        {
            Order order = new Order();
            Assert.IsAssignableFrom<INotifyPropertyChanged>(order);
        }

        /// <summary>
        /// Implements INotify
        /// </summary>
        [Fact]
        public void ShouldImplementINotifyCollectionChanged()
        {
            Order order = new Order();
            Assert.IsAssignableFrom<INotifyCollectionChanged>(order);
        }

        /// <summary>
        /// PlacedAt reflects the time that the order was created
        /// </summary>
        [Fact]
        public void PlacedAtReflectsOrderCreationTime()
        {
            DateTime currentTime = DateTime.Now;
            Order order = new Order();

            // Allow a small time difference due to execution time
            Assert.InRange(order.PlacedAt, currentTime.AddSeconds(-1), currentTime.AddSeconds(1));
        }

        /// <summary>
        /// PlacedAt does not change for an order
        /// </summary>
        [Fact]
        public void PlacedAtDoesNotChangeOnRepeatedAccess()
        {
            Order order = new Order();
            DateTime creationTime = order.PlacedAt;

            // Repeated access should not change the value
            for (int i = 0; i < 10; i++)
            {
                Assert.Equal(creationTime, order.PlacedAt);
            }
        }
    }
}
