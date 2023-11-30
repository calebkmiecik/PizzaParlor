using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PizzaParlor.Data;

namespace PizzaParlor.PointOfSale
{
    public class OrderItemEventArgs : RoutedEventArgs
    {
        public IMenuItem Item { get; private set; }

        public OrderItemEventArgs(IMenuItem item)
        {
            Item = item;
        }
    }
}
