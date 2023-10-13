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
    /// Interaction logic for MovingLyrics.xaml
    /// </summary>
    public partial class MovingLyrics : UserControl
    {
        private MediaPlayer player;
        private string[] lyrics;
        private double[] duration;
        private bool rightToLeft;
        private double fontSize;

        public MovingLyrics(MediaPlayer player, string lyrics, double[] duration, 
            bool rightToLeft = true, double fontSize = 30)
        {
            InitializeComponent();
            this.player = player;
            this.lyrics = lyrics.Replace("\r\n", "").Split(new string[4] { ". ", "? ", ".", "?" }, StringSplitOptions.None).ToArray();
            this.duration = duration;
            this.rightToLeft = rightToLeft;
            this.fontSize = fontSize;
            UpdateLyrics();
        }

        private void UpdateLyrics() 
        {
            StackPanel sp = lyricSP;
            if (rightToLeft) sp.FlowDirection = FlowDirection.RightToLeft;
            else sp.FlowDirection = FlowDirection.LeftToRight;
            foreach (var lyric in lyrics) 
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = lyric;
                textBlock.FontSize = fontSize;
                textBlock.Width = Width;
                textBlock.TextWrapping = TextWrapping.WrapWithOverflow;
                textBlock.Margin = new Thickness(20, 5, 20, 5);
                sp.Children.Add(textBlock);
            }
        }

    }
}
