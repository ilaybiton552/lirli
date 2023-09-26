﻿using System;
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

namespace Project_API
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            tbPass.Text = string.Empty;
            tbPass.Foreground = Brushes.Black;
            tbPass.GotFocus -= Password_GotFocus;
        }


        private void Password_PreviewKeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
