using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
        private MediaVolume mediaVolume;
        private double nameOffset = 0;
        private double authorOffset = 0;
        private DispatcherTimer nameTimer;
        private DispatcherTimer authorTimer;
        private bool isNameScroll = false;
        private bool isAuthorScroll = false;
        private bool isNameBackword = false;
        private bool isAuthorBackword = false;
        private bool waitName = true;
        private bool waitAuthor = true;

        public SongInterface(Song[] songs)
        {
            InitializeComponent();
            this.songs = songs;
            WindowSetting();
            nameTimer = new DispatcherTimer();
            nameTimer.Interval = TimeSpan.FromSeconds(2);
            nameTimer.Tick += Name_Tick;
            nameTimer.Start();
            authorTimer = new DispatcherTimer();
            authorTimer.Interval = TimeSpan.FromSeconds(2);
            authorTimer.Tick += Author_Tick;
            authorTimer.Start();
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void Name_Tick(object sender, EventArgs e)
        {
            if (waitName) waitName = false;
            else
            {
                if (!isNameScroll)
                {
                    isNameScroll = true;
                    nameTimer.Stop();
                }
            }
        }

        private void Author_Tick(object sender, EventArgs e)
        {
            if (waitAuthor) waitAuthor = false;
            else
            {
                if (!isAuthorScroll)
                {
                    isAuthorScroll = true;
                    authorTimer.Stop();
                }
            }
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (player.Position.TotalSeconds == 0)
            {
                SongDetails();
                isNameBackword = isNameScroll = isAuthorBackword = isAuthorScroll = false;
                nameOffset = authorOffset = 0;
                waitName = waitAuthor = true;
                nameScroll.ScrollToLeftEnd();
                authorScroll.ScrollToLeftEnd();
                authorTimer.Stop();
                nameTimer.Stop();
                authorTimer.Start();
                nameTimer.Start();
            }
            else
            {
                if (!waitName)
                {
                    HandleScrolling(ref nameScroll, ref isNameBackword, ref isNameScroll, ref nameOffset, ref nameTimer, ref waitName);
                }
                if (!waitAuthor)
                {
                    HandleScrolling(ref authorScroll, ref isAuthorBackword, ref isAuthorScroll, ref authorOffset, ref authorTimer, ref waitAuthor);
                }
            }
        }

        private void HandleScrolling(ref ScrollViewer scrollViewer, ref bool backword, ref bool scroll, ref double offset, ref DispatcherTimer timer, ref bool wait)
        {
            if (scrollViewer.HorizontalOffset == 0 && backword)
            {
                scroll = backword = false;
                wait = true;
                timer.Start();
            }
            else if (offset <= scrollViewer.HorizontalOffset + 1 && !backword && scroll)
            {
                scrollViewer.ScrollToHorizontalOffset(offset);
                offset += .2;
            }
            else if (scroll)
            {
                if (!backword)
                {
                    wait = backword = true;
                    timer.Start();
                }
                else
                {
                    scrollViewer.ScrollToHorizontalOffset(offset);
                    offset -= .2;
                }
            }
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
            mediaVolume = new MediaVolume(player, 0.5);
            mediaVolume.Width = 150;
            mediaVolume.HorizontalAlignment = HorizontalAlignment.Right;
            mediaVolume.Margin = new Thickness(0, 0, 0, 0);
            grid.Children.Add(songPlayer);
            grid.Children.Add(mediaProgress);
            grid.Children.Add(mediaVolume);
            player.MediaEnded += Player_MediaEnded;
        }

        private void SongDetails()
        {
            currentSong = songPlayer.CurrentSong;
            name.Text = currentSong.Name;
            author.Text = currentSong.Author;
        }

        private void Player_MediaEnded(object sender, EventArgs e)
        {
            songPlayer.PlayNextSong();
        }
    }
}
