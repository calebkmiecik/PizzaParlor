﻿using PizzaParlor.Data;
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
    /// Interaction logic for DrinkControl.xaml
    /// </summary>
    public partial class DrinkControl : UserControl
    {
        public static readonly DependencyProperty DrinkProperty = DependencyProperty.Register(
            "Drink", typeof(Drink), typeof(DrinkControl));

        public Drink Drink
        {
            get { return (Drink)GetValue(DrinkProperty); }
            set { SetValue(DrinkProperty, value); }
        }

        public DrinkControl()
        {
            InitializeComponent();
        }
    }
}
