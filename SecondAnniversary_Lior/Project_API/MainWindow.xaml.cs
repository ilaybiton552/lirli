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

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            GenerateArraysOfTextBox();
            LinkMail();
            FocusManager.SetFocusedElement(this, date[0]); // set focus for first input
            PlaySong();
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

        private bool ExistInFile(string line)
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "check.txt");
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
            // get MyDocuments path
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            using (StreamWriter outputFile = new StreamWriter(System.IO.Path.Combine(path, "check.txt"), true))
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
            ((Border)sender).Background = Brushes.AliceBlue;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = Brushes.White;
        }

        private void SendPass_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string password = GetPassword();
            mailMessage.Body = "The used password is " + password;

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception)
            {
                MessageBox.Show("Please check your internet connection");
                return;
            }

            switch (password)
            {
                case "130906":
                    MessageBox.Show("Haha like the way you think :)\nBut this is about you not me");
                    break;
                case "311006":
                    if (DateTime.Now.Day == 31 && DateTime.Now.Month == 10)
                        MessageBox.Show("Well happy birthday Lior :)");
                    else
                        MessageBox.Show("Hey today is NOT your birthday...\nJust trust me, make sure you turn on this computer on your birthday\nYou won't regret it😉");
                    break;
                case "200621":
                    if (!ExistInFile("Anniversary")) WriteToFile("Anniversary");
                    AnniversaryWindow anniVindow = new AnniversaryWindow();
                    player.Stop();
                    Close();
                    anniVindow.ShowDialog();
                    break;
                case "240923":
                    if (!ExistInFile("Breakup")) WriteToFile("Breakup");
                    BreakupWindow breakupWindow = new BreakupWindow();
                    player.Stop();
                    Close();
                    breakupWindow.ShowDialog();
                    break;
                default:
                    GenerateRandomHint();
                    break;
            }

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

        private void GenerateRandomHint()
        {
            Random rnd = new Random();
            int num = rnd.Next(1, 101);

            if (num < 11) // 10% chance
            {
                MessageBox.Show("Think about the breakup...");
            }
            else if (num > 10 && num < 61) // 50% chance
            {
                MessageBox.Show("Anniversaryyyyyyyyy!!!!");
            }
            else
            {
                MessageBox.Show("Nahhh no hint for now");
            }

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
            Image image = sender as Image;
            if (mute)
            {
                image.Source = new BitmapImage(new Uri(@"images\sound.png", UriKind.Relative));
                player.PlayLooping();
            }
            else
            {
                image.Source = new BitmapImage(new Uri(@"images\mute.png", UriKind.Relative));
                player.Stop();
            }
            mute = !mute;
        }
    }
}
