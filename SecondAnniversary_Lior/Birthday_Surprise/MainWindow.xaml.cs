using AudioSwitcher.AudioApi.CoreAudio;
using Project_API;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfAnimatedGif;

namespace Birthday_Surprise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Stopwatch stopwatch;
        private bool playGifSound = true;
        private bool playGif = true;
        private MediaPlayer mediaPlayer;
        private MediaPlayer player;
        private double confettiTime = 6.5;
        private bool mediaEnded = false;
        private double width;
        private double height;
        private CoreAudioDevice defaultPlaybackDevice;

        public MainWindow()
        {
            InitializeComponent();
            //if (DateTime.Now.Day != 31 || DateTime.Now.Month != 10)
            //{
            //    Close();
            //}

            // get the screen width and height
            width = SystemParameters.PrimaryScreenWidth;
            height = SystemParameters.PrimaryScreenHeight;

            // get the default playback devoid
            defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;

            CompositionTarget.Rendering += CompositionTarget_Rendering;
            stopwatch = new Stopwatch();
            stopwatch.Start();
            mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri("confettisound.mp3", UriKind.Relative));
            player = new MediaPlayer();
            player.Open(new Uri("birthday.mp3", UriKind.Relative));
            player.Play();
            player.Volume = .25;
            player.MediaEnded += Player_MediaEnded;
        }

        private void AddAnimationForText()
        {
            ColorAnimation animation = new ColorAnimation
            {
                To = Color.FromRgb(80, 200, 120),
                Duration = TimeSpan.FromSeconds(.5),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
            tx.Foreground = new SolidColorBrush(Color.FromRgb(21, 105, 199));
            tx.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        private void Player_MediaEnded(object sender, EventArgs e)
        {
            mediaEnded = true;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (tx.Visibility == Visibility.Visible && tx.FontSize < 150)
                tx.FontSize += 5;
            if (playGifSound && stopwatch.ElapsedMilliseconds > 1000 * (confettiTime - .6))
            {
                playGifSound = false;
                mediaPlayer.Play();
            }
            if (playGif && stopwatch.ElapsedMilliseconds > 1000 * confettiTime)
            {
                playGif = false;
                ImageBehavior.SetAnimatedSource(gifRight, new BitmapImage(new Uri("/Images/confetti.gif", UriKind.Relative)));
                ImageBehavior.SetAnimatedSource(gifLeft, new BitmapImage(new Uri("/Images/confetti.gif", UriKind.Relative)));
                tx.Visibility = Visibility.Visible;
            }
            if (defaultPlaybackDevice.IsMuted) defaultPlaybackDevice.Mute(false);
            if (defaultPlaybackDevice.Volume < 50) defaultPlaybackDevice.Volume = 50;
        }

        private void Gif_AnimationCompleted(object sender, RoutedEventArgs e)
        {
            CustomMessageBox customMessageBox = new CustomMessageBox("Well that's it lol\nJust thought it would be fun to trick you like that haha\nHappy Birthday Lior❤\n(find the close button)", "Happy Birthday");
            customMessageBox.ShowDialog();
            close.Visibility = Visibility.Visible;
            AddAnimationForText();
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            ColorAnimation animation = new ColorAnimation
            {
                To = (Color)ColorConverter.ConvertFromString("#1569C7"),
                Duration = TimeSpan.FromSeconds(.2)
            };
            border.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            ColorAnimation animation = new ColorAnimation
            {
                To = (Color)ColorConverter.ConvertFromString("#50C878"),
                Duration = TimeSpan.FromSeconds(.2)
            };
            border.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!mediaEnded)
            {
                Border border = sender as Border;
                Random rnd = new Random();
                Canvas.SetLeft(border, rnd.Next((int)width - 200));
                Canvas.SetTop(border, rnd.Next((int)height - 60));
            }
            else Close();
        }
    }
}
