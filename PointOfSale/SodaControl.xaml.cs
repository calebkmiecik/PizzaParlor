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
    /// <summary>
    /// Interaction logic for SodaControl.xaml
    /// </summary>
    public partial class SodaControl : UserControl
    {
        public static readonly DependencyProperty SodaProperty = DependencyProperty.Register(
            "Soda", typeof(Soda), typeof(SodaControl));

        public Soda Soda
        {
            get { return (Soda)GetValue(SodaProperty); }
            set { SetValue(SodaProperty, value); }
        }

        public SodaControl()
        {
            InitializeComponent();
        }
    }
}
