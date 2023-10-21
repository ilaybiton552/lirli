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
    /// Interaction logic for FreestyleProgress.xaml
    /// </summary>
    public partial class FreestyleProgress : Window
    {
        private MediaPlayer player;
        private VideoInterface videoInterface;

        public FreestyleProgress()
        {
            InitializeComponent();
            player = new MediaPlayer();
            Uri uri = new Uri("freestyle.wav", UriKind.Relative);
            player.Open(uri);
            videoInterface = new VideoInterface(ref player);
            videoInterface.Width = 800;
            videoInterface.Height = 75;
            videoInterface.VerticalAlignment = VerticalAlignment.Top;
            grid.Children.Add(videoInterface);
            player.MediaEnded += Player_MediaEnded;
        }

        private void Player_MediaEnded(object sender, EventArgs e)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = "Back to Main";
            textBlock.FontSize = 25;
            textBlock.Foreground = Brushes.Black;
            textBlock.FontFamily = new FontFamily("David");
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            Border border = new Border();
            border.CornerRadius = new CornerRadius(5);
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(2);
            border.Width = 150;
            border.Height = 30;
            border.MouseEnter += Border_MouseEnter;
            border.MouseLeave += Border_MouseLeave;
            border.MouseLeftButtonDown += Border_MouseLeftButtonDown;
            border.Background = Brushes.White;
            border.VerticalAlignment = VerticalAlignment.Bottom;
            border.Margin = new Thickness(0, 0, 0, 5);
            border.Child = textBlock;
            grid.Children.Add(border);
            this.Height += 30;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
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

    }
}
