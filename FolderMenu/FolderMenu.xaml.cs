using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace RNNEmailClient
{
    /// <summary>
    /// Interaction logic for FolderMenu.xaml
    /// </summary>
    public partial class FolderMenu : UserControl
    {
        public User.user user = new User.user();
        public MainWindow mw;
        Thread addEmailsToFolderThread = new Thread(addEmailsToFolder_DoWork);
        public FolderMenu(User.user _user, MainWindow _mw)
        {
            user = _user;
            mw = _mw;
            InitializeComponent();
        }
        private void EmailAddTimer_Tick(object sender, EventArgs e)
        {
            if (!addEmailsToFolderThread.IsAlive)
            {
                addEmailsToFolderThread = new Thread(addEmailsToFolder_DoWork);
                addEmailsToFolderThread.Start(this);
            }
            
        }
        private static void addEmailsToFolder_DoWork(object obj)
        {
            FolderMenu thisFolderMenu = (FolderMenu)obj;
            foreach (Folders item in thisFolderMenu.headerTreeItem.Items)
            {
                item.addEmailsToFolder();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            emailName.Content = user.email;
            Folders myInbox = new Inbox(this, mw);
            headerTreeItem.Items.Add(myInbox);
            Folders myFolder = new Folder(this, mw);
            headerTreeItem.Items.Add(myFolder);
            Folders mySendt = new Sendt(this, mw);
            headerTreeItem.Items.Add(mySendt);
            Folders mySpam = new SpamFilter(this, mw);
            headerTreeItem.Items.Add(mySpam);
            Folders myTrash = new Trash(this, mw);
            headerTreeItem.Items.Add(myTrash);

            EmailAddTimer_Tick(this, new EventArgs());
            DispatcherTimer EmailAddTimer = new DispatcherTimer();
            EmailAddTimer.Tick += new EventHandler(EmailAddTimer_Tick);
            EmailAddTimer.Interval = new TimeSpan(0, 1, 0);
            EmailAddTimer.Start();
        }
    }
}
