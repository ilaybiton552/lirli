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
using System.Windows.Shapes;

namespace Project_API
{
    /// <summary>
    /// Interaction logic for AnniversaryWindow.xaml
    /// </summary>
    public partial class AnniversaryWindow : Window
    {
        private Song[] songs;

        public AnniversaryWindow()
        {
            InitializeComponent();
            LoadSongs();
            grid.Children.Add(new SongInterface(songs));
        }

        private void LoadSongs()
        {
            songs = new Song[]
            {
                new Song("Big Jet Plane", "Angus & Julia Stone", "Big-Jet-Plane.mp3"),
                new Song("Get You The Moon (feat. Snow)", "Kina, Snow", "Get-You-The-Moon.mp3"),
                new Song("You Are Enough", "Sleeping At Last", "You-Are-Enough.mp3"),
                new Song("Happiest Year", "Jaymes Young", "Happiest-Year.mp3"),
                new Song("Here With Me", "d4vd", "Here-With-Me.mp3"),
                new Song("I GUESS I'M IN LOVE", "Clinton Kane", "I-GUESS-I'M-IN-LOVE.mp3"),
                new Song("Those Eyes", "New West", "Those-Eyes.mp3")
            };
        }

    }

    public class Song
    {
        private string name;
        private string author;
        private string path;

        public Song(string name, string author, string path)
        {
            this.name = name;
            this.author = author;
            this.path = path;
        }

        public string Name { get { return name; } }
        public string Author { get { return author; } }
        public string Path { get { return path; } }
    }

}
