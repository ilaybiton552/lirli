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
        private string highlightColor;
        private string circleColor;
        private string progressColor;

        public MediaProgress(ref MediaPlayer player, string highlightColor = "#00C22C", 
            string circleColor = "#000000", string progressColor = "#000000", int width = 500)
        {
            InitializeComponent();
            this.player = player;
            this.highlightColor = highlightColor;
            this.circleColor = circleColor;
            this.progressColor = progressColor;

            Width = width;
            still.X2 = width - 50;
            progLine.Stroke = (SolidColorBrush)new BrushConverter().ConvertFrom(progressColor);
            cir.Stroke = (SolidColorBrush)new BrushConverter().ConvertFrom(circleColor);

            mouseDown = false;
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
                if (mouseDown)
                {
                    double xMouse = Mouse.GetPosition(progLine).X;
                    if (xMouse >= still.X1 && xMouse <= still.X2)
                    {
                        cir.X1 = cir.X2 = progLine.X2 = xMouse;
                        progress.Text = SecondsToFormat((int)((xMouse - still.X1) / (still.X2 - still.X1) * durationSeconds));
                    }
                    else if (xMouse > still.X2) progress.Text = duration.Text;
                    else if (duration.Text.Length == 5) progress.Text = "00:00";
                    else progress.Text = "0:00";
                    if (Mouse.LeftButton == MouseButtonState.Released) // left button mouse up
                    {
                        player.Position = TimeSpan.FromSeconds((xMouse - 50) / (still.X2 - still.X1) * durationSeconds);
                        mouseDown = false;
                        progLine.Stroke = (SolidColorBrush)new BrushConverter().ConvertFrom(highlightColor);
                        cir.StrokeThickness = 0;
                    }
                }
                else if (text != string.Empty) progress.Text = text;
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
            if (progLine.StrokeThickness != 5 && player.Position.TotalSeconds > 0.5) progLine.StrokeThickness = 5;
            if (!mouseDown) cir.X1 = cir.X2 = progLine.X2 = 50 + player.Position.TotalSeconds / durationSeconds * (still.X2 - still.X1);
        }

        private void Line_MouseEnter(object sender, MouseEventArgs e)
        {
            progLine.Stroke = (SolidColorBrush)new BrushConverter().ConvertFrom(highlightColor);
            cir.StrokeThickness = 10;
        }

        private void Line_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!mouseDown)
            {
                progLine.Stroke = (SolidColorBrush)new BrushConverter().ConvertFrom(progressColor);
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
            player.Position = TimeSpan.FromSeconds((xMouse - 50) / (still.X2 - still.X1) * durationSeconds);
        }
    }
}
