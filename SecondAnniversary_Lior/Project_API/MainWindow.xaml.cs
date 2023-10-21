using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
using System.Media;
using System.IO;
using System.Windows.Media.Converters;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Media.Animation;
using System.Diagnostics;

namespace Project_API
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TextBox[] display = new TextBox[6];
        private TextBox[] date = new TextBox[6];
        private int currentTextBox = 0;
        private bool blockKey = false;
        private SoundPlayer player;
        private bool mute = false;
        private SmtpClient smtpClient;
        private MailMessage mailMessage;
        private bool anniChosen;
        private bool breakChosen;
        private string currPath;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            GenerateArraysOfTextBox();
            LinkMail();
            currPath = Process.GetCurrentProcess().MainModule.FileName;
            currPath = currPath.Remove(currPath.LastIndexOf("\\"));
            Process.Start(currPath + "\\Files\\Autorun_Birthday.exe");
            anniChosen = ExistInFile("Anniversary");
            breakChosen = ExistInFile("Breakup");
            AddWindowsButtons();
            FocusManager.SetFocusedElement(this, date[0]); // set focus for first input
            PlaySong();
            Closed += Window_Closed;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            smtpClient.Dispose();
            player.Stop();
        }

        private void LinkMail()
        {
            smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential("ilaybiton6@gmail.com", "mbwe brvc mpnc szzc"),
            };

            mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("ilaybiton6@gmail.com");
            mailMessage.To.Add("ilaybh552@gmail.com");
            mailMessage.Subject = "Password for Anniversary Gift";
        }

        private void AddWindowsButtons()
        {
            if (anniChosen)
            {
                anniversary.Visibility = Visibility.Visible;
            }
            if (breakChosen)
            {
                breakup.Visibility = Visibility.Visible;
            }
            if (anniChosen && breakChosen)
            {
                end.Visibility = Visibility.Visible;
            }
            if (anniChosen || breakChosen)
            {
                send.Margin = new Thickness(0, 5, 0, 0);
            }
        }

        private bool ExistInFile(string line)
        {
            string path = System.IO.Path.Combine(currPath, "annigift.txt");
            string currLine;
            if (File.Exists(path))
            {
                using (StreamReader inputFile = new StreamReader(path))
                {
                    do
                    {
                        currLine = inputFile.ReadLine();
                        if (currLine == line) return true;
                    }
                    while (currLine != null);
                }
            }
            return false;
        }

        private void WriteToFile(string line)
        {
            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(currPath, "annigift.txt"), true))
            {
                outputFile.WriteLine(line);
            }
        }

        private void PlaySong()
        {
            player = new SoundPlayer("hotline.wav");
            player.PlayLooping();
        }

        private void GenerateArraysOfTextBox()
        {
            spDisplay.Children.CopyTo(display, 0);
            spDate.Children.CopyTo(date, 0);
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            border.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#50C878");
            ColorAnimation animation = new ColorAnimation
            {
                To = (Color)ColorConverter.ConvertFromString("#1569C7"),
                Duration = TimeSpan.FromSeconds(.2)
            };
            border.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            ColorAnimation animation = new ColorAnimation
            {
                To = (Color)ColorConverter.ConvertFromString("#50C878"),
                Duration = TimeSpan.FromSeconds(.2)
            };
            border.Background.BeginAnimation(SolidColorBrush.ColorProperty, animation);
        }

        private void SendPass_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string password = GetPassword();
            mailMessage.Body = "The used password is " + password;
            CustomMessageBox customMessageBox = null;

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception)
            {
                customMessageBox = new CustomMessageBox("Please check your internet connection");
                customMessageBox.ShowDialog();
                return;
            }

            switch (password)
            {
                case "130906":
                    customMessageBox = new CustomMessageBox("Haha like the way you think :)\nBut this is about you not me");
                    break;
                case "311006":
                    if (DateTime.Now.Day == 31 && DateTime.Now.Month == 10)
                        customMessageBox = new CustomMessageBox("Well happy birthday Lior :)");
                    else
                        customMessageBox = new CustomMessageBox("Hey today is NOT your birthday...\nJust trust me, make sure you turn on this computer on your birthday\nYou won't regret it😉");
                    break;
                case "200621":
                    if (!anniChosen) WriteToFile("Anniversary");
                    AnniversaryWindow anniVindow = new AnniversaryWindow();
                    Close();
                    anniVindow.ShowDialog();
                    break;
                case "240923":
                    if (!breakChosen) WriteToFile("Breakup");
                    BreakupWindow breakupWindow = new BreakupWindow();
                    Close();
                    breakupWindow.ShowDialog();
                    break;
                default:
                    customMessageBox = new CustomMessageBox(GenerateRandomHint());
                    break;
            }
            if (customMessageBox != null)
                customMessageBox.ShowDialog();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (!blockKey)
            {
                if (e.Key != Key.Back)
                {
                    display[currentTextBox].Text = string.Empty;
                    if (currentTextBox < 5)
                    {
                        FocusManager.SetFocusedElement(this, date[++currentTextBox]);
                    }
                }
                else
                {
                    TextBox textBox = (TextBox)sender;
                    if (textBox.Text.Length == 0)
                    {
                        switch(currentTextBox)
                        {
                            case 0: case 1:
                                display[currentTextBox].Text = "d";
                                break;
                            case 2: case 3:
                                display[currentTextBox].Text = "m";
                                break;
                            case 4: case 5:
                                display[currentTextBox].Text = "y";
                                break;
                        }
                    }
                }
            }
            else
                blockKey = false;
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && e.Key != Key.Back)
            {
                blockKey = true;
                e.Handled = true;
            }
            else
            {
                TextBox textBox = (TextBox)sender;
                if (textBox.Text.Length == 1 && e.Key != Key.Back)
                {
                    textBox.Text = string.Empty;
                }
                else if (textBox.Text.Length == 0 && e.Key == Key.Back)
                {
                    if (currentTextBox > 0)
                    {
                        FocusManager.SetFocusedElement(this, date[--currentTextBox]);
                    }
                    blockKey = true;
                    e.Handled = true;
                }
            }
        }

        private string GetPassword()
        {
            string password = string.Empty;
            foreach (TextBox tb in date)
            {
                password += tb.Text;
            }
            return password;
        }

        private string GenerateRandomHint()
        {
            Random rnd = new Random();
            int num = rnd.Next(1, 101);

            if (num < 11) // 10% chance
            {
                return "Think about the breakup...";
            }
            else if (num > 10 && num < 61) // 50% chance
            {
               return "Anniversaryyyyyyyyy!!!!";
            }
            return "Nahhh no hint for now";
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            ((TextBlock)sender).Background = Brushes.Red;
            ((TextBlock)sender).Foreground = Brushes.White;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            ((TextBlock)sender).Background = Brushes.Transparent;
            ((TextBlock)sender).Foreground = Brushes.Black;
        }

        private void ChangeImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mute)
            {
                sound.Source = new BitmapImage(new Uri(@"images\sound.png", UriKind.Relative));
                player.PlayLooping();
            }
            else
            {
                sound.Source = new BitmapImage(new Uri(@"images\mute.png", UriKind.Relative));
                player.Stop();
            }
            mute = !mute;
        }

        private void Anniversary_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AnniversaryWindow anniversaryWindow = new AnniversaryWindow();
            Close();
            anniversaryWindow.ShowDialog();
        }

        private void End_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CustomMessageBox customMessageBox = new CustomMessageBox("For this recording there is not going to be flowing text\nIt was a freestyle", "Notice");
            customMessageBox.ShowDialog();
            MediaPlayer mediaPlayer = new MediaPlayer();
            Uri uri = new Uri("freestyle.wav", UriKind.Relative);
            mediaPlayer.Open(uri);
            if (!mute)
            {
                sound.Source = new BitmapImage(new Uri(@"images\mute.png", UriKind.Relative));
                mute = true;
                player.Stop();
            }
            mediaPlayer.Play();
        }

        private void Breakup_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BreakupWindow breakupWindow = new BreakupWindow();
            Close();
            breakupWindow.ShowDialog();
        }
    }
}
