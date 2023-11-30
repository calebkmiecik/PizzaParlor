using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PizzaParlor.Data
{
    /// <summary>
    /// The definition of the Pizza class
    /// </summary>
    public class Pizza : IMenuItem, INotifyPropertyChanged
    {
        /// <summary>
        /// Property changed event handler
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        private Crust _pizzaCrust = Crust.Original;
        /// <summary>
        /// The type of crust for this pizza
        /// </summary>
        public Crust PizzaCrust
        {
            get { return _pizzaCrust; }
            set
            {
                _pizzaCrust = value;
                OnPropertyChanged(nameof(Crust));
                OnPropertyChanged(nameof(CaloriesPerEach));
                OnPropertyChanged(nameof(CaloriesTotal));
                OnPropertyChanged(nameof(SpecialInstructions));
                OnPropertyChanged(nameof(Price));
            }
        }

        private Size _pizzaSize = Size.Medium;
        /// <summary>
        /// The size of this pizza
        /// </summary>
        public Size PizzaSize
        {
            get { return _pizzaSize; }
            set
            {
                _pizzaSize = value;
                OnPropertyChanged(nameof(Size));
                OnPropertyChanged(nameof(CaloriesPerEach));
                OnPropertyChanged(nameof(CaloriesTotal));
                OnPropertyChanged(nameof(SpecialInstructions));
                OnPropertyChanged(nameof(Price));
            }
        }
        /// <summary>
        /// The name of the Pizza instance
        /// </summary>
        public virtual string Name { get; } = "Build-Your-Own Pizza";

        /// <summary>
        /// The description of the Pizza instance
        /// </summary>
        public virtual string Description => "A pizza you get to build";

        /// <summary>
        /// The number of slices in this Pizza instance
        /// </summary>
        public uint Slices { get; } = 8;
        /// <summary>
        /// List of all the toppings this pizza could have-
        /// </summary>
        public List<PizzaTopping> PossibleToppings { get; } = new List<PizzaTopping>();

        /// <summary>
        /// The price of this Pizza instance
        /// </summary>
        public virtual decimal Price
        {
            get
            {
                decimal price = 9.99m;
                if (PizzaSize == Size.Small) { price -= 2.00m; }
                if (PizzaSize == Size.Large) { price += 2.00m; }
                if (PizzaCrust == Crust.DeepDish) { price += 1.00m; }
                foreach (var topping in PossibleToppings) 
                {
                    if (topping.OnPizza) { price += 1.00m; }
                }
                return price;
            }
        }

        /// <summary>
        /// The number of calories in each slice of this Pizza instance
        /// </summary>
        public virtual uint CaloriesPerEach
        {
            get
            {
                uint cals = 250;
                if (PizzaCrust == Crust.Thin) { cals = 150; }
                if (PizzaCrust == Crust.DeepDish) { cals = 300; }

                foreach (var topping in PossibleToppings)
                {
                    if (topping.OnPizza) cals += topping.BaseCalories;

                }

                if (PizzaSize == Size.Small) { cals = (uint)(cals * .75); }
                if (PizzaSize == Size.Large) { cals = (uint)(cals * 1.3); }
                return cals;
            }
        }

        /// <summary>
        /// The total number of calories in this Pizza instance
        /// </summary>
        public uint CaloriesTotal
        {
            get
            {
                //all pizzas have 8 slices

                return CaloriesPerEach * Slices;
            }
        }

        /// <summary>
        /// Special instructions for the preparation of this FlyingSaucer
        /// </summary>
        public virtual IEnumerable<string> SpecialInstructions
        {
            get
            {
                List<string> instructions = new();

                instructions.Add(PizzaSize.ToString());
                instructions.Add(PizzaCrust.ToString());

                foreach (var instruction in PossibleToppings) 
                { 
                    if (instruction.OnPizza) instructions.Add(instruction.Name);
                }
                return instructions;
            }
        }
        /// <summary>
        /// Constructs a new pizza item and sets all possible toppings as false
        /// </summary>
        public Pizza()
        {
            this.PossibleToppings.Clear();
            this.PossibleToppings.Add(new PizzaTopping(Topping.Sausage, false));
            this.PossibleToppings.Add(new PizzaTopping(Topping.Pepperoni, false));
            this.PossibleToppings.Add(new PizzaTopping(Topping.Ham, false));
            this.PossibleToppings.Add(new PizzaTopping(Topping.Bacon, false));
            this.PossibleToppings.Add(new PizzaTopping(Topping.Olives, false));
            this.PossibleToppings.Add(new PizzaTopping(Topping.Onions, false));
            this.PossibleToppings.Add(new PizzaTopping(Topping.Mushrooms, false));
            this.PossibleToppings.Add(new PizzaTopping(Topping.Peppers, false));
            this.PossibleToppings.Add(new PizzaTopping(Topping.Pineapple, false));
        }
        /// <summary>
        /// Edit a single topping
        /// </summary>
        /// <param name="t">The topping to be changed</param>
        /// <param name="value">Is this topping on the pizza?</param>
        public void EditToppings (Topping t, bool value)
        {
            foreach (var p in this.PossibleToppings)
            {
                if (p.ToppingType == t)
                {
                    p.OnPizza = value;
                }
            }

            OnPropertyChanged(nameof(CaloriesPerEach));
            OnPropertyChanged(nameof(CaloriesTotal));
            OnPropertyChanged(nameof(SpecialInstructions));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(PossibleToppings));
        }
        /// <summary>
        /// Does this pizza contain a specific topping
        /// </summary>
        /// <param name="t">The topping you are looking for</param>
        /// <returns>Whether or not the PossibleToppings list contains the given topping</returns>
        public bool ContainsTopping(Topping t)
        {
            foreach (PizzaTopping topping in this.PossibleToppings)
            {
                if (topping.ToppingType == t)
                {
                    return topping.OnPizza;
                }
            }
            return false;
        }
        /// <summary>
        /// Property changed handler
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}