using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNNEmailClient
{
    /// <summary>
    /// A Inbox folder
    /// </summary>
    class Inbox : Folders
    {
        /// <summary>
        /// An Inbox folder
        /// </summary>
        /// <param name="_folderMenu">the folder mene</param>
        /// <param name="_mw">the main window</param>
        public Inbox(FolderMenu _folderMenu, MainWindow _mw)
            : base(_folderMenu, _mw)
        {
            folderName = "Indbakke";
        }
        /// <summary>
        /// add all users emails to this folder there not are placed in the emails list and not spam
        /// </summary>
        public override void addEmailsToFolder()
        {
            emails = new List<PopClient.emailStruct>();
            foreach (PopClient.emailStruct email in db.getUserEmailList(user.email))
            {
                bool found = false;
                foreach (string emailAddress in emailAddresses)
                {
                    if (email.senderEmail == emailAddress)
                    {
                        found = true;
                    }
                }
                if (email.messageID == "NULL")
                {
                    found = true;
                }
                if (!found)
                {
                    emails.Add(email);
                }
            }
            SetNewEmails();
        }
    }
}
