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
        private MediaPlayer player = new MediaPlayer();

        public AnniversaryWindow()
        {
            InitializeComponent();
            LoadSongs();
            CreateLyrics();

            SongInterface songInterface = new SongInterface(songs);
            songInterface.Width = 800;
            songInterface.Height = 75;
            songInterface.VerticalAlignment = VerticalAlignment.Bottom;
            songInterface.Margin = new Thickness(0, 0, 0, 15);

            var uri = new Uri("anniversary.wav", UriKind.Relative);
            player.Open(uri);
            VideoInterface videoInterface = new VideoInterface(ref player);
            videoInterface.Width = 800;
            videoInterface.Height = 75;
            videoInterface.VerticalAlignment = VerticalAlignment.Top;

            grid.Children.Add(songInterface);
            grid.Children.Add(videoInterface);
        }

        private void CreateLyrics()
        {
            string lyrics = "היי לירלי.\r\nאני בטוחה שלא היה כל כך קשה לנחש את זה הא?\r\nאת האמת שאני לא כל כך יודעת מה אני רוצה לעשות כאן.\r\nעכשיו זה מרגיש פשוט מוזר לעשות את זה.\r\nכאילו זה נגמר. אבל אני חייבת לך. אני חייבת למה שהיה לנו.\r\nאני רוצה להגיד תודה.\r\nתודה על כל מה שהיה. תודה על כל שניה של אהבה חסרת גבולות. תודה על כל שניה של אכפתיות. תודה על כל שניה של דאגה. תודה על כל רגע מושלם איתך.\r\nתודה שתמיד היית שם בשבילי. ביום, בלילה, בכל רגע נתון שהייתי צריכה. תודה שהתעוררת בשבילי בלילה למרות שהיו לך דברים לעשות. תודה שנשארת ערה בשבילי כדי לוודא שאני בסדר. תודה שתמיד דאגת לשלומי.\r\nתודה על כל בוקר טוב. תודה על כל לילה טוב. תודה על כל שיחה. תודה על כל חיוך שהעלית לי על הפנים. תודה על כל צחוק. תודה על כל תשוקה. תודה על פרפרים בבטן. תודה על כל טיפה של אהבה.\r\nתודה על כל משחק. תודה על כל שטות. תודה על זה שתמיד זרמת איתי, גם עם דברים שאת לא אוהבת.\r\nתודה על שראית איתי דרגון בול למרות שלא כל כך אהבת. תודה ששיחקת איתי סודוקו למרות שעשיתי כמעט הכל. תודה שראית איתי סרטונים, גם אם זה לא עניין אותך. תודה שראית איתי סרטים, למרות שהם לא עניינו אותך. תודה שראית איתי סדרות, למרות שהם לא עניינו אותך.\r\nתודה שלא משנה מה, תמיד כיבדת אותי. תודה שלא משנה מה, תמיד זרמת איתי עם דברים. תודה שלא משנה מה, היית כנה איתי. תודה שלא משנה מה, תמיד התמודדת עם השטויות שלי. תודה שלא משנה מה, תמיד סלחת והכלת. תמיד הבנת ואהבת. תמיד דאגת ווידאת שהכל טוב.\r\nתודה על כל \"אני אוהבת אותך\". תודה על כך \"איך את\". תודה על כל כך הרבה חוויות. תודה על כל הקדשת שיר.\r\nתודה על שסכמת עליי. תודה על שתמיד האמנת לי. תודה שתמיד היית גאה בי. תודה שתמיד תמכת בי, לטוב ולרע. תודה שלא משנה מה תמיד היית שם בשבילי, לטוב ולרע.\r\nתודה על שסלחת לי. על כל הפעמים שפגעתי בך. והיו הרבה. תודה על שהכלת את ההתנהגות שלי המוקצנת שלי.\r\nתודה על שתמיד התחשבת ברגשות שלי. תודה שכשהייתי צריכה, תמיד שמת את הרגשות והצרכים שלך בצד כדי שאני אהיה בסדר. אני מעריכה את זה.\r\nתודה על כל שנייה שהסתכלת עליי והרגשתי כמו הבן אדם הכי מאושר בעולם. תודה על כל שנייה שגרמת לי להרגיש כל כך נאהבת. תודה על כל שנייה שגרמת לי להרגיש כל כך אהובה. תודה על כל שנייה שגרמת לי להרגיש שלמישהו אכפת ממני. תודה על כל שנייה שגרמת לי להרגיש שמישהו דואג לי. תודה על כל שנייה שגרמת לי להרגיש שאני חשובה למישהו. תודה על כל שנייה שגרמת לי להרגיש מה זה אהבה. תודה על כל שנייה שגרמת לי להרגיש שמישהו אוהב אותי. תודה על כל שנייה שלא ויתרת עליי. תודה על כל שנייה שהיית שם בשבילי. תודה על כל שנייה שגרמת לי להרגיש שאני רוצה לחיות ולהישאר בעולם הזה. תודה שהוצאת אותי מהרגשות הרעים שלי. תודה שהוצאת אותי מהמחשבות הרעות שלי.\r\nובקיצור, פשוט תודה על השנתיים הכי מאושרת בחיי. כל רגע, כל שנייה, הכל היה שווה את זה. בטוב וברע. ביום ובלילה. בריב ובסתם יום רגיל.\r\nהכל היה מושלם.\r\nהכל היה חוויה לא נשכחת.\r\nאלה השנתיים הכי טובות בחיים שלי.\r\nאני כל כך שמחה שחוויתי אותן איתך.\r\nאת כל העולם שלי.\r\nאני מקווה שלקחת את הכל כאן בקטע טוב.\r\nלירלי, תשמחי שזה קרה.\r\nאני שמחה שעברתי את כל זה איתך. לא הייתי מחליפה את החוויה הזאת באחרת.\r\nאני אוהבת אותך, sunshine.\r\n";
            double[] duration = new double[0];
            MovingLyrics movingLyrics = new MovingLyrics(player, lyrics, duration);
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
