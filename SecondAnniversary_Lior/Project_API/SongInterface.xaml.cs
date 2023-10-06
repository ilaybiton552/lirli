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

        public SongInterface(Song[] songs)
        {
            InitializeComponent();
            this.songs = songs;
            player = new MediaPlayer();
            songPlayer = new SongPlayer(songs, ref player);
            songPlayer.Width = 300;
            mediaProgress = new MediaProgress(ref player);
            mediaProgress.Width = 500;
            player.MediaEnded += Player_MediaEnded;
            songPlayerSP.Children.Add(songPlayer);
            progressSP.Children.Add(mediaProgress);
        }

        private void Player_MediaEnded(object sender, EventArgs e)
        {
            songPlayer.PlayNextSong();
        }
    }
}
