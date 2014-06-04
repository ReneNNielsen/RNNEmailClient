using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNNEmailClient
{
    /// <summary>
    /// spam Folder there will contain all spam mails
    /// </summary>
    class SpamFilter : Folders
    {
        /// <summary>
        /// An Inbox folder
        /// </summary>
        /// <param name="_folderMenu">the folder mene</param>
        /// <param name="_mw">the main window</param>
        public SpamFilter(FolderMenu _folderMenu, MainWindow _mw)
            : base(_folderMenu, _mw)
        {
            folderName = "Spam";
        }
        /// <summary>
        /// Add all users spam mails to this folder
        /// </summary>
        public override void addEmailsToFolder()
        {
            emails = new List<PopClient.emailStruct>();
            foreach (PopClient.emailStruct email in db.getUserEmailList(user.email))
            {
                bool found = false;
                    if (email.messageID == "NULL")
                    {
                        found = true;
                    }
                if (found)
                {
                    emails.Add(email);
                }
            }
            SetNewEmails();
        }
    }
}
