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
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenPop;
using OpenPop.Pop3;
using OpenPop.Mime;

namespace RNNEmailClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<MailDatabaseHandler> mdhList = new List<MailDatabaseHandler>();
        public MainWindow()
        {
            InitializeComponent();
            
        }
        private void newUser_Click(object sender, RoutedEventArgs e)
        {
            addNewUser newUser = new addNewUser();
            if(newUser.ShowDialog() == true)
            {
                FolderMenu folderMenu = new FolderMenu(User.users[User.users.Count-1],this);
                leftMenu.Children.Add(folderMenu);
                MailDatabaseHandler mdh = new MailDatabaseHandler(User.users[User.users.Count-1], true);
                mdhList.Add(mdh);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            User.GetUsersFromSettings();
            foreach (User.user user in User.users)
            {
                FolderMenu folderMenu = new FolderMenu(user,this);
                leftMenu.Children.Add(folderMenu);
                MailDatabaseHandler mdh = new MailDatabaseHandler(user);
                mdhList.Add(mdh);
            }
        }

        private void newEmail_Click(object sender, RoutedEventArgs e)
        {
            NewEmail newEmail = new NewEmail();
            if (newEmail.ShowDialog() == true)
            {

            }
        }
    }
}
