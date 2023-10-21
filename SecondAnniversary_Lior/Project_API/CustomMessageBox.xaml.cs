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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project_API
{
    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox(string content)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            tbContent.Text = content;
        }

        public CustomMessageBox(string content, string title)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            tbContent.Text = content;
            tbTitle.Text = title;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            ColorAnimation animation = new ColorAnimation
            {
                To = Color.FromRgb(255, 255, 255),
                Duration = TimeSpan.FromSeconds(.2)
            };
            border.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            border.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFF");
            ColorAnimation animation = new ColorAnimation
            {
                To = (Color)ColorConverter.ConvertFromString("#1569C7"),
                Duration = TimeSpan.FromSeconds(.2)
            };
            border.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        private void Ok_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
