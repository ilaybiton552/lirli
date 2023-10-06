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
        private string[] songs;

        public SongInterface(string[] songs)
        {
            InitializeComponent();
            this.songs = songs;
            grid.Children.Add(new SongPlayer(songs));
        }

    }
}
