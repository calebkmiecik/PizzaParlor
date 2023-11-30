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
    public partial class WingsControl : UserControl
    {
        // DependencyProperty for binding to the Wings instance
        public static readonly DependencyProperty WingsProperty = DependencyProperty.Register(
            "Wings", typeof(Wings), typeof(WingsControl));

        // Public property to get or set the Wings instance
        public Wings Wings
        {
            get { return (Wings)GetValue(WingsProperty); }
            set { SetValue(WingsProperty, value); }
        }

        public WingsControl()
        {
            InitializeComponent();
        }
    }
}
