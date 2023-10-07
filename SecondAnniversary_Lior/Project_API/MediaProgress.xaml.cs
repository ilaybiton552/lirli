﻿using System;
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

        public MediaProgress(ref MediaPlayer player)
        {
            InitializeComponent();
            this.player = player;
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
            }
        }

        private void Player_MediaOpened(object sender, EventArgs e)
        {
            timer.Stop();
            watch.Start();
            progress.Text = "0:00";
            progLine.StrokeThickness = 0;
            durationSeconds = (int)player.NaturalDuration.TimeSpan.TotalSeconds;
            duration.Text = SecondsToFormat(durationSeconds);
            timer.Start();
        }

        private string SecondsToFormat(int TotalSeconds)
        {
            if (TotalSeconds == 0) return string.Empty;
            string format = "00";
            string result = (TotalSeconds / 60).ToString() + ':' + (TotalSeconds % 60).ToString(format);
            return result;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            progLine.StrokeThickness = 5;
            cir.X1 = cir.X2 = progLine.X2 = 50 + (player.Position.TotalSeconds / durationSeconds) * 400;
        }

        private void Line_MouseEnter(object sender, MouseEventArgs e)
        {
            progLine.Stroke = (SolidColorBrush)new BrushConverter().ConvertFrom("#00C22C");
            cir.StrokeThickness = 10;
        }

        private void Line_MouseLeave(object sender, MouseEventArgs e)
        {
            progLine.Stroke = Brushes.White;
            cir.StrokeThickness = 0;
        }
    }
}
