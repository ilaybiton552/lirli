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
    /// Interaction logic for SongPlayer.xaml
    /// </summary>
    public partial class SongPlayer : UserControl
    {
        private string[] songs;
        private int currentSong;
        private MediaPlayer player;
        private bool isPlaying;
        private bool isLoop;
        private bool isShuffle;
        private Queue<int> preSongs;

        public SongPlayer(string[] songs)
        {
            InitializeComponent();
            preSongs = new Queue<int>();
            this.songs = songs;
            currentSong = 0;
            isPlaying = false;
            isLoop = false;
            isShuffle = false;
            player = new MediaPlayer();
            player.MediaEnded += Player_MediaEnded;
            PlaySong();
        }

        private void PlaySong()
        {
            var uri = new Uri(songs[currentSong], UriKind.Relative);
            preSongs.Append(currentSong);
            player.Open(uri);
            player.Play();
        }

        private void Player_MediaEnded(object sender, EventArgs e)
        {
            PlayNextSong();
        }

        private void Play_Click(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            string path;
            if (isPlaying)
            {
                path = @"pause";
                player.Pause();
            }
            else
            {
                path = @"play";
                player.Play();
            }
            image.Source = new BitmapImage(new Uri(path, UriKind.Relative));
            isPlaying = !isPlaying;
        }

        private void Loop_Click(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            string path;
            if (isLoop) path = @"loop_off";
            else path = @"loop_on";
            image.Source = new BitmapImage(new Uri(path, UriKind.Relative));
            isLoop = !isLoop;
        }

        private void Start_Click(object sender, MouseButtonEventArgs e)
        {
            player.Stop();
            player.Play();
        }

        private void Next_Click(object sender, MouseButtonEventArgs e)
        {
            player.Stop();
            PlayNextSong();
        }

        private void Shuffle_Click(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            string path;
            if (isLoop) path = @"shuffle_off";
            else path = @"shuffle_on";
            image.Source = new BitmapImage(new Uri(path, UriKind.Relative));
            isShuffle = !isShuffle;
        }

        private void PlayNextSong()
        {
            if (!isLoop)
            {
                if (isShuffle) GenerateRandomSong();
                else currentSong = (currentSong + 1) % songs.Length;
            }
            PlaySong();
        }

        private void GenerateRandomSong()
        {
            Random rnd = new Random();
            int temp = currentSong;
            currentSong = rnd.Next(0, songs.Length);
            if (songs.Length > 2)
                while (currentSong != temp && currentSong != temp + 1)
                    currentSong = rnd.Next(0, songs.Length);
        }

    }
}
