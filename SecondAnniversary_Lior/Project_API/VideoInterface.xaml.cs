using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
using System.Windows.Threading;

namespace Project_API
{
    /// <summary>
    /// Interaction logic for VideoInterface.xaml
    /// </summary>
    public partial class VideoInterface : UserControl
    {
        private MediaPlayer player;
        private bool play = false;
        private int durationSeconds = 0;
        private DispatcherTimer timer;

        public VideoInterface(ref MediaPlayer player)
        {
            InitializeComponent();
            this.player = player;
            player.MediaOpened += Player_MediaOpened;
            
            MediaVolume mediaVolume = new MediaVolume(ref player);
            mediaVolume.Width = 150;
            mediaVolume.Height = 75;
            mediaVolume.HorizontalAlignment = HorizontalAlignment.Right;
            mediaVolume.VerticalAlignment = VerticalAlignment.Top;
            mediaVolume.Margin = new Thickness(0, 0, 0, 20);
            grid.Children.Add(mediaVolume);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Player_MediaOpened(object sender, EventArgs e)
        {
            durationSeconds = (int)player.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (player.Position.TotalSeconds > 0)
            {
                if (progLine.StrokeThickness != 50) progLine.StrokeThickness = 50;
                progLine.X2 = still.X1 + player.Position.TotalSeconds / durationSeconds * (still.X2 - still.X1);
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            if (play)
            {
                image.Source = new BitmapImage(new Uri(@"images\play.png", UriKind.Relative));
                player.Pause();
            }
            else
            {
                image.Source = new BitmapImage(new Uri(@"images\pause.png", UriKind.Relative));
                player.Play();
            }
            play = !play;
        }

        private void Line_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            progLine.X2 = Mouse.GetPosition(progLine).X;
            player.Position = TimeSpan.FromSeconds((progLine.X2 - still.X1) / (still.X2 - still.X1) * durationSeconds);
        }
    }
}
