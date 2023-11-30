using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaParlor.Data
{
    public class PizzaTopping : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;


        /// <summary>
        /// The type of topping
        /// </summary>
        public Topping ToppingType { get; init; }

        private bool _onPizza;
        /// <summary>
        /// Is this topping on the pizza?
        /// </summary>
        public bool OnPizza 
        { 
            get { return _onPizza; }
            set
            {
                _onPizza = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OnPizza)));
            }
        }
        /// <summary>
        /// Name of this topping
        /// </summary>
        public string Name
        {
            get
            {
                     if (this.ToppingType == Topping.Sausage) { return "Sausage"; }
                else if (this.ToppingType == Topping.Pepperoni) { return "Pepperoni"; }
                else if (this.ToppingType == Topping.Ham) { return "Ham"; }
                else if (this.ToppingType == Topping.Bacon) { return "Bacon"; }
                else if (this.ToppingType == Topping.Olives) { return "Olives"; }
                else if (this.ToppingType == Topping.Onions) { return "Onions"; }
                else if (this.ToppingType == Topping.Mushrooms) { return "Mushrooms"; }
                else if (this.ToppingType == Topping.Peppers) { return "Peppers"; }
                else if (this.ToppingType == Topping.Pineapple) { return "Pineapple"; }
                else return "NULL";
            }
        }
        /// <summary>
        /// The base calories of this topping
        /// </summary>
        public uint BaseCalories
        {
            get
            {
                if (this.ToppingType == Topping.Sausage) { return 30; }
                else if (this.ToppingType == Topping.Pepperoni ||
                         this.ToppingType == Topping.Ham ||
                         this.ToppingType == Topping.Bacon) { return 20; }
                else if (this.ToppingType == Topping.Pineapple) { return 10; }
                else return 5;
            }
        }
        /// <summary>
        /// Constructor for a PizzaTopping item
        /// </summary>
        /// <param name="type">What kind of topping?</param>
        /// <param name="on">Is this topping on the pizza?</param>
        public PizzaTopping (Topping type, bool on)
        {
            this.ToppingType = type;
            this.OnPizza = on;
        }
    }
}
