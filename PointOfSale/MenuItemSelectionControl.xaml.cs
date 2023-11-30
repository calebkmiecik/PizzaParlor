using PizzaParlor.Data;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace PizzaParlor.PointOfSale
{
    /// <summary>
    /// Interaction logic for MenuItemSelectionControl.xaml
    /// </summary>
    public partial class MenuItemSelectionControl : UserControl
    {

        public event EventHandler<OrderItemEventArgs> CustomItem;
        public MenuItemSelectionControl()
        {
            InitializeComponent();
        }
        public void BackToMenu()
        {
            ItemSelctionContainer.Visibility = Visibility.Visible;
            WingsControlContainer.Visibility = Visibility.Hidden;
            PizzaControlContainer.Visibility = Visibility.Hidden;
            SodaControlContainer.Visibility = Visibility.Hidden;
            DrinkControlContainer.Visibility = Visibility.Hidden;
            SideControlContainer.Visibility = Visibility.Hidden;
            BreadsticksControlContainer.Visibility = Visibility.Hidden;
            CinnamonSticksControlContainer.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Handles click events
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">I don't know what this is</param>
        public void SquareClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button b)
            {
                string nameOfButton = b.Name;
                if (DataContext is Order order)
                {
                    if (nameOfButton == "BuildYourOwn")
                    {
                        Pizza pizza = new();
                        order.Add(pizza);
                        PizzaWindow(pizza);
                    }
                    if (nameOfButton == "Hawaiian")
                    {
                        HawaiianPizza pizza = new();
                        order.Add(pizza);
                        PizzaWindow(pizza);
                    }
                    if (nameOfButton == "Meats")
                    {
                        MeatsPizza pizza = new();
                        order.Add(pizza);
                        PizzaWindow(pizza);
                    }
                    if (nameOfButton == "Supreme")
                    {
                        SupremePizza pizza = new();
                        order.Add(pizza);
                        PizzaWindow(pizza);
                    }
                    if (nameOfButton == "Veggie")
                    {
                        VeggiePizza pizza = new();
                        order.Add(pizza);
                        PizzaWindow(pizza);
                    }
                    if (nameOfButton == "Breadsticks")
                    {
                        Breadsticks item = new();
                        order.Add(item);
                        ItemSelctionContainer.Visibility = Visibility.Hidden;
                        BreadsticksControlContainer.Visibility = Visibility.Visible;
                        BreadsticksControl.DataContext = item;
                    }
                    if (nameOfButton == "GarlicKnots")
                    {
                        GarlicKnots item = new();
                        order.Add(item);
                        ItemSelctionContainer.Visibility = Visibility.Hidden;
                        SideControlContainer.Visibility = Visibility.Visible;
                        SideControl.DataContext = item;
                    }
                    if (nameOfButton == "CinnamonSticks")
                    {
                        CinnamonSticks item = new();
                        order.Add(item);
                        ItemSelctionContainer.Visibility = Visibility.Hidden;
                        CinnamonSticksControlContainer.Visibility = Visibility.Visible;
                        CinnamonSticksControl.DataContext = item;
                    }
                    if (nameOfButton == "IcedTea")
                    {
                        IcedTea item = new();
                        order.Add(item); 
                        ItemSelctionContainer.Visibility = Visibility.Hidden;
                        DrinkControlContainer.Visibility = Visibility.Visible;
                        DrinkControl.DataContext = item;
                    }
                    if (nameOfButton == "Soda")
                    {
                        Soda item = new();
                        order.Add(item);
                        ItemSelctionContainer.Visibility = Visibility.Hidden;
                        SodaControlContainer.Visibility = Visibility.Visible;
                        SodaControl.DataContext = item;
                    }
                    if (nameOfButton == "Wings")
                    {
                        Wings item = new();
                        order.Add(item);
                        ItemSelctionContainer.Visibility = Visibility.Hidden;
                        WingsControlContainer.Visibility = Visibility.Visible;
                        WingsControl.DataContext = item;
                    }

                    //order.Add(Item)
                    //CustomItem?.Invoke(this, new OrderItemEventArgs(Item));
                }
            }
        }

        public void PizzaWindow (Pizza pizza)
        {
            ItemSelctionContainer.Visibility = Visibility.Hidden;
            PizzaControlContainer.Visibility = Visibility.Visible;
            PizzaControl.MainGridContainer.Visibility = Visibility.Visible;
            PizzaControl.DataContext = pizza;
        }

    }
}
