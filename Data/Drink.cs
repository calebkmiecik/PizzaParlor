using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PizzaParlor.Data
{
    /// <summary>
    /// Abstract base class representing drink menu items
    /// </summary>
    public abstract class Drink : IMenuItem, INotifyPropertyChanged
    {
        /// <summary>
        /// Property changed event handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The name of this drink
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The description of this drink
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// The price of this drink
        /// </summary>
        public abstract decimal Price { get; }

        /// <summary>
        /// The calories per each of this drink
        /// </summary>
        public abstract uint CaloriesPerEach { get; }

        /// <summary>
        /// The total calories for this drink
        /// </summary>
        public abstract uint CaloriesTotal { get; }

        /// <summary>
        /// A list of special instructions pertaining to this drink
        /// </summary>
        public abstract IEnumerable<string> SpecialInstructions { get; }

        private bool _ice = true;


        /// <summary>
        /// Does this Drink have ice in it
        /// </summary>
        public bool Ice
        {
            get { return _ice; }
            set
            {
                _ice = value;
                OnPropertyChanged(nameof(Ice));
                OnPropertyChanged(nameof(SpecialInstructions));
            }
        }

        private Size _size = Size.Medium;

        /// <summary>
        /// The size of this Drink
        /// </summary>
        public Size DrinkSize
        {
            get { return _size; }
            set
            {
                _size = value;
                OnPropertyChanged(nameof(SpecialInstructions));
                OnPropertyChanged(nameof(CaloriesPerEach));
                OnPropertyChanged(nameof(CaloriesTotal));
                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(DrinkSize));
            }
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
