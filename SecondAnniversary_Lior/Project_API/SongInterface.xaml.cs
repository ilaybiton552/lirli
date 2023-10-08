using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Xml.Serialization;

namespace Project_API
{
    /// <summary>
    /// Interaction logic for SongInterface.xaml
    /// </summary>
    public partial class SongInterface : UserControl
    {
        private Song[] songs;
        private MediaPlayer player;
        private MediaProgress mediaProgress;
        private SongPlayer songPlayer;
        private Song currentSong;

        public SongInterface(Song[] songs)
        {
            InitializeComponent();
            this.songs = songs;
            WindowSetting();
            this.DataContext = currentSong;
        }

        private void WindowSetting()
        {
            player = new MediaPlayer();
            songPlayer = new SongPlayer(songs, ref player);
            songPlayer.Width = 200;
            songPlayer.VerticalAlignment = VerticalAlignment.Center;
            songPlayer.Margin = new Thickness(0, 0, 0, 5);
            mediaProgress = new MediaProgress(ref player);
            mediaProgress.Width = 500;
            mediaProgress.VerticalAlignment = VerticalAlignment.Bottom;
            grid.Children.Add(songPlayer);
            grid.Children.Add(mediaProgress);
            SongDetails();
            player.MediaEnded += Player_MediaEnded;
        }

        private void SongDetails()
        {
            currentSong = songPlayer.CurrentSong;

        }

        private void Player_MediaEnded(object sender, EventArgs e)
        {
            songPlayer.PlayNextSong();
        }
    }
}
