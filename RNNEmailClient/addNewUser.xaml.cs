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

namespace RNNEmailClient
{
    /// <summary>
    /// Interaction logic for addNewUser.xaml
    /// </summary>
    public partial class addNewUser : Window
    {
        public addNewUser()
        {
            InitializeComponent();
        }

        private void btnCon_Click(object sender, RoutedEventArgs e)
        {
            int port;
            if (!String.IsNullOrEmpty(txtEmail.Text))
            {
                if (!String.IsNullOrEmpty(txtPass.Password))
                {
                    if (!String.IsNullOrEmpty(txtPoP.Text))
                    {
                        if (!String.IsNullOrEmpty(txtPort.Text) && int.TryParse(txtPort.Text, out port))
                        {
                            User.user newUser = new User.user();
                            newUser.email = txtEmail.Text;
                            newUser.password = txtPass.Password;
                            newUser.pop3 = txtPoP.Text;
                            newUser.port = port;
                            newUser.ssl = Convert.ToBoolean(chbSsl.IsChecked.ToString());
                            User.AddUser(newUser);
                            DialogResult = true;
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Port er ikke et tal eller/og er tom");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Pop3 er tom");
                    }
                }
                else
                {
                    MessageBox.Show("Password er tom");
                }
            }
            else
            {
                MessageBox.Show("Email er tom");
            }
        }

        private void btnCan_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
