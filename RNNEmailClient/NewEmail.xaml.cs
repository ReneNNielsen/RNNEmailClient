using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

namespace RNNEmailClient
{
    /// <summary>
    /// Interaction logic for NewEmail.xaml
    /// </summary>
    public partial class NewEmail : Window
    {
        public NewEmail()
        {
            InitializeComponent();
        }

        /// <summary>
        /// check if the email To textbox is not empty if not sent the the email
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void send_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtTo.Text))
            {
                
                foreach (User.user user in User.users)
                {
                    string selectEmail = cbUsers.SelectedItem.ToString().Substring(cbUsers.SelectedItem.ToString().LastIndexOf(' ')).Trim();
                    if (user.email == selectEmail)
                    {
                        try
                        {
                            sendMail(txtTo.Text, txtSubject.Text, txtBody.Text, user);
                            DialogResult = true;
                            Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Til mangler");
            }
        }
        /// <summary>
        /// Send the email
        /// </summary>
        /// <param name="to">who is the email to?</param>
        /// <param name="subject">the subject of the email</param>
        /// <param name="body">the body of the email</param>
        /// <param name="user">the user there should send the email</param>
        public void sendMail(string to, string subject, string body,User.user user)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(user.email, user.email);

            mail.To.Add(to);

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient(user.pop3);
            smtp.Send(mail);
        }
        /// <summary>
        /// adds the user to the dropdown box then the window has been loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bool selectedOne = false;
            foreach (User.user user in User.users)
            {
                ComboBoxItem newitem = new ComboBoxItem();
                newitem.Content = user.email;
                if (!selectedOne)
                {
                    newitem.IsSelected = true;
                    selectedOne = true;
                }
                
                cbUsers.Items.Add(newitem);
                
            }
        }
    }
}
