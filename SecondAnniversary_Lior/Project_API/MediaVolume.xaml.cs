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

namespace Project_API
{
    /// <summary>
    /// Interaction logic for MediaVolume.xaml
    /// </summary>
    public partial class MediaVolume : UserControl
    {
        private MediaPlayer player;
        private bool mouseDown;
        private bool mute;
        private double lastProgress;

        public MediaVolume(MediaPlayer player, double mediaVolume = 1)
        {
            InitializeComponent();
            this.player = player;
            lastProgress = player.Volume = mediaVolume;
            cir.X1 = cir.X2 = progLine.X2 = lastProgress * 100 + still.X1; 
            mouseDown = false;
            mute = false;
            CompositionTarget.Rendering += CompositionTarget_Rendering; 
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (mouseDown)
            {
                if (progLine.StrokeThickness == 0) progLine.StrokeThickness = 5;
                double xMouse = Mouse.GetPosition(progLine).X;
                double currVolume = xMouse - still.X1;
                ChangeVolume(currVolume);
                if (xMouse >= still.X1 && xMouse <= still.X2)
                {
                    cir.X1 = cir.X2 = progLine.X2 = xMouse;
                    player.Volume = currVolume / 100;
                    if (mute) mute = false;
                }
                else
                {
                    if (xMouse < still.X1)
                    {
                        player.Volume = 0;
                        progLine.StrokeThickness = 0;
                        mute = true;
                    }
                    else player.Volume = 1;
                }
                if (Mouse.LeftButton == MouseButtonState.Released)
                {
                    if (xMouse > still.X1) lastProgress = currVolume;
                    progLine.Stroke = Brushes.Black;
                    cir.StrokeThickness = 0;
                    mouseDown = false;
                }
            }
        }

        private void Line_MouseEnter(object sender, MouseEventArgs e)
        {
            progLine.Stroke = (SolidColorBrush)new BrushConverter().ConvertFrom("#00C22C");
            cir.StrokeThickness = 10;
        }

        private void Line_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!mouseDown)
            {
                progLine.Stroke = Brushes.Black;
                cir.StrokeThickness = 0;
            }
        }

        private void Volume_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mute)
            {
                player.Volume = lastProgress;
                progLine.StrokeThickness = 5;
                cir.X1 = cir.X2 = progLine.X2 = player.Volume * 100 + still.X1;
            }
            else
            {
                player.Volume = 0;
                cir.X1 = cir.X2 = still.X1;
                progLine.StrokeThickness = 0;
            }
            mute = !mute;
            ChangeVolume(player.Volume);
        }

        private void ChangeVolume(double currentVolume)
        {
            if (currentVolume <= 0) volume.Source = new BitmapImage(new Uri(@"images\mute-volume.png", UriKind.Relative));
            else if (currentVolume < .33) volume.Source = new BitmapImage(new Uri(@"images\low-volume.png", UriKind.Relative));
            else if (currentVolume > .66) volume.Source = new BitmapImage(new Uri(@"images\high-volume.png", UriKind.Relative));
            else volume.Source = new BitmapImage(new Uri(@"images\volume.png", UriKind.Relative));
        }

        private void Cir_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mouseDown = true;
        }

        private void Line_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            double xMouse = Mouse.GetPosition(progLine).X;
            if (progLine.StrokeThickness == 0) progLine.StrokeThickness = 5;
            if (mute) mute = false;
            progLine.X2 = cir.X1 = cir.X2 = xMouse;
            lastProgress = player.Volume = (xMouse - still.X1) / 100;
            ChangeVolume(lastProgress);
        }

    }
}
