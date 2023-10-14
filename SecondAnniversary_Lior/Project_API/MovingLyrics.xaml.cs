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

namespace Project_API
{
    /// <summary>
    /// Interaction logic for MovingLyrics.xaml
    /// </summary>
    public partial class MovingLyrics : UserControl
    {
        private MediaPlayer player;
        private double[] duration;
        private bool rightToLeft;
        private double fontSize;
        private int currentTextBlockIndex = 0;
        private TextBlock currentTextBlock;
        private TextBlock[] lyrics;
        private bool isMediaStart = false;
        private bool pressed = false;

        public bool Pressed { set { pressed = value; } }

        public MovingLyrics(ref MediaPlayer player, string lyrics, double[] duration,
            bool rightToLeft = true, double fontSize = 30)
        {
            InitializeComponent();
            this.player = player;
            this.duration = duration;
            this.rightToLeft = rightToLeft;
            this.fontSize = fontSize;
            UpdateLyrics(lyrics.Replace("\r\n", "").Split(new string[4] { ". ", "? ", ".", "?" }, StringSplitOptions.None));
            player.MediaOpened += Player_MediaOpened;
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void Player_MediaOpened(object sender, EventArgs e)
        {
            isMediaStart = true;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (isMediaStart)
            {
                if (player.Position.TotalSeconds < player.NaturalDuration.TimeSpan.TotalSeconds && currentTextBlockIndex < duration.Length)
                {
                    if (duration[currentTextBlockIndex] <= player.Position.TotalSeconds)
                    {
                        if (currentTextBlockIndex != 0)
                        {
                            Point position = currentTextBlock.TransformToAncestor(this).Transform(new Point(0, 0));
                            if (position.Y > 40 || position.Y < 10)
                            {
                                scroller.ScrollToVerticalOffset(currentTextBlock.ActualHeight / 2 + position.Y - 40 + scroller.VerticalOffset);
                            }
                            if (!currentTextBlock.IsMouseOver)
                            {
                                currentTextBlock.Foreground = Brushes.Gray;
                                currentTextBlock.FontWeight = FontWeights.Normal;
                            }
                            currentTextBlock = lyrics[currentTextBlockIndex];
                        }
                        else currentTextBlock = lyrics[0];
                        currentTextBlock.Foreground = Brushes.Blue;
                        currentTextBlock.FontWeight = FontWeights.Bold;
                        currentTextBlockIndex++;
                    }
                    if (pressed)
                    {
                        pressed = false;
                        currentTextBlockIndex = GetCurrentIndex();
                        ModifyTextBlocks(currentTextBlockIndex);
                        Point position = currentTextBlock.TransformToAncestor(this).Transform(new Point(0, 0));
                        if (position.Y > 40 || position.Y < 10)
                        {
                            scroller.ScrollToVerticalOffset(currentTextBlock.ActualHeight / 2.5 + position.Y - 40 + scroller.VerticalOffset);
                        }
                    }
                }
            }
        }

        private void UpdateLyrics(string[] lyrics) 
        {
            int count = 0;
            StackPanel sp = lyricSP;
            if (rightToLeft) sp.FlowDirection = FlowDirection.RightToLeft;
            else sp.FlowDirection = FlowDirection.LeftToRight;
            foreach (var lyric in lyrics) 
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = lyric;
                textBlock.FontSize = fontSize;
                textBlock.Width = Width;
                textBlock.Tag = count++;
                textBlock.TextWrapping = TextWrapping.WrapWithOverflow;
                textBlock.Margin = new Thickness(20, 5, 20, 5);
                textBlock.MouseEnter += TextBlock_MouseEnter;
                textBlock.MouseLeave += TextBlock_MouseLeave;
                textBlock.MouseLeftButtonDown += TextBlock_MouseLeftButtonDown;
                sp.Children.Add(textBlock);
            }
            this.lyrics = new TextBlock[sp.Children.Count];
            sp.Children.CopyTo(this.lyrics, 0);
        }

        private void ModifyTextBlocks(int index)
        {
            for (int i = 0; i < lyrics.Length; i++) 
            {
                if (i == index)
                {
                    currentTextBlock = lyrics[i];
                    currentTextBlock.Foreground = Brushes.Blue;
                    currentTextBlock.FontWeight = FontWeights.Bold;
                }
                else if (!lyrics[i].IsMouseOver)
                {
                    if (i < index) lyrics[i].Foreground = Brushes.Gray;
                    else if (i > index) lyrics[i].Foreground = Brushes.Black;
                    lyrics[i].FontWeight = FontWeights.Normal;
                }
            }
        }

        private int GetCurrentIndex()
        {
            int index = 0;
            for (index = 0; index < duration.Length; index++) 
                if (player.Position.TotalSeconds < duration[index]) 
                    break;
            return index;
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            currentTextBlockIndex = (int)textBlock.Tag;
            ModifyTextBlocks(currentTextBlockIndex);
            player.Position = TimeSpan.FromSeconds(duration[currentTextBlockIndex]);
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (currentTextBlock == null || !currentTextBlock.Equals(textBlock))
            {
                if (duration[(int)textBlock.Tag] < player.Position.TotalSeconds) textBlock.Foreground = Brushes.Gray;
                else textBlock.Foreground = Brushes.Black;
                textBlock.FontWeight = FontWeights.Normal;
            }
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            textBlock.Foreground = Brushes.Blue;
            textBlock.FontWeight = FontWeights.Bold;
        }

    }
}
