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
    class MailDatabaseHandler
    {
        PopClient popClient;
        MailDatabase db = new MailDatabase();
        User.user user;
        Thread getMailsThread = new Thread(getMailsThread_DoWork);
        Thread getAllMailsThread = new Thread(getAllMailsThread_DoWork);
        bool isConnectionBusy = false;
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
            ConnectionTimer.Interval = new TimeSpan(0, 0, 1);
            ConnectionTimer.Start();
        }
        public void SetSeenUids()
        {
            popClient.seenUids = db.getUserMessageIDList(user.email);
        }
        private void ConnectionTimer_Tick(object sender, EventArgs e)
        {
            if (!isConnectionBusy && !getMailsThread.IsAlive)
            {
                getMailsThread = new Thread(getMailsThread_DoWork);
                getMailsThread.Start(this);
            }
        }
        private static void getMailsThread_DoWork(object obj)
        {
            MailDatabase db = ((MailDatabaseHandler)obj).db;
            PopClient popClient = ((MailDatabaseHandler)obj).popClient;
            List<PopClient.emailStruct> allEmails = popClient.FetchUnseenMessages();
            foreach (PopClient.emailStruct email in allEmails)
            {
                db.addEmailToDB(email);
            }
        }
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
