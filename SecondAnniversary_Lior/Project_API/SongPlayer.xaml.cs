﻿using System;
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
    /// Interaction logic for SongPlayer.xaml
    /// </summary>
    public partial class SongPlayer : UserControl
    {
        private Song[] songs;
        private int currentSong;
        private MediaPlayer player;
        private bool isPlaying;
        private bool isLoop;
        private bool isShuffle;
        private Queue<int> preSongs;
        private Song song;
        public Song CurrentSong { get { return song; } }

        public SongPlayer(Song[] songs, ref MediaPlayer player)
        {
            InitializeComponent();
            preSongs = new Queue<int>();
            this.songs = songs;
            currentSong = 0;
            isPlaying = true;
            isLoop = false;
            isShuffle = false;
            this.player = player;
            PlaySong();
        }

        private void PlaySong()
        {
            song = songs[currentSong];
            var uri = new Uri(song.Path, UriKind.Relative);
            preSongs.Append(currentSong);
            player.Open(uri);
            if (isPlaying)
                player.Play();
        }

        private void Play_Click(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            if (isPlaying)
            {
                image.Source = new BitmapImage(new Uri(@"images\play.png", UriKind.Relative));
                player.Pause();
            }
            else
            {
                image.Source = new BitmapImage(new Uri(@"images\pause.png", UriKind.Relative));
                player.Play();
            }
            
            isPlaying = !isPlaying;
        }

        private void Loop_Click(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            if (isLoop) image.Source = new BitmapImage(new Uri(@"images\loop_off.png", UriKind.Relative));
            else image.Source = new BitmapImage(new Uri(@"images\loop_on.png", UriKind.Relative));
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
            if (isShuffle) image.Source = new BitmapImage(new Uri(@"images\shuffle_off.png", UriKind.Relative));
            else image.Source = new BitmapImage(new Uri(@"images\shuffle_on.png", UriKind.Relative));
            isShuffle = !isShuffle;
        }

        public void PlayNextSong()
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
                while (currentSong == temp || currentSong == temp + 1)
                    currentSong = rnd.Next(0, songs.Length);
        }

    }
}
