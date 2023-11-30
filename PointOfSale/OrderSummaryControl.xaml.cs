using PizzaParlor.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PizzaParlor.PointOfSale
{
    /// <summary>
    /// Interaction logic for OrderSummaryControl.xaml
    /// </summary>
    public partial class OrderSummaryControl : UserControl
    {
        public OrderSummaryControl()
        {
            InitializeComponent();
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Order order)
            {
                if (ItemsList.SelectedItem is IMenuItem selectedItem)
                {

                }
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if(DataContext is Order order)
            {
                if (ItemsList.SelectedItem is IMenuItem selectedItem)
                {
                    order.Remove(selectedItem);
                }
            }
        }
    }
}
