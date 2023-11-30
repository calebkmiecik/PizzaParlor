using PizzaParlor.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PizzaParlor.PointOfSale
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public void OnCustomize(object? sender, OrderItemEventArgs e)
        {
            UserControl control;
      
            if (e.Item is Pizza)
            {
                control = PizzaControl;

            }

            //control.Visibility = Visibility.Visible;
        }
        public MainWindow()
        {
            InitializeComponent();
            Order order = new Order();
            DataContext = order;
          //  MenuItemSelection.CustomItem += window switch method
        }
        /// <summary>
        /// Click event handler for buttons in main window
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event</param>
        public void SquareClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button b)
            {
                string nameOfButton = b.Name;
                if (DataContext is Order order)
                {
                    if (nameOfButton == "Cancel")
                    {
                        NewOrder(order);
                    }
                }
            }
        }
        public void BackToMenuClick(object sender, RoutedEventArgs e)
        {
            MenuItemSelection.BackToMenu();
            MenuItemContainer.Visibility = Visibility.Visible;
            PaymentContainer.Visibility = Visibility.Hidden;
            SummaryContainer.Visibility = Visibility.Visible;
        }
        private void CompleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Order order)
            {

            }
            MenuItemContainer.Visibility = Visibility.Hidden;
            PaymentContainer.Visibility = Visibility.Visible;
        }
        public void NewOrder(Order order)
        {
            order.Clear();
            MenuItemSelection.BackToMenu();
            DataContext = new Order();
            MenuItemSelection.BackToMenu();
            MenuItemContainer.Visibility = Visibility.Visible;
            PaymentContainer.Visibility = Visibility.Hidden;
            SummaryContainer.Visibility = Visibility.Visible;
        }
    }
}
