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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PizzaParlor.PointOfSale
{
    public partial class PizzaControl : UserControl
    { 
        public static readonly DependencyProperty PizzaProperty = DependencyProperty.Register(
            "Pizza", typeof(Pizza), typeof(PizzaControl));
        public Pizza Pizza
        {
            get { return (Pizza)GetValue(PizzaProperty); }
            set { SetValue(PizzaProperty, value); }
        }

        public PizzaControl()
        {
            InitializeComponent();
        }

    }
}
