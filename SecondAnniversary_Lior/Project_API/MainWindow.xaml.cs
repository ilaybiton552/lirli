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

namespace Project_API
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TextBox[] display = new TextBox[6];
        TextBox[] date = new TextBox[6];
        int currentTextBox = 0;
        bool blockKey = false;
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            GenerateArraysOfTextBox();
            FocusManager.SetFocusedElement(this, date[0]); // set focus for first input
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
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential("ilaybiton6@gmail.com", "mbwe brvc mpnc szzc"),
            };

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("ilaybiton6@gmail.com");
            mailMessage.To.Add("ilaybh552@gmail.com");
            mailMessage.Subject = "Password for Anniversary Gift";
            mailMessage.Body = "The used password is ";

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch(Exception)
            {
                MessageBox.Show("Please check your internet connection");
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

    }
}
