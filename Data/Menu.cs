using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaParlor.Data;

namespace PizzaParlor.Data
{
    public class Menu
    {
        /// <summary>
        /// Creates a list of all possible pizza options
        /// </summary>
        public IEnumerable<IMenuItem> Pizzas
        {
            get
            {
                var crusts = Enum.GetValues(typeof(Crust));
                var sizes = Enum.GetValues(typeof(Size));

                List<IMenuItem> items = new List<IMenuItem>();

                foreach (Crust crust in crusts)
                {
                    foreach (Size size in sizes)
                    {
                        HawaiianPizza pizza = new();
                        pizza.PizzaCrust = crust;
                        pizza.PizzaSize = size;

                        items.Add(pizza);
                    }
                }

                foreach (Crust crust in crusts)
                {
                    foreach (Size size in sizes)
                    {
                        SupremePizza pizza = new();
                        pizza.PizzaCrust = crust;
                        pizza.PizzaSize = size;

                        items.Add(pizza);
                    }
                }

                foreach (Crust crust in crusts)
                {
                    foreach (Size size in sizes)
                    {
                        MeatsPizza pizza = new();
                        pizza.PizzaCrust = crust;
                        pizza.PizzaSize = size;

                        items.Add(pizza);
                    }
                }

                foreach (Crust crust in crusts)
                {
                    foreach (Size size in sizes)
                    {
                        VeggiePizza pizza = new();
                        pizza.PizzaCrust = crust;
                        pizza.PizzaSize = size;

                        items.Add(pizza);
                    }
                }

                foreach (Crust crust in crusts)
                {
                    foreach (Size size in sizes)
                    {
                        Pizza pizza = new();
                        pizza.PizzaCrust = crust;
                        pizza.PizzaSize = size;

                        items.Add(pizza);
                    }
                }

                return items;
            }

        }
        /// <summary>
        /// Creates a list of all possible sides options
        /// </summary>
        public IEnumerable<IMenuItem> Sides
        {

            get
            {
                List<IMenuItem> items = new List<IMenuItem>();

                Breadsticks breadSticks = new Breadsticks();

                breadSticks.Cheese = false;
                items.Add(breadSticks);

                breadSticks = new Breadsticks();

                breadSticks.Cheese = true;
                items.Add(breadSticks);

                CinnamonSticks cinnamonSticks = new CinnamonSticks();

                cinnamonSticks.Frosting = false;
                items.Add(cinnamonSticks);

                cinnamonSticks = new CinnamonSticks();

                cinnamonSticks.Frosting = true;
                items.Add(cinnamonSticks);

                GarlicKnots garlicKnots = new GarlicKnots();

                items.Add(garlicKnots);

                var flavors = Enum.GetValues(typeof(Wings.WingSauce));
               
                foreach (Wings.WingSauce flavor in flavors)
                {
                    Wings wings = new();

                    wings.Sauce = flavor;
                    wings.BoneIn = false;

                    items.Add(wings);

                    wings.BoneIn = true;

                    items.Add(wings);
                }

                return items;
            }

        }
        /// <summary>
        /// Creates a list of all possible drinks options
        /// </summary>
        public IEnumerable<IMenuItem> Drinks
        {
            get
            {
                List<IMenuItem> items = new List<IMenuItem>();

                var flavors = Enum.GetValues(typeof(Soda.SodaFlavor));

                var sizes = Enum.GetValues(typeof(Size));

                foreach (Size size in sizes)
                {

                    foreach (Soda.SodaFlavor flavor in flavors)
                    {
                        Soda drink = new Soda();

                        drink.DrinkSize = size;
                        drink.DrinkType = flavor;

                        items.Add(drink);
                    }
                    IcedTea tea = new();
                    tea.DrinkSize = size;

                    items.Add(tea);
                }

                return items;
            }
        }
        /// <summary>
        /// Creates a list of all possible item menu options
        /// </summary>
        public IEnumerable<IMenuItem> FullMenu
        {
            get
            {
                List<IMenuItem> items = new();

                foreach (IMenuItem item in Pizzas)
                {
                    items.Add(item);
                }

                foreach (IMenuItem item in Sides)
                {
                    items.Add(item);
                }

                foreach (IMenuItem item in Drinks)
                {
                    items.Add(item);
                }

                return items;
            }
        }

