using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNNEmailClient
{
    class SpamFilter : Folders
    {
        public SpamFilter(FolderMenu _folderMenu, MainWindow _mw)
            : base(_folderMenu, _mw)
        {
            folderName = "Spam";
        }
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
