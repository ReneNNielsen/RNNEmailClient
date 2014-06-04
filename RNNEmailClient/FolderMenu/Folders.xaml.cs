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
    /// Base class for different folders
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
        public List<int> messesID = new List<int>();
        public List<PopClient.emailStruct> emails = new List<PopClient.emailStruct>();
        public MailDatabase db = new MailDatabase();
        public User.user user = new User.user();
        public int newEmails = 0;
        public FolderMenu folderMenu;
        public MainWindow mw;
        /// <summary>
        /// Base class for different folders
        /// </summary>
        /// <param name="_folderMenu">the folder menu where the folder is placed</param>
        /// <param name="_mw">the main window</param>
        public Folders(FolderMenu _folderMenu, MainWindow _mw)
        {
            InitializeComponent();
            folderMenu = _folderMenu;
            user = folderMenu.user;
            folderImg = "folder.jpg";
            mw = _mw;
        }
        /// <summary>
        /// left click on the folder. adds the email to the emailList in the mainwindow  
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
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
        /// <summary>
        /// Add the users emails to this folder where emailAddress are equel to the emails in the emails list
        /// </summary>
        public virtual void addEmailsToFolder()
        {
            emails = new List<PopClient.emailStruct>();
            foreach (string emailAddress in emailAddresses)
            {
                emails.AddRange(db.getUserEmailListByreciverEmailAndSenderEmail(user.email, emailAddress));
            }
            SetNewEmails();
        }
        /// <summary>
        /// Set the new emails number
        /// </summary>
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
        /// <summary>
        /// NOT MADE! should come a menu up where the user cound:
        /// add email address to this folder,
        /// remove email address from this folder,
        /// rename the folder,
        /// and more
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void textName_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ContextMenu rightClickMenu = new ContextMenu();
            MenuItem rename = new MenuItem();
            rename.Header = "elefant";
            rightClickMenu.Items.Add(rename);
        }

    }
}
