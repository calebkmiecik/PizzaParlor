 using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Linq;

namespace PizzaParlor.Data
{
    /// <summary>
    /// Represents an order
    /// </summary>
    public class Order : INotifyPropertyChanged, INotifyCollectionChanged
    {
        private decimal _amountGiven = 0;

        public decimal AmountGiven
        {
            get { return _amountGiven; }
            set
            {
                if (value >= Total)
                {
                    _amountGiven = value;
                    OnPropertyChanged(nameof(AmountGiven));
                    OnPropertyChanged(nameof(Change));

                }
            }
        }
        private decimal _change = 0;

        public decimal Change
        {
            get 
            {
                if (AmountGiven - Total > 0)
                {
                    _change = AmountGiven - Total;
                    return _change;
                }
                return _change;
            }
            set
            {
                _change = value;
                OnPropertyChanged(nameof(Change));
            }
        }
        /// <summary>
        /// Static int that increments with each new order
        /// </summary>
        private static int _lastOrderNumber = 0;

        /// <summary>
        /// The items in the order
        /// </summary>
        private ObservableCollection<IMenuItem> _orderItems = new ObservableCollection<IMenuItem>();
        
        /// <summary>
        /// Tax rate percentage
        /// </summary>
        private decimal _taxRate = 9.15m;
        /// <summary>
        /// Order number
        /// </summary>
        public int Number { get; }
        /// <summary>
        /// When the order was placed
        /// </summary>
        public DateTime PlacedAt { get; }
        /// <summary>
        /// Collection changed handler
        /// </summary>
        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        /// <summary>
        /// Property changed handler
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        /// <summary>
        /// Constructs new order
        /// </summary>
        public Order()
        {
            Number = ++_lastOrderNumber;
            PlacedAt = DateTime.Now;

            _orderItems.CollectionChanged += HandleCollectionChanged;
        }
        /// <summary>
        /// Order items collection
        /// </summary>
        public ObservableCollection<IMenuItem> OrderItems
        {
            get => _orderItems;
            set
            {
                _orderItems = value;
                OnPropertyChanged(nameof(OrderItems));
                OnPropertyChanged(nameof(Tax));
                OnPropertyChanged(nameof(Total));
                OnPropertyChanged(nameof(Subtotal));
                OnPropertyChanged(nameof(Change));
            }
        }
        /// <summary>
        /// Subtotal cost
        /// </summary>
        public decimal Subtotal => _orderItems.Sum(item => item.Price);
        /// <summary>
        /// Tax rate get and set
        /// </summary>
        public decimal TaxRate
        {
            get => _taxRate;
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("TaxRate must be between 0 and 100.");
                }

                _taxRate = value;
                OnPropertyChanged(nameof(TaxRate));
                OnPropertyChanged(nameof(Tax));
                OnPropertyChanged(nameof(Total));
                OnPropertyChanged(nameof(Subtotal));
                OnPropertyChanged(nameof(Change));
            }
        }
        /// <summary>
        /// Tax based on the tax rate and subtotal
        /// </summary>
        public decimal Tax => Subtotal * (TaxRate / 100);
        /// <summary>
        /// Total cost of the order
        /// </summary>
        public decimal Total => Subtotal + Tax;
        /// <summary>
        /// Property changed handler
        /// </summary>
        /// <param name="propertyName">Name of property</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// Collection changed handler
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event</param>
        private void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(OrderItems));
            OnPropertyChanged(nameof(Subtotal));
            OnPropertyChanged(nameof(Tax));
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(Change));
        }

        /// <summary>
        /// Collection changed handler
        /// </summary>
        /// <param name="e">Event</param>
        protected void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
        }
        /// <summary>
        /// Add an item to the order
        /// </summary>
        /// <param name="item">Menu item to be added</param>
        /// <exception cref="ArgumentNullException">Null item</exception>
        public void Add(IMenuItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _orderItems.Add(item);


            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        public void Remove(IMenuItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var test = _orderItems.Remove(item);

            OnPropertyChanged(nameof(OrderItems));
            OnPropertyChanged(nameof(Subtotal));
            OnPropertyChanged(nameof(Tax));
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(Change));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
        }
        /// <summary>
        /// Clear the order
        /// </summary>
        public void Clear()
        {
            _orderItems.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}

