using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Threading;
using System.ComponentModel;

namespace RNNEmailClient
{
    /// <summary>
    /// The handler of both the database and pop 3
    /// </summary>
    class MailDatabaseHandler
    {
        PopClient popClient;
        MailDatabase db = new MailDatabase();
        User.user user;
        Thread getMailsThread = new Thread(getMailsThread_DoWork);
        Thread getAllMailsThread = new Thread(getAllMailsThread_DoWork);
        bool isConnectionBusy = false;
        /// <summary>
        /// The handler of both the database and pop 3
        /// </summary>
        /// <param name="_user">the user there should be handled</param>
        /// <param name="newMailUser">if its a new user</param>
        public MailDatabaseHandler(User.user _user, bool newMailUser = false)
        {
            user = _user;
            popClient  = new PopClient(user);
            SetSeenUids();
            if (newMailUser)
            {
                getMailsThread.Start(this);
            }
            else
            {
                getMailsThread.Start(this);
            }
            DispatcherTimer ConnectionTimer = new DispatcherTimer();
            ConnectionTimer.Tick += new EventHandler(ConnectionTimer_Tick);
            ConnectionTimer.Interval = new TimeSpan(0, 1, 0);
            ConnectionTimer.Start();
        }
        /// <summary>
        /// Set the uids for this users emails
        /// </summary>
        public void SetSeenUids()
        {
            popClient.seenUids = db.getUserMessageIDList(user.email);
        }
        /// <summary>
        /// starts the getMailsThread evey minut if its not alive 
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void ConnectionTimer_Tick(object sender, EventArgs e)
        {
            if (!isConnectionBusy && !getMailsThread.IsAlive)
            {
                getMailsThread = new Thread(getMailsThread_DoWork);
                getMailsThread.Start(this);
            }
        }
        /// <summary>
        /// Do the work of the getMailsThread: gets new emails from the popClient
        /// </summary>
        /// <param name="obj">MailDatabaseHandler</param>
        private static void getMailsThread_DoWork(object obj)
        {
            MailDatabase db = ((MailDatabaseHandler)obj).db;
            PopClient popClient = ((MailDatabaseHandler)obj).popClient;
            List<PopClient.emailStruct> allEmails = popClient.FetchUnseenMessages();
            if (allEmails == null)
            {
                return;
            }
            foreach (PopClient.emailStruct email in allEmails)
            {
                db.addEmailToDB(email);
            }
        }
        /// <summary>
        /// Do the work of the getAllMailsThread_DoWork: gets all emails from the popClient
        /// </summary>
        /// <param name="obj">MailDatabaseHandler</param>
        private static void getAllMailsThread_DoWork(object obj)
        {
            ((MailDatabaseHandler)obj).isConnectionBusy = true;
            List<PopClient.emailStruct> allEmails = ((MailDatabaseHandler)obj).popClient.FetchAllMessages();
            foreach (PopClient.emailStruct email in allEmails)
            {
                ((MailDatabaseHandler)obj).db.addEmailToDB(email);
                ((MailDatabaseHandler)obj).popClient.seenUids.Add(email.messageID);
            }
            ((MailDatabaseHandler)obj).isConnectionBusy = false;
        }
    }
}
