using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
        private MediaPlayer player = new MediaPlayer();
        private VideoInterface videoInterface;
        private MovingLyrics movingLyrics;
        private SongInterface songInterface;

        public AnniversaryWindow()
        {
            InitializeComponent();
            LoadSongs();
            CreateLyrics();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            songInterface = new SongInterface(songs);
            songInterface.Width = 800;
            songInterface.Height = 75;
            songInterface.VerticalAlignment = VerticalAlignment.Bottom;
            songInterface.Margin = new Thickness(0, 0, 0, 15);

            var uri = new Uri("anniversary.wav", UriKind.Relative);
            player.Open(uri);
            player.MediaEnded += Player_MediaEnded;
            videoInterface = new VideoInterface(ref player);
            videoInterface.Width = 800;
            videoInterface.Height = 75;
            videoInterface.VerticalAlignment = VerticalAlignment.Top;

            grid.Children.Add(songInterface);
            grid.Children.Add(videoInterface);

            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void Player_MediaEnded(object sender, EventArgs e)
        {
            MessageBox.Show("That's it for this window!\nYou can go back to the main window\n you can also stay here and listen to the songs :)", "Notice", MessageBoxButton.OK);
            movingLyrics.Height = 200;
            songInterface.Margin = new Thickness(0, 0, 0, 40);
            TextBlock textBlock = new TextBlock();
            textBlock.Text = "Back to Main";
            textBlock.FontSize = 25;
            textBlock.Foreground = Brushes.Black;
            textBlock.FontFamily = new FontFamily("David");
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            Border border = new Border();
            border.CornerRadius = new CornerRadius(5);
            border.BorderBrush = Brushes.Black;
            border.BorderThickness = new Thickness(2);
            border.Width = 150;
            border.Height = 30;
            border.MouseEnter += Border_MouseEnter;
            border.MouseLeave += Border_MouseLeave;
            border.MouseLeftButtonDown += Border_MouseLeftButtonDown;
            border.Background = Brushes.White;
            border.VerticalAlignment = VerticalAlignment.Bottom;
            border.Margin = new Thickness(0, 0, 0, 5);
            border.Child = textBlock;
            grid.Children.Add(border);
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            songInterface.Player.Stop();
            Close();
            mainWindow.ShowDialog();
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = Brushes.White;
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Border).Background = Brushes.AliceBlue;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (videoInterface.Pressed)
            {
                videoInterface.Pressed = false;
                movingLyrics.Pressed = true;
            }
        }

        private void CreateLyrics()
        {
            string lyrics = "היי לירלי.\r\nאני בטוחה שלא היה כל כך קשה לנחש את זה הא?\r\nאת האמת שאני לא כל כך יודעת מה אני רוצה לעשות כאן.\r\nעכשיו זה מרגיש פשוט מוזר לעשות את זה.\r\nכאילו זה נגמר. אבל אני חייבת לך. אני חייבת למה שהיה לנו.\r\nאני רוצה להגיד תודה.\r\nתודה על כל מה שהיה. תודה על כל שניה של אהבה חסרת גבולות. תודה על כל שניה של אכפתיות. תודה על כל שניה של דאגה. תודה על כל רגע מושלם איתך.\r\nתודה שתמיד היית שם בשבילי. ביום, בלילה, בכל רגע נתון שהייתי צריכה. תודה שהתעוררת בשבילי בלילה למרות שהיו לך דברים לעשות. תודה שנשארת ערה בשבילי כדי לוודא שאני בסדר. תודה שתמיד דאגת לשלומי.\r\nתודה על כל בוקר טוב. תודה על כל לילה טוב. תודה על כל שיחה. תודה על כל חיוך שהעלית לי על הפנים. תודה על כל צחוק. תודה על כל תשוקה. תודה על פרפרים בבטן. תודה על כל טיפה של אהבה.\r\nתודה על כל משחק. תודה על כל שטות. תודה על זה שתמיד זרמת איתי, גם עם דברים שאת לא אוהבת.\r\nתודה על שראית איתי דרגון בול למרות שלא כל כך אהבת. תודה ששיחקת איתי סודוקו למרות שעשיתי כמעט הכל. תודה שראית איתי סרטים, למרות שהם לא עניינו אותך. תודה שראית איתי סרטונים, גם אם זה לא עניין אותך. תודה שראית איתי סדרות, למרות שהם לא עניינו אותך.\r\nתודה שלא משנה מה, תמיד כיבדת אותי. תודה שלא משנה מה, תמיד זרמת איתי עם דברים. תודה שלא משנה מה, היית כנה איתי. תודה שלא משנה מה, תמיד התמודדת עם השטויות שלי. תודה שלא משנה מה, תמיד סלחת והכלת. תמיד הבנת ואהבת. תמיד דאגת ווידאת שהכל טוב.\r\nתודה על כל \"אני אוהבת אותך\". תודה על כך \"איך את\". תודה על כל כך הרבה חוויות. תודה על כל הקדשת שיר.\r\nתודה על שסכמת עליי. תודה על שתמיד האמנת לי. תודה שתמיד היית גאה בי. תודה שתמיד תמכת בי, לטוב ולרע. תודה שלא משנה מה תמיד היית שם בשבילי, לטוב ולרע.\r\nתודה על שסלחת לי. על כל הפעמים שפגעתי בך. והיו הרבה. תודה על שהכלת את ההתנהגות שלי המוקצנת שלי.\r\nתודה על שתמיד התחשבת ברגשות שלי. תודה שכשהייתי צריכה, תמיד שמת את הרגשות והצרכים שלך בצד כדי שאני אהיה בסדר. אני מעריכה את זה.\r\nתודה על כל שנייה שהסתכלת עליי והרגשתי כמו הבן אדם הכי מאושר בעולם. תודה על כל שנייה שגרמת לי להרגיש כל כך נאהבת. תודה על כל שנייה שגרמת לי להרגיש כל כך אהובה. תודה על כל שנייה שגרמת לי להרגיש שלמישהו אכפת ממני. תודה על כל שנייה שגרמת לי להרגיש שמישהו דואג לי. תודה על כל שנייה שגרמת לי להרגיש שאני חשובה למישהו. תודה על כל שנייה שגרמת לי להרגיש מה זה אהבה. תודה על כל שנייה שגרמת לי להרגיש שמישהו אוהב אותי. תודה על כל שנייה שלא ויתרת עליי. תודה על כל שנייה שהיית שם בשבילי. תודה על כל שנייה שגרמת לי להרגיש שאני רוצה לחיות ולהישאר בעולם הזה. תודה שהוצאת אותי מהרגשות הרעים שלי. תודה שהוצאת אותי מהמחשבות הרעות שלי.\r\nובקיצור, פשוט תודה על השנתיים הכי מאושרות בחיי. כל רגע, כל שנייה, הכל היה שווה את זה. בטוב וברע. ביום ובלילה. בריב ובסתם יום רגיל.\r\nהכל היה מושלם.\r\nהכל היה חוויה לא נשכחת.\r\nאלה השנתיים הכי טובות בחיים שלי.\r\nאני כל כך שמחה שחוויתי אותן איתך.\r\nאת כל העולם שלי.\r\nאני מקווה שלקחת את הכל כאן בקטע טוב.\r\nלירלי, תשמחי שזה קרה.\r\nאני שמחה שעברתי את כל זה איתך. לא הייתי מחליפה את החוויה הזאת באחרת.\r\nאני אוהבת אותך, sunshine.\r\n";
            double[] duration =
            {
                0.9, 2.1, 5, 9.2, 12, 13.5, 14.9, 16.5, 18.1, 20, 22.7, 24.9, 26.7, 28.6, 30.2, 34, 38, 
                42, 44, 45.8, 47, 48.5, 50.8, 51.8, 53, 54.7, 56.4, 58.3, 59.8, 64.5, 67.8, 71.5, 75.5, 
                79, 82.8, 86, 89.3, 92.1, 95.5, 98.5, 100.2, 102.5, 104.4, 105.6, 107.8, 110.5, 112, 
                114, 115.8, 118.7, 123, 124.7, 126.9, 128.6, 133.5, 136.5, 141.6, 144, 149.5, 152.8, 
                156.2, 160, 164.5, 167.7, 171.5, 175, 177.8, 180.1, 185.6, 188.8, 191.8, 196.7, 199.8, 
                201.5, 203.4, 205.8, 207.7, 210, 213, 215.8, 218.5, 221.5, 224.5, 227, 230
            };
            movingLyrics = new MovingLyrics(ref player, lyrics, duration);
            movingLyrics.Width = 625;
            movingLyrics.Height = 250;
            grid.Children.Add(movingLyrics);
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
