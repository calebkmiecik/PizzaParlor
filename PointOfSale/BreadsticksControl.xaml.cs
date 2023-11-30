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
    public partial class BreadsticksControl : UserControl
    {
        // DependencyProperty for binding to the Breadsticks instance
        public static readonly DependencyProperty BreadsticksProperty = DependencyProperty.Register(
            "Breadsticks", typeof(Breadsticks), typeof(BreadsticksControl));

        // Public property to get or set the Breadsticks instance
        public Breadsticks Breadsticks
        {
            get { return (Breadsticks)GetValue(BreadsticksProperty); }
            set { SetValue(BreadsticksProperty, value); }
        }

        public BreadsticksControl()
        {
            InitializeComponent();
        }
    }
}