        /// <summary>
        /// Retrieves a filtered list of pizza menu items based on specified criteria.
        /// </summary>
        /// <param name="searchTerm">The search term for filtering by name or special instructions.</param>
        /// <param name="minCalories">The minimum calories filter.</param>
        /// <param name="maxCalories">The maximum calories filter.</param>
        /// <param name="minPrice">The minimum price filter.</param>
        /// <param name="maxPrice">The maximum price filter.</param>
        /// <returns>A list of filtered pizza menu items.</returns>
        public List<IMenuItem> GetFilteredPizzas(string searchTerm, string minCalories, string maxCalories, string minPrice, string maxPrice)
        {
            IEnumerable<IMenuItem> filteredPizzas = Pizzas;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                filteredPizzas = filteredPizzas.Where(pizza =>
                    searchTerms.All(term =>
                        pizza.Name.ToLower().Contains(term) ||
                        pizza.SpecialInstructions.Any(instruction => instruction.ToLower().Contains(term))
                    )
                );
            }

            filteredPizzas = ApplyFilterRange(filteredPizzas, minCalories, maxCalories, pizza => pizza.CaloriesPerEach);
            filteredPizzas = ApplyFilterRange(filteredPizzas, minPrice, maxPrice, pizza => pizza.Price);

            return filteredPizzas.ToList();
        }

        /// <summary>
        /// Retrieves a filtered list of side menu items based on specified criteria.
        /// </summary>
        /// <param name="searchTerm">The search term for filtering by name or special instructions.</param>
        /// <param name="minCalories">The minimum calories filter.</param>
        /// <param name="maxCalories">The maximum calories filter.</param>
        /// <param name="minPrice">The minimum price filter.</param>
        /// <param name="maxPrice">The maximum price filter.</param>
        /// <returns>A list of filtered side menu items.</returns>
        public List<IMenuItem> GetFilteredSides(string searchTerm, string minCalories, string maxCalories, string minPrice, string maxPrice)
        {
            IEnumerable<IMenuItem> filteredSides = Sides;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                filteredSides = filteredSides.Where(side =>
                    searchTerms.All(term =>
                        side.Name.ToLower().Contains(term) ||
                        side.SpecialInstructions.Any(instruction => instruction.ToLower().Contains(term))
                    )
                );
            }

            filteredSides = ApplyFilterRange(filteredSides, minCalories, maxCalories, side => side.CaloriesPerEach);
            filteredSides = ApplyFilterRange(filteredSides, minPrice, maxPrice, side => side.Price);

            return filteredSides.ToList();
        }

        /// <summary>
        /// Retrieves a filtered list of drink menu items based on specified criteria.
        /// </summary>
        /// <param name="searchTerm">The search term for filtering by name or special instructions.</param>
        /// <param name="minCalories">The minimum calories filter.</param>
        /// <param name="maxCalories">The maximum calories filter.</param>
        /// <param name="minPrice">The minimum price filter.</param>
        /// <param name="maxPrice">The maximum price filter.</param>
        /// <returns>A list of filtered drink menu items.</returns>
        public List<IMenuItem> GetFilteredDrinks(string searchTerm, string minCalories, string maxCalories, string minPrice, string maxPrice)
        {
            IEnumerable<IMenuItem> filteredDrinks = Drinks;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchTerms = searchTerm.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                filteredDrinks = filteredDrinks.Where(drink =>
                    searchTerms.All(term =>
                        drink.Name.ToLower().Contains(term) ||
                        drink.SpecialInstructions.Any(instruction => instruction.ToLower().Contains(term))
                    )
                );
            }

            filteredDrinks = ApplyFilterRange(filteredDrinks, minCalories, maxCalories, drink => drink.CaloriesPerEach);
            filteredDrinks = ApplyFilterRange(filteredDrinks, minPrice, maxPrice, drink => drink.Price);

            return filteredDrinks.ToList();
        }

        /// <summary>
        /// Applies a numerical range filter to a collection of menu items.
        /// </summary>
        /// <param name="items">The collection of menu items to be filtered.</param>
        /// <param name="minValue">The minimum value for the filter.</param>
        /// <param name="maxValue">The maximum value for the filter.</param>
        /// <param name="selector">A function to extract the numerical value from each menu item.</param>
        /// <returns>A filtered collection of menu items based on the numerical range.</returns>
        private IEnumerable<IMenuItem> ApplyFilterRange(IEnumerable<IMenuItem> items, string minValue, string maxValue, Func<IMenuItem, decimal> selector)
        {
            if (!string.IsNullOrWhiteSpace(minValue) && decimal.TryParse(minValue, out var min))
            {
                items = items.Where(item => selector(item) >= min);
            }

            if (!string.IsNullOrWhiteSpace(maxValue) && decimal.TryParse(maxValue, out var max))
            {
                items = items.Where(item => selector(item) <= max);
            }

            return items;
        }

    }
}
