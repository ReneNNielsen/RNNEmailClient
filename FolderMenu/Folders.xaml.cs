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
using io = System.IO;

namespace RNNEmailClient
{
    /// <summary>
    /// Interaction logic for Folders.xaml
    /// </summary>
    public partial class Folders : UserControl
    {
        private string _folderImg;
        public string folderImg
        {
            get
            {
                return _folderImg;
            }
            set
            {
                _folderImg = value;
                imgName.Source = new BitmapImage(new Uri("ImgIcons/" + value, UriKind.Relative));
            }
        }
        private string _folderName;
        public string folderName
        {
            get
            {
                return _folderName;
            }
            set
            {
                _folderName = value;
                Dispatcher.BeginInvoke((Action)(() =>
                textName.Content = value));
            }
        }
        public List<string> emailAddresses = new List<string>();
        public int messesID;
        public List<PopClient.emailStruct> emails = new List<PopClient.emailStruct>();
        public MailDatabase db = new MailDatabase();
        public User.user user = new User.user();
        public int newEmails = 0;
        public FolderMenu folderMenu;
        public MainWindow mw;
        public Folders(FolderMenu _folderMenu, MainWindow _mw)
        {
            InitializeComponent();
            folderMenu = _folderMenu;
            user = folderMenu.user;
            folderImg = "folder.jpg";
            mw = _mw;
        }

        private void headerTreeItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            mw.emailList.Children.Clear();
            string headerText = folderName;
            if (folderName.Contains('(') && folderName.Contains(')'))
            {
                headerText = folderName.Substring(0, folderName.IndexOf('(')).Trim();
            }
            EmailListShower els = new EmailListShower(emails, headerText, mw);
            mw.emailList.Children.Add(els);
        }

        public virtual void addEmailsToFolder()
        {
            emails = new List<PopClient.emailStruct>();
            foreach (string emailAddress in emailAddresses)
            {
                emails.AddRange(db.getUserEmailListByreciverEmailAndSenderEmail(user.email, emailAddress));
            }
            SetNewEmails();
        }
        public void SetNewEmails()
        {
            newEmails = 0;
            foreach (PopClient.emailStruct email in emails)
            {
                if (!email.seen)
                {
                    newEmails++;
                }
            }
            if (folderName.Contains('(') && folderName.Contains(')'))
            {
                folderName = folderName.Substring(0,folderName.IndexOf('(')).Trim();
            }
            folderName += " (" + newEmails.ToString() + ")";
        }
        private void textName_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ContextMenu rightClickMenu = new ContextMenu();
            MenuItem rename = new MenuItem();
            rename.Header = "elefant";
            rightClickMenu.Items.Add(rename);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
