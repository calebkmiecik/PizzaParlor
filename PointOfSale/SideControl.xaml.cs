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
    public partial class SideControl : UserControl
    {
        
        public static readonly DependencyProperty SideProperty = DependencyProperty.Register(
            "Side", typeof(Side), typeof(SideControl));

        public Side Side
        {
            get { return (Side)GetValue(SideProperty); }
            set { SetValue(SideProperty, value); }
        }

        public SideControl()
        {
            InitializeComponent();
        }
    }
}
