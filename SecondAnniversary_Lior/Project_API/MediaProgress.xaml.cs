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

        public MediaProgress(ref MediaPlayer player)
        {
            InitializeComponent();
            this.player = player;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TimerTick;
            player.MediaOpened += Player_MediaOpened;
        }

        private void Player_MediaOpened(object sender, EventArgs e)
        {
            durationSeconds = (int)player.NaturalDuration.TimeSpan.TotalSeconds;
            duration.Text = SecondsToFormat(durationSeconds);
            progLine.StrokeThickness = 0;
            timer.Start();
        }

        private string SecondsToFormat(int TotalSeconds)
        {
            string format = "00";
            string result = (TotalSeconds / 60).ToString() + ':' + (TotalSeconds % 60).ToString(format);
            return result;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            progress.Text = SecondsToFormat((int)player.Position.TotalSeconds);
            progLine.StrokeThickness = 5;
            progLine.X2 = 50 + (player.Position.TotalSeconds / durationSeconds) * 400;
        }

    }
}
