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
    public partial class CinnamonSticksControl : UserControl
    {
        // DependencyProperty for binding to the Breadsticks instance
        public static readonly DependencyProperty CinnamonSticksProperty = DependencyProperty.Register(
            "CinnamonSticks", typeof(CinnamonSticks), typeof(CinnamonSticksControl));

        // Public property to get or set the Breadsticks instance
        public CinnamonSticks CinnamonSticks
        {
            get { return (CinnamonSticks)GetValue(CinnamonSticksProperty); }
            set { SetValue(CinnamonSticksProperty, value); }
        }

        public CinnamonSticksControl()
        {
            InitializeComponent();
        }
    }
}
