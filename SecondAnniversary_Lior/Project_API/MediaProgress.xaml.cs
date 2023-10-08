using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace Project_API
{
    /// <summary>
    /// Interaction logic for MediaProgress.xaml
    /// </summary>
    public partial class MediaProgress : UserControl
    {
        private MediaPlayer player;
        private DispatcherTimer timer;
        private int durationSeconds = 0;
        private Stopwatch watch;
        private bool mouseDown;
        private bool mouseIn;

        public MediaProgress(ref MediaPlayer player)
        {
            InitializeComponent();
            this.player = player;
            mouseDown = false;
            mouseIn = false;
            watch = new Stopwatch();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TimerTick;
            player.MediaOpened += Player_MediaOpened;
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (watch.IsRunning)
            {
                string text = SecondsToFormat((int)player.Position.TotalSeconds);
                if (text != string.Empty) progress.Text = text;
                if (mouseDown)
                {
                    double xMouse = Mouse.GetPosition(progLine).X;
                    if (xMouse >= still.X1 && xMouse <= still.X2)
                    {
                        cir.X1 = cir.X2 = progLine.X2 = xMouse;
                    }
                    if (Mouse.LeftButton == MouseButtonState.Released) // left button mouse up
                    {
                        player.Position = TimeSpan.FromSeconds((xMouse - 50) / 400 * durationSeconds);
                        mouseDown = false;
                        progLine.Stroke = Brushes.Black;
                        cir.StrokeThickness = 0;
                    }
                }
            }
        }

        private void Player_MediaOpened(object sender, EventArgs e)
        {
            timer.Stop();
            watch.Start();
            progLine.StrokeThickness = 0;
            durationSeconds = (int)player.NaturalDuration.TimeSpan.TotalSeconds;
            duration.Text = SecondsToFormat(durationSeconds);
            if (duration.Text.Length == 5)
            {
                progress.Text = "00:00";
                progress.Margin = new Thickness(18, 0, 0, 0);
                duration.Margin = new Thickness(0, 0, 18, 0);
            }
            else progress.Text = "0:00";
            timer.Start();
        }

        private string SecondsToFormat(int TotalSeconds)
        {
            if (TotalSeconds == 0) return string.Empty;
            string format = "00";
            string sec = ':' + (TotalSeconds % 60).ToString(format);
            string result = (TotalSeconds / 60).ToString();
            if (duration.Text.Length == 5) result = (TotalSeconds / 60).ToString(format);
            return result + sec;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (progLine.StrokeThickness != 5) progLine.StrokeThickness = 5;
            if (!mouseDown) cir.X1 = cir.X2 = progLine.X2 = 50 + player.Position.TotalSeconds / durationSeconds * 400;
        }

        private void Line_MouseEnter(object sender, MouseEventArgs e)
        {
            mouseIn = true;
            progLine.Stroke = (SolidColorBrush)new BrushConverter().ConvertFrom("#00C22C");
            cir.StrokeThickness = 10;
        }

        private void Line_MouseLeave(object sender, MouseEventArgs e)
        {
            mouseIn = false;
            if (!mouseDown)
            {
                progLine.Stroke = Brushes.Black;
                cir.StrokeThickness = 0;
            }
        }

        private void Cir_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mouseDown = true;
        }

        private void Line_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            double xMouse = Mouse.GetPosition(progLine).X;
            cir.X1 = cir.X2 = progLine.X2 = xMouse;
            player.Position = TimeSpan.FromSeconds((xMouse - 50) / 400 * durationSeconds);
        }
    }
}
