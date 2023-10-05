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
        private Queue<int> nextSongs;

        public SongPlayer(string[] songs)
        {
            InitializeComponent();
            this.songs = songs;
            currentSong = 0;
            player = new MediaPlayer();
            isPlaying = false;
            isLoop = false;
            isShuffle = false;
        }

        private void PlaySong(string path)
        {
            var uri = new Uri(path, UriKind.Relative);
            player.Open(uri);
            player.Play();
        }

        private void Play_Click(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            string path;
            if (isPlaying)
            {
                path = "pause";
                player.Pause();
            }
            else
            {
                path = "play";
                player.Play();
            }
            image.Source = new BitmapImage(new Uri(path, UriKind.Relative));
            isPlaying = !isPlaying;
        }

        private void Loop_Click(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            string path;
            if (isLoop) path = "loop";
            else path = "loop"; // special loop
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
            if (isShuffle) currentSong = nextSongs.Dequeue();
            else currentSong = (currentSong + 1) % songs.Length;
            preSongs.Append(currentSong);
            PlaySong(songs[currentSong]);
        }

        private void Shuffle_Click(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            string path;
            if (isLoop) path = "loop";
            else path = "loop"; // special shuffle
            image.Source = new BitmapImage(new Uri(path, UriKind.Relative));
            isShuffle = !isShuffle;
            // TODO: add logic for shuffle songs
        }
    }
}
