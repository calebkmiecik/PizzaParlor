using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Linq;

namespace PizzaParlor.Data
{
    /// <summary>
    /// Abstract base class representing side menu items
    /// </summary>
    public abstract class Side : IMenuItem, INotifyPropertyChanged
    {
        /// <summary>
        /// Property changed handler
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        /// <summary>
        /// Property changed handler
        /// </summary>
        /// <param name="propertyName">Name of property</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// The name of this side item
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The description of this side item
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// The count of this side item
        /// </summary>
        private uint _count = 8;
        public virtual uint SideCount
        {
            get { return _count; }
            set
            {
                if (value > 13)
                {
                    _count = 13;
                }
                else if (value < 4)
                {
                    _count = 4;
                }
                else
                {
                    _count = value;
                }
                OnPropertyChanged(nameof(SideCount));
                OnPropertyChanged(nameof(CaloriesTotal));
                OnPropertyChanged(nameof(CaloriesPerEach));
                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(SpecialInstructions));
            }
        }

        /// <summary>
        /// The price of this side item
        /// </summary>
        public abstract decimal Price { get; }

        /// <summary>
        /// The calories per each of this side item
        /// </summary>
        public abstract uint CaloriesPerEach { get; }

        /// <summary>
        /// The total number of calories in this Wings instance
        /// </summary>
        public uint CaloriesTotal
        {
            get
            {
                return CaloriesPerEach * SideCount;
                OnPropertyChanged(nameof(CaloriesTotal));
            }
        }

        /// <summary>
        /// A list of special instructions pertaining to this side item
        /// </summary>
        public abstract IEnumerable<string> SpecialInstructions { get; }
    }
}