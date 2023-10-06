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
        private string[] songs;

        public AnniversaryWindow()
        {
            InitializeComponent();
            LoadSongs();
            grid.Children.Add(new SongInterface(songs));
        }

        private void LoadSongs()
        {
            songs = new string[]
            {
                "Big-Jet-Plane.mp3",
                "Get-You-The-Moon.mp3",
                "You-Are-Enough.mp3",
                "Happiest-Year.mp3",
                "Here-With-Me.mp3",
                "I-GUESS-I'M-IN-LOVE.mp3",
                "Those-Eye.mp3"
            };
        }

    }
}
