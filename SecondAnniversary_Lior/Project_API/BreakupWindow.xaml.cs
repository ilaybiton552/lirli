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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project_API
{
    /// <summary>
    /// Interaction logic for BreakupWindow.xaml
    /// </summary>
    public partial class BreakupWindow : Window
    {
        private Song[] songs;
        private MediaPlayer player = new MediaPlayer();
        private VideoInterface videoInterface;
        private MovingLyrics movingLyrics;
        private SongInterface songInterface;

        public BreakupWindow()
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

            var uri = new Uri("breakup.wav", UriKind.Relative);
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
            CustomMessageBox customMessageBox = new CustomMessageBox("That's it for this window!\nYou can go back to the main window\n you can also stay here and listen to the songs :)", "Notice");
            customMessageBox.ShowDialog();
            movingLyrics.Height = 300;
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
            Border border = sender as Border;
            ColorAnimation animation = new ColorAnimation
            {
                To = Color.FromRgb(255, 255, 255),
                Duration = TimeSpan.FromSeconds(.2)
            };
            border.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            border.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFF");
            ColorAnimation animation = new ColorAnimation
            {
                To = (Color)ColorConverter.ConvertFromString("#1569C7"),
                Duration = TimeSpan.FromSeconds(.2)
            };
            border.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
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
            string lyrics = "היי לירלי, זאת איליי.\r\nוואו אני לא מאמינה שאשכרה הגיע הרגע, להקליט את הכל. בדיוק שבועיים אחרי (וואי עבר כמעט חודש חחחח). כל כך הרבה שינויים של הרגע האחרון חחחחח. מאיה הייתה אמורה לעבור על כל זה ולהגיב לי אבל לצערי הרב לא יצא לה (היא בסוף עברה אבל כבר הקלטתי). זה היה כל כך חשוב לי כי היא מכירה אותך טוב והיא דומה לך. אבל it is what it is.\r\nזה הולך להיות ארוך. מאודדדד ארוך.\r\nהחלטתי שאני כותבת את הכל מראש כדי לא לפספס כלום וגם כי כל פעם קופץ לי עוד משהו לראש שאני רוצה להגיד, אז אני רוצה שכל הדברים החשובים והמרכזיים יהיו כתובים שאני לא אשכח כלום.\r\nכמו שאת כבר יודעת, ברגע שאת שומעת את זה אני יודעת כי עשיתי ככה שכל סיסמה שאת מכניסה לתוכנה נשלחת לי למייל. סטוקרית מעולה שכמותי.\r\nעוד משהו לפני שמתחילים. המטרה של הקובץ היא לא לפגוע בך. אני באמת ניסיתי למצוא חוות דעת של אנשים על כל הדברים שכתבתי כאן ובאמת שיניתי, מחקתי וניסחתי הרבה דברים כדי למנוע מצב שתיפגעי. לצערי, אני לא יכולה לדעת איך תקחי את הדברים שכתובים כאן. אם את נפגעת כאן ממשהו אני מצטערת, זאת ממש לא הכוונה.\r\n\r\nבכיפור ישבתי וחשבתי על מה אני רוצה לעשות עם האפליקציה, כי הרי אני לא יכולה לעשות משהו נורמלי של שנתיים עכשיו כשאנחנו לא ביחד. אז הרעיון שלי היה לעשות תודה כזה וכן רציתי לעשות משהו שקשור ל24 כי בכל זאת, זה הסוף.\r\nבצאת הצום דיברתי קצת עם אליה ואמרתי לה שאני רוצה לעשות כזה סגירת מעגל ובגלל שעד שתשמעי את זה יעבור זמן – להציע לך להיות חברה שלי שוב. בואי נגיד שאליה הייתה קצת סקפטית. כאילו לפני כיפור שלחתי לה כזה איזה מפגרת אני איך נתתי לעצמי לסמוך על מישהו ועוד על החברה הכי טובה של האקסית שלי, כאילו מה כבר ציפיתי שיקרה. (זה היה בלהט של שברון הרגע אני לא מתכוונת לזה בכלל. לדעתי לסמוך עלייך עם הלב שלי הייתה הבחירה הכי טובה בחיים שלי). \r\n\r\nבאותו לילה חשבתי קצת על זה ואמרתי לעצמי טוב בוא נראה, כל הכיפור חשבתי מה אני רוצה בדיוק להגיד לך והתחלתי להתערער אם אני באמת רוצה לעשות את זה. אבל כאילו הבנתי, אני אוהבת אותך, אין לי מה להפסיד מלשאול אותך.\r\nעכשיו למה בעצם אין לי מה להפסיד?\r\nבמקרה הכי גרוע את אומרת לא, אני גם ככה לוקחת את זה על עצמי שזה כנראה מה שתגידי. (לדעתי) ויתרת עליי (על הזוגיות שלנו) כבר, זה (מרגיש) די פתטי להאמין שתרצי לחזור אליי, (במיוחד) אחרי שאמרת את \"המילה האחרונה\" - שזה נגמר.\r\nובמקרה הטוב את תגידי לי כן ואנחנו נחזור להיות ביחד.\r\nאין לי מה להפסיד. במקרה הראשון אני פשוט מבינה (שכביכול) ויתרת עליי (עלינו) לגמרי, מה שמאפשר לי באמת להתחיל להתגבר. במקרה השני, אנחנו ליטרלי חוזרות להיות ביחד ואשכרה בונות עתיד ביחד כמו שאני רוצה (בתקווה גם את).\r\nעכשיו אני באמת הייתי בהתלבטויות האם אני רוצה להציע לך מחדש. בסופו של דבר, בחרת להיפרד ממני. לבוא ולהציע לך שוב? דורש הרבה אומץ. דורש דריסת אגו מוחלטת. זה לבוא ולהגיד לך בפרצוף החלטת משהו, אבל (לדעתי את טועה) זה לא נכון. זה לבוא ולערער בהחלטה שלך, החלטה שמשפיעה על שתינו. זה להתנגד למה שהחלטת, כי אני אוהבת אותך.\r\n\r\nעבר שבוע. טוב, כמובן כשאת שומעת את זה עבר יותר, אבל נכון לכרגע, שבוע. שבוע בלי לכתוב לך בוקר טוב. שבוע בלי לכתוב לך לילה טוב. שבוע שאני בודקת מה איתך דרך חברות. שבוע פשוט מוזר. שבוע שכל מה שאני חושבת לעצמי זה מתי הזמן הנכון לשלוח לך את זה. מתי זה לא יהיה מוקדם מדי ולא יהיה מאוחר מדי. זה חלון שמרגיש כל כך צפוף. חלון שאני לא יודעת מתי יגיע. חלון שפשוט אסור לי לפספס.\r\n\r\nעבר שבוע וחצי. אני כל כך מתגעגעת אלייך. משהו מצחיק שרציתי לכתוב. כל פעם שאני מתוסכלת או מתבאסת ממשהו אני אומרת \"אוף ליאור\", \"אוי ליאור\", \"אוף לירלי\". זה מצחיק ועצוב בו זמנית אבל אני חושבת עלייך ואז אני אומרת אוף ליאור ואז אני נזכרת בזה ואומרת נו באמת חחחחחחחח. אני באמת ממש מקווה שתאהבי את האפליקציה. אני כל כך רוצה לכתוב לך ולשאול מה איתך ואם את בסדר אבל אני פשוט לא יכולה.\r\n\r\nדיברתי עם עלמה והבנתי ממנה שהיית בטלפון בכיפור כדי לברוח בגלל הפרידה. וכאילו זה הרגע שדברים התחילו לנחות כמו סלע. זה כואב לך. את לא יודעת איך להתמודד עם זה. וזה כל כך הרג אותי לשמוע את זה.\r\nזה כל כך נכנס בי שחלמתי על זה בלילה, ואמרת לי משהו בחלום, המפתח להכל, המפתח למה שפתר את הכל בחלום אבל אני פשוט לא זוכרת מה זה היה וזה פשוט הורג אותי.\r\nכל כך כאב לי לשמוע את זה מעלמה בגלל שאני לא מרגישה שינוי כזה גדול. היית במחנה לשלושה שבועות ולא דיברנו כמעט כל התקופה הזאת, דרך התקשורת היחידה שלי איתך היה לשלוח לך מיילים – שזה בעצם מקביל לקובץ הזה שאני עושה עכשיו ולכל הקטע של המתנה לשנתיים (שיחה חד צדדית). ואז כשחזרת פשוט קרה כל כך הרבה שהתקופה עכשיו של אחרי הפרידה לא מרגישה שונה ממה שהיה לפני.\r\nבנוסף לכך, אני ידעתי שאת הולכת להיפרד ממני. שום דבר לא הפתיע אותי. זה היה לי ברור שזה הולך לקרות מתישהו בזמן הקרוב. אבל לא ידעתי מה אני צריכה לעשות כדי למנוע את זה כי אף פעם לא הסברת לי מה קורה. זה הרגיש כאילו כל פעם זרקת שם משהו ושם משהו. הכל הרגיש כל כך מפוזר והיה קשה להבין מה הייתה הבעיה האמיתית. לא הייתה בינינו תקשורת כי פשוט חסמת את האפשרות שתהיה.\r\nאבל אני חושבת שהסיבה האמיתית שאני מרגישה ככה, היא שאני עדיין לא ויתרתי על מה שהיה לנו. את בעצמך יודעת, כל מה שניסיתי לעשות זה שנישאר ביחד. העובדה שאת בכלל שומעת את זה מוכיחה לך. זה כל כך פתטי אני כותבת קובץ שלם כדי לגרום לך לחזור אליי, רק כדי להתחמק מהעובדה שנפרדנו כי אני פשוט לא רוצה את זה. ואת התחלת להתאבל ואני לא מוכנה כי אני עדיין לא רוצה שזה יגמר.\r\nאבל אז דיברתי עם ניקול והסברתי לה קצת על מה שקרה והיא אמרה לי שכנראה שלא לקחת את הזוגיות כל כך ברצינות כמו שאני לקחתי. ואני כל כך לא רוצה להאמין לזה, למרות שזה מרגיש נכון.\r\n\r\nאני באמת כבר לא יודעת מה לעשות. כאילו מצד אחד אני רוצה לעשות את כל הקטע של האפליקציה בצורה הכי מושלמת אבל מצד שני ככל שעובר הזמן אני מאבדת אותך יותר ואני לא רוצה לאבד אותך.\r\nהזמן מתקתק מהר מדי ואני מרגישה שאני מתחילה לאבד זמן ולאבד אותך ואני לא רוצה את זה.\r\nאני לא יודעת מה לעשות אני פשוט לא יודעת מה לעשות. אני לא רוצה לאבד אותך, אני רוצה להיות שלך, אבל זה כנראה פשוט הסוף. לנצח. אני פשוט בהתלבטות אם לשלוח את זה כמה שיותר מוקדם בלי האפליקציה או לחכות עד לאפליקציה אבל אני לא רוצה לחכות עד שיהיה מאוחר מדי.\r\nלמרות שאם אני חושבת על זה, אם לא לקחת את הזוגיות ברצינות כמוני, אין באמת סיבה שתרצי לחזור להיות ביחד. ולמרות כל זאת, אני יושבת וכותבת, עם כנראה תקוות שווא, שהתפילה שלי בכיפור תתגשם ואנחנו נחזור להיות ביחד.\r\nעכשיו, מה אני מתכוונת בזה שלא לקחת את הזוגיות ברצינות כמוני? אני רוצה עתיד. אני רוצה להתחתן איתך. אני רוצה לגור איתך. אני לא אומרת שלא רצית את זה אבל קשה להאמין שכן אם בסופו של דבר בחרת להיפרד ממני, שזה אומר לוותר על זה.\r\n\r\nאני הולכת לעשות מן ניתוח אישי כזה של איך שאני רואה את הדברים. איך שאני מבינה את הטעויות שלי, תוך כדי שאני מסבירה למה זה קרה. ההסברה לא אמורה להביא הצדקה. אני רוצה לפתוח בפנייך את כל הקלפים על השולחן תוך כדי הבעת ביקורת עצמית כי כדי באמת להתפתח ולהשתנות, צריך לנתח את הטעויות מקרוב וללמוד מהן. לא עשיתי את זה. ולדעתי בסופו של דבר, זה מה שהוביל לפרידה.\r\nאני חושבת שכבר כשחזרת מהמחנה קיץ עברתי חתיכת כאפה לפרצוף. כל מה שרציתי זה לדבר איתך וזה הכניס אותך פשוט להלם שחסמת אותי מכל דרך אפשרית. הבעתי את הרגשות שלי כלפייך. וטוב, זה נראה כאילו זה התחיל להכניס לך דברים לראש.\r\nדבר ראשון, חזרת ומן הסתם שרציתי לדבר איתך. היית צריכה את הזמן שלך. כאן טעיתי ואת צודקת, זה לגיטימי לגמרי, כי העולם שלך זה לא רק אני (ולהסתגל לחיים הרגילים אחרי תקופה זה קשה). אבל את יודעת כבר מה שבר אותי. העובדה שדיברת איתי והיית כולך בדיכאון ואז שיחה של שתי דקות עם מישהי מהמחנה הפכה אותך לשמחה. פשוט קנאה טיפוסית. אני גם לא יכולה להאשים אותך, בילית עם האנשים האלה שלושה שבועות, ברור שתשמחי לדבר איתם.\r\nדבר שני, היה את הקטע של השעות שינה שלך שרציתי שתתחילי להפוך. אני לא יודעת למה הייתי כל כך תוקפנית לגבי זה. לא היה מכבד, אני יודעת. הייתי עייפה והתחלתי לריב איתך על זה כמו מפגרת. נכון לא בסדר. לא היה לך כוח לזה ואני מכבדת את זה. אמרת שכבר תחזרי אליי ונשארתי ערה חצי שעה בשביל שתגידי לי שבכלל לא תכננת לחזור אליי. קיבלתי, למרות שזה הרג אותי מבפנים. באותו ריב, כתבתי לך שאת גורמת לי להרגיש כמו ששירן גרמה לי להרגיש. זה באמת שבר אותי לכתוב את זה. וככה באמת הרגשתי באותה השיחה. בחרת להתגונן על עצמך באותה סיטואציה, שזה לגיטימי לגמרי, אני לא יודעת למה אכלתי לך את הראש אחרי זה. חשבת שאני נפרדת ממך. *** מתחיל להיכנס לך הרעיון של להיפרד לראש ***. אמרתי לך שזה מרגיש שהלכת בן אדם אחד וחזרת בן אדם שונה. *** מתחיל להיכנס הרעיון של אני משתנה לראש ***.\r\nדבר שלישי, שאלת אותי מה דעתי על פרידות. אמרתי לך את דעתי הכנה. שזה עלוב לדעתי. והסברתי לך למה. לא הייתי אומרת שהעובדה שנפרדת ממני עלובה. אבל לדעתי האישית, פרידה זה פתרון פשוט כשלא רוצים לפתור בעיה בזוגיות. אל תביני אותי לא נכון, אני לא אומרת שפרידה זה קל. אבל לדעתי זה כן יותר פשוט מלהתמודד עם הבעיות פנים מול פנים.\r\nדבר רביעי, דיברנו על זה שההתנהגות שלך פוגעת בי ושאת לא יודעת למה את ככה. אמרת שאת מנסה לחסום אותי ושאת אדישה ואת לא מבינה למה. אמרת שהרגשת כל כך רע שאני התגעגעתי אלייך הרבה וכל כך רציתי לדבר איתך ואת לא. לא כי לא רצית אלא כי פשוט לא היה לך זמן לזה כמוני. אני לא זוכרת איך התנהגתי אחרי ועד שהגעת לאילת, אני מצטערת. אני יודעת שהייתי מאוד עסוקה עם הפסיכומטרי שלי וחוץ מזה אני לא זוכרת הרבה. אני מתנצלת, אבל אני לא מעוניינת להיכנס לדיסקורד או לווצאפ כדי להיזכר. אני לא זוכרת את ההתנהגות שלי כלפייך באותה תקופה, אבל אני יודעת שמאז שחזרת מהמחנה עשיתי הרבה דברים שפגעו בך וכנראה שגם בתקופה הזאת. אז אני מתנצלת.\r\nדבר חמישי, הגעת לאילת. רצית לעשות לי הפתעה אבל לא כל כך הלך לך. חייבת לציין שהיה ממש נחמד שלמרות שדברים תססו עדיין חשבת על להפתיע אותי ולעשות לי טוב. די לקרוא את המשפט הזה כל פעם מחדש גורם לי לחייך. ביום שהגעת לא הרגשתי טוב והייתי אדישה כלפייך. למה? פשוט רציתי שתתני לי תשומת לב אחרי שהיית אדישה כלפיי מאז שחזרת מהמחנה. לא בוגר בכלל, את האמת שממש מאכזב אפילו. וואלה הגעת לאילת, רצית לעשות לי הפתעה, היה כל כך אכפת לך אחרי שתקפתי אותך ואז התנהגתי כמו סמרטוט מטבח. יום אחרי הגעת אליי אחרי שהייתי ממש ביקורתית כלפייך שאת ערה הרבה זמן אבל לא נפגשת איתי. שוב, פשוט רציתי תשומת לב וזמן איתך אחרי שלא דיברנו הרבה ואחרי שהיית אדישה כלפיי. למרות זאת, ההתנהגות שלי לא הייתה מוצדקת. כאילו ליטרלי במקום לנצל את הזמן הזה בוואלה יש לי זמן ללמוד לפסיכומטרי התעסקתי כמו מפגרת בלבקר אותך כי וואלה גם לך יש חיים. הגעת אליי והתחלנו לדבר. דיברת על זה שאת שוקלת להיפרד ונשברתי בפנייך. היה לי ברור שמהרגע ששאלת אם אני נפרדת ממך עכשיו הנושא יעלה לך לראש ובטח כששאלת אותי בעצמך. אבל לא רציתי להאמין לזה. כי לא האמנתי שבאמת תיפרדי ממני. הייתי ממש קשה איתך עם הקטע שלא כיבדת את לוח הזמנים שלי וביקרתי אותך (וואו כמה מפתיע) שאם היית מתעניינת קצת היית יודעת יותר ושזאת לא הייתה תקופה טובה לנסוע בה. הייתי ממש חרדה לגבי כל הקטע של הפסיכומטרי, נתתי לתסכול ולחרדה הזאת לצאת עלייך וזה לא מוצדק בשום סיבה. העברתי עלייך ביקורת שאת לא יודעת מה קורה איתי (דבר שהמשיך לקרות במהלך התקופה עד הפרידה) אבל זה נבע מהאדישות ומהרצון להיות עסוקה בעצמך. לא הבנתי את זה. פגעתי בך. לא הבנתי את המשמעות של זמן איכות עם עצמך. אני מתנצלת.\r\nדבר שישי, התקופה שבין הפגישה לאילת לנסיעה שלי למגשימים+. דברים המשיכו כרגיל לאותה התקופה. אני לא הייתי בסדר והייתי תוקפנית, התחלתי עם תגובות ציניות ולא נחמדות שבאמת פגעו בך. אני חושבת שפשוט השיחה שלנו כשהיית באילת גרמה לי לחשוב שדברים יחזרו להיות כמו שהם. אז היה לי מאוד קשה להכיל את העובדה שזה לא באמת קרה. חשבתי שדברים ישתפרו, יהפכו ליותר טובים, ליותר בסדר. סוף סוף לא יהיה את הפחד שתיפרדי ממני. אבל טעיתי ובגדול והמשכתי לרמוס אותך כמו מטומטמת.\r\nדבר שביעי, הנסיעה שלי למגשימים+. רציתי שננצל את הזמן בצורה הכי טובה כשאני נוסעת אבל לא התחשבתי בדברים שיש לך. די אירוני בהתחשב בעובדה שכשאת הגעת כל מה שרציתי זה שהיית מתחשבת בדברים שלי ולא היית מגיעה. רק מציינת שהיה לי ממש כיף ואני ממש שמחה שהגעת. בקיצור, לא כיבדתי את לוח הזמנים שלך. אבל באמת היה ממש כיף בכל שנייה איתך. זה באמת תקופה שהרגשתישאנחנו רחוקות מלהיפרד אחת מהשנייה. ואז שאלתי אותך בערב מה את חושבת על זה. רציתי לדבר איתך על זה פנים מול פנים (אנחנו גם לא כל כך דיברנו, לא צריכה להסביר את זה את כבר יודעת). ואז אמרת שאת פשוט לא מרגישה תשוקה יותר. כשאנחנו פיזית ביחד כן אבל לא כשאנחנו בנפרד מרחוק. זה הרג אותי לשמוע אותך אומרת את זה. לדעתי זה לגיטימי לגמרי, אבל אני חושבת שזה היה משהו שתקוע לך בגרון שבאמת גרם לאדישות. אני לא יודעת מה יכולתי לעשות כדי לגרום לך להרגיש תשוקה. אבל זה בוודאות לא מה שעשיתי שזה להיות תוקפנית וחסרת התחשבות. אני מתנצלת. אני זוכרת שאחרי זה דיברתי קצת על הקטע של התשוקה ואמרו לי שיש מצב שזה בגלל המחזור הלא סדיר שלך, שזה את האמת ממש הגיוני, כי מחזור לא סדיר גורם לחוסר יציבות בהורמונים.\r\nדבר שמיני, אחרי הפגישה ועד לפרידה. כמו בפעם הקודמת, בפגישה היה טוב אז כשחזרתי שוב ציפיתי שדברים ישתנו. אני ממש טובה בללמוד מהעבר חחחח. אז כעסתי כשלא. הייתי מתוסכלת כי לא הבנתי למה מה שציפיתי לא קורה. לא הבנתי למה ההתנהגות הזאת ממשיכה. אבל זה פשוט ברור מאוד למה. התחלת לעבור שינוי, בין אם זה בגלל שהכנסתי לך את הרעיון הזה לראש לבין אם פשוט הבחנת בזה בעצמך, לבין אם שניהם. זה קשה גם לעבור שינוי וגם לנסות להחזיק הכל כמו שהיה. אני לא הבנתי את זה. אני הייתי תקועה במיינדסט של בגלל שהפגישה הייתה טובה, כך גם יהיה בשאר הזמן. וכל התסכול הזה נפל עלייך בתגובות ציניות, חריפות וחסרות תקנה. אני מתנצלת על כל הפעמים שפגעתי בך ככה. אני לא הבנתי ולא לקחתי בחשבון את מה שאמרת לי. את כל הקטע של החוסר תשוקה, האדישות והשינוי. לא חיברתי ביניהם. ולמרות שכבר התחלתי להתרגל למצב הזה, זה עדיין פגע בי וזה למה התגובות הציניות המשיכו.\r\nדבר תשיעי, הפרידה. כמו שכבר אמרתי, אני ידעתי שזה הנושא שתדברי איתי עליו. פשוט לא ידעתי מה תגידי כי כמו שכבר אמרתי, לא חיברתי בין כל הדברים. כמובן, שההתנהגות שלי הייתה מה שגרם לך בסופו של דבר להגיע להחלטה הזאת. זה פגע בך. אני נפגעתי. נמאס לך להרגיש חייבת לדבר איתי. פשוט רציתי שהסיוט הזה יגמר. לא רציתי לחשוב שזה אמיתי. לא האמנתי שזאת המציאות. שאת רוצה להיפרד. שויתרת על כל מה שבנינו במשך יותר משנתיים. אבל ככה גרמתי לך להרגיש. בחרת שלא להאמין לי שאני יכולה להכיל לא לדבר אחת עם השנייה. זה נכון, אני באמת יכולה להכיל את זה. פשוט עד אותו רגע לא הייתה לי את ההבנה. עד אותו הרגע לא היה לי את חיבור הנקודות. אני התחלתי להתרגל לסיטואציה, זה לא היה קשה בשבילי באמת להמשיך ככה. הסיבה שהתנהגתי איך שהתנהגתי זה כי לא הבנתי למה זה צריך להיות ככה. לא הייתה בינינו תקשורת. ואז כשהסברת את הכל הבנתי. וברגע שהבנתי זה היה פשוט. אבל בחרת שלא להאמין לי. אני לא יכולה להאשים אותך, הייתי מאוד חצופה ותוקפנית ולא ידעת למה. אני ידעתי למה, אבל את לא. את ידעת למה את מתנהגת ככה אבל אני לא. וכל זה נבע מחוסר תקשורת. שחוסר התקשורת נבע מכל העניין של השינוי. זה כאילו אני מנסה לחשוב על איך היה אפשר לפתור את זה אבל זה מרגיש בלתי אפשרי. אולי אם היית מדברת איתי שוב פעם על זה שאת שוקלת להיפרד והיית מסבירה בדיוק כמו שהסברת רק בלי ההחלטה הסופית שאת הולכת להיפרד ממני. זה בדיוק למה רציתי להילחם שתתני לי עוד הזדמנות. כי ברגע שהגיעה ההבנה ידעתי שמשם אין יותר בעיה.\r\nאבל זה לא קרה. \r\nובסופו של יום, ויתרת עליי ועל מה שהיה לנו.\r\nאני לא אומרת שזאת אשמתך. להפך, אני לוקחת את זה על עצמי. יכולתי לנסות להבין בעצמי את הסיבה להכל אבל זה לא קרה. רק אחרי שדיברת ואמרת את המילה האחרונה הבנתי. רק כשזה היה מאוחר מדי בכדי להבין ולנסות לשנות דברים. וניסיתי. והייתה בי תקווה שתשני את דעתך. אבל זה לא קרה.\r\n\r\nאני חושבת שמה שהופך את זה לכל כך קשה בשבילי זה העובדה שבתחילת המערכת יחסים שלנו, למרות שבקושי הכרתי אותך ולא ידעתי בכלל מה אני עושה. עדיין בכלל לא עיכלתי את מה שקרה. בחרתי בך, מנגד למה שאמא שלי ביקשה ממני. שיקרתי כל התקופה שהיינו ביחד רק כדי להישאר איתך.\r\n\r\nאני אוהבת אותך לירלי. אני רוצה להיות איתך, אני רוצה עתיד איתך, אני רוצה להתחתן איתך. אני רוצה כל בוקר לקום במיטה לידך, אני רוצה להזדקן איתך ורק איתך.\r\nכל הפעמים שאמרתי שאני רוצה עתיד ואני רוצה להתחתן איתך הייתי רצינית, כי אני באמת רוצה. כל שניה בזוגיות איתך הייתה מלאת שמחה ואושר ואני לא רוצה לוותר על זה בקלות.\r\nתראי, אני יודעת שאני מנחיתה עלייך הרבה כאן. אבל אני חושבת שלא היית רוצה לשבת בשיחה ולדבר על זה. אני יודעת שהקשבת לכל מילה ומילה ויחסת לה משמעות רבה. אני יודעת שכל מה שאמרתי כאן מסתובב לך בתוך המוח. אני הבנתי את הטעויות שלי. אני הבנתי את הסיבה אליהם. ואני יודעת שקשה להאמין, אבל אני באמת מאמינה שעכשיו, בגלל שהבנתי את הסיבה, דברים יהיו שונים. פעם קודמת בחרת שלא להאמין לי ואני לא יכולה להאשים אותך, ההתנהגות שלי הייתה שונה לגמרי ממה שאמרתי שאני אעשה. אבל אני מאמינה, באמת מאמינה, שעכשיו אחרי שעשיתי ניתוח משמעותי. אחרי שבאמת הבנתי את הסיבה לכל דבר. הדברים לא יהיו ככה. אני רוצה שתתני לי עוד הזדמנות ותתני לזה ניסיון. גם אם זה אומר שנדבר פעם בשבועיים. גם אם זה אומר נטו לכתוב הודעות בוקר טוב ולילה טוב. גם אם זה אומר רק לשאול איך את פעם ביומיים.\r\nאני רוצה שנחזור להיות ביחד, מהסיבה הפשוטה שאני יודעת שככל שהשעון מתקתק כך אני הולכת ומאבדת אותך. אני יודעת שככל שהזמן עובר, כך הסיכוי שבאמת יהיה לנו עתיד הולך ודועך. ואני לא רוצה את זה. כי אני רוצה רק אותך. אני אוהבת רק אותך.\r\nבכיפור באמת הבנתי שגם אני צריכה זמן לעצמי. ובאמת עם הזמן הזה, הצלחתי להבין את הטעויות שלי. כי עכשיו במקום לרדוף אחרייך בלבקר אותך ואת ההתנהגות שלך, רדפתי אחרי עצמי בלבקר אותי ואת ההתנהגות שלי.\r\nאני רוצה לכתוב לך בוקר טוב כל בוקר אני רוצה לכתוב לך לילה טוב כל לילה, גם אם לא נדבר באותו יום בשיחה. אני רוצה להיות עם היכולת לתקשר איתך, לשאול מה איתך ולאהוב אותך בלי שיגידו לי \"את צריכה לשחרר\".\r\nאני לא רוצה לשחרר, כי מבחינתי זה עדיין לא אבוד. אני לא רוצה לשחרר, כי אני באמת מאמינה במה שיש לנו. אני באמת הצלחתי להפנים דברים. להסתכל על הטעויות שלי וללמוד מהם.\r\nבסופו של דבר אני לא יכולה להכריח אותך לעשות כלום. אבל אני באמת רוצה שתשקלי לחזור ביחד.\r\nאכפת לי ממה שהיה לנו ואני באמת לא רוצה להאמין שזה נגמר. את יכולה להגיד איליי את סתם מכחישה. אולי זה נכון. זאת כן בסופו של דבר דרך של הכחשה. אבל אני באמת מאמינה בזה שאני אומרת שעדיין יש לנו סיכוי. אני באה ועושה את כל זה, שופכת את כל כולי רק בשביל שתחזרי להיות איתי ביחד. אני באמת מבקשת ממך, אם את עדיין אוהבת אותי, תני לי עוד הזדמנות ואני מבטיחה לך שנלך הכי לאט שאפשר, כמה שתצטרכי.\r\nליאור אכפת לי ממך ואני אוהבת אותך מאוד מאוד מאוד ואני רוצה להיות חלק מהחיים שלך. אני רוצה לראות אותך מתפתחת והופכת לאדם האמיתי שאת. ואני רוצה לעשות את זה כשאני בת הזוג שלך. אני רוצה לתמוך בך ולהיות שם בשבילך. אני רוצה לחבק אותך עם כל האהבה שבעולם. אני רוצה לנשק אותך עם כל כך הרבה תשוקה. אני רוצה להחזיק לך את היד ולגרום לך להרגיש כמו שגרמת לי להרגיש באותו יום ברכב. אני פשוט רוצה להיות שלך.\r\nתראי אני לא מבקשת תשובה מיידית. אני גם לא מצפה לתשובה מיידית. כי אני באמת רוצה שתחשבי על זה. אני לא רוצה שתתחרטי. גם אם זה אומר שתחשבי ותחליטי שלא. גם אם זה אומר שתחשבי ותחליטי שכן. אני פשוט רוצה שבאמת תשקלי את כל העניין מחדש.\r\nאז לירלי, את רוצה להיות ביחד?\r\n";
            double[] duration =
            {
                1, 2.5, 6.2, 7.5, 10.5, 14.4, 17.4, 19.21, 20.3, 22.2, 34.1, 41, 43.1, 44.4, 49.1, 57.1,
                60.5, 66.1, 75.1, 81.6, 90.3, 93, 102.3, 107, 112.4, 122.1, 126.15, 129.2, 134.3, 140,
                144.05, 145.2, 150.4, 156.2, 160, 162.4, 164.2, 165.35, 167.4, 171.1, 174.45, 179.05,
                180.2, 184.6, 186.2, 188.2, 190.2, 192.3, 197.3, 200, 201.5, 203.2, 206.3, 208.25, 210.2,
                212.1, 217.45, 225.3, 227.4, 233.3, 239, 242.25, 243.3, 245, 247, 257.35, 262.25, 274.14,
                280.2, 283.35, 285.1, 288.25, 292.4, 296.25, 300.33, 304.3, 310.45, 314.3, 317.15, 325.15,
                329.25, 337.1, 341.15, 343, 353, 358.05, 360.15, 364.06, 365.17, 372.45, 378.5, 386.4,
                391.08, 392.24, 393.5, 395.3, 403.05, 406.43, 410.25, 412.18, 421.35, 422.35, 427.3,
                430.38, 437.1, 438.4, 442.4, 446.35, 448.05, 452.4, 454.5, 461.25, 463.1, 469.45, 474.15,
                476.45, 478.4, 482, 483.3, 485.38, 491.3, 494.35, 499.15, 501.1, 503.45, 511, 513.35, 516.26,
                520.25, 523.42, 527.1, 528.35, 530.4, 532.05, 534.45, 539.3, 542.4, 547.05, 552, 555.25,
                561, 564.2, 567.8, 572.6, 577.5, 584.8, 587.5, 590, 592.9, 600.2, 604.4, 607.7, 609.1,
                613.5, 616.4, 623.1, 628.2, 634.5, 637.9, 645.8, 647.7, 651.2, 658.8, 660.6, 662.9, 677.4,
                684.33, 694.8, 695.9, 696.8, 699.33, 701.2, 705.7, 708, 713.5, 719.5, 723, 726.1, 728,
                732.7, 735.95, 741.1, 747.4, 751.4, 754.6, 758.85, 762.1, 764.6, 771.7, 774, 777.7,
                779.7, 787, 790, 795.5, 797.4, 810.1, 813, 818.1, 821.3, 823, 826, 827.8, 830, 836.8,
                840.5, 842, 847, 851.5, 854.3, 857.4, 860.66, 862, 868.6, 871.3, 874.5, 879.1, 883.4,
                884.4, 885.75, 887.8, 889.9, 891.7, 893.4, 894.8, 897.8, 899.8, 904.1, 906.5, 909.05,
                911, 915.7, 920, 921.7, 924.15, 926.7, 928.5, 932.6, 934.6, 937.2, 939.2, 942, 946.5,
                955.6, 958.3, 961.4, 963.3, 966.55, 967.9, 970.1, 973.5, 976.4, 980.5, 981.6, 984.2,
                986.4, 995.33, 997.6, 1000.8, 1004.6, 1006.1, 1010, 1014.5, 1020.3, 1025.4, 1028.5,
                1032.8, 1036.5, 1039.5, 1041.5, 1043.5, 1050.1, 1057.6, 1062.5, 1065.1, 1067.2, 1070.7,
                1073, 1076.4, 1080, 1087.5, 1092.1, 1093.1, 1094.5, 1096.33, 1099.9, 1103.6, 1115.1,
                1122.1, 1128.4, 1132.05, 1136.33, 1138.2, 1141.6, 1145.25, 1149, 1152.8, 1155.1,
                1156.3, 1159.2, 1163.7, 1168.8, 1178.1, 1183.6, 1187, 1190.1, 1192.4, 1195.2, 1198,
                1204, 1206.9, 1209.6, 1211.6, 1214.7, 1216.4, 1219, 1221.9, 1226.5
            };
            movingLyrics = new MovingLyrics(ref player, lyrics, duration, true);
            movingLyrics.Width = 625;
            movingLyrics.Height = 350;
            grid.Children.Add(movingLyrics);
        }

        private void LoadSongs()
        {
            songs = new Song[]
            {
                new Song("Love Is Gone - Acoustic", "SLANDER, Dylan Matthew", "Love-Is-Gone.mp3"),
                new Song("Lonely City", "Mokita", "Lonely-City.mp3"),
                new Song("Only Love", "Mother Mother", "Only-Love.mp3"),
                new Song("All of Me", "John Legend", "All-of-Me.mp3"),
                new Song("Angels", "The xx", "Angels.mp3"),
                new Song("Black Friday", "Tom Odell", "Black-Friday.mp3"),
                new Song("Car's Outside", "James Arthur", "Car's-Outside.mp3"),
                new Song("chance with you", "mehro", "chance-with-you.mp3"),
                new Song("How Do I Say Goodbye", "Dean Lewis", "How-Do-I-Say-Goodbye.mp3"),
                new Song("It's You", "Ali Gatie", "It's-You.mp3"),
                new Song("Leaving My Love Behind", "Lewis Capaldi", "LeavingMyLoveBehind.mp3"),
                new Song("Take Me Higher", "yaeow, Rnla", "Take-Me-Higher.mp3"),
                new Song("Where's My Love", "SYML", "Where's-My-Love.mp3")
            };
        }

    }
}
