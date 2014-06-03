using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenPop.Pop3;
using OpenPop.Mime;

namespace RNNEmailClient
{
    public class PopClient
    {
        public struct emailStruct
        {
            public string messageID;
            public string senderName;
            public string senderEmail;
            public string body;
            public string timeStamp;
            public string cc;
            public string subject;
            public string mailTo;
            public string attachment;
            public string receiver;
            public bool seen;
        }
        public List<string> seenUids;
        private User.user user;
        private Pop3Client client;
        public PopClient(User.user _user)
        {
            user = _user;
        }
        public bool SetConnection()
        {
            client = new Pop3Client();
            try
            {
                client.Connect(user.pop3, user.port, user.ssl);
                try
                {
                    client.Authenticate(user.email, user.password);
                    return true;
                }
                catch
                {
                    client.Disconnect();
                }
            }
            catch
            {
            }
            return false;
        }
        public string checkConnection()
        {
            
            string returnString = "";
            try
            {
                client.Connect(user.pop3, user.port, user.ssl);
                try
                {
                    client.Authenticate(user.email, user.password);
                }
                catch (Exception ex)
                {
                    returnString = ex.Message;
                    client.Disconnect();
                }
            }
            catch (Exception ex)
            {
                returnString = ex.Message;
            }
            client.Disconnect();
            return returnString;
        }
        public List<emailStruct> FetchAllMessages()
        {
            if(!SetConnection())
            {
                return null;
            }
            int messageCount = client.GetMessageCount();
            List<emailStruct> allEmails = new List<emailStruct>(messageCount);

            for (int i = 1; i <= messageCount; i++)
            {
                emailStruct email = new emailStruct();
                Message mes = client.GetMessage(i);
                email.messageID = mes.Headers.MessageId;
                email.senderName = mes.Headers.From.DisplayName;
                email.senderEmail = mes.Headers.From.MailAddress.Address;
                string cc = "";
                for (int j = 0; j < mes.Headers.Cc.Count; j++)
                {
                    if (!String.IsNullOrEmpty(cc))
                    {
                        cc += ",";
                    }
                    cc += mes.Headers.Cc[j];
                }
                string mailTo = "";
                for (int j = 0; j < mes.Headers.To.Count; j++)
                {
                    if (!String.IsNullOrEmpty(mailTo))
                    {
                        mailTo += ",";
                    }
                    mailTo += mes.Headers.To[j];
                }
                email.cc = cc;
                email.mailTo = mailTo;
                email.subject = mes.Headers.Subject;
                email.timeStamp = mes.Headers.Date;
                email.body = mes.FindFirstHtmlVersion().GetBodyAsText();
                email.receiver = user.email;
                email.seen = true;
                allEmails.Add(email);

            }
            client.Disconnect();
            return allEmails;

        }
        public List<emailStruct> FetchUnseenMessages()
        {
            SetConnection();
            List<string> uids = client.GetMessageUids();
            List<emailStruct> allEmails = new List<emailStruct>();

            for (int i = 0; i < uids.Count; i++)
            {
                string currentUidOnServer = uids[i];
                if (!seenUids.Contains(currentUidOnServer))
                {
                    emailStruct email = new emailStruct();
                    Message mes = client.GetMessage(i + 1);
                    email.messageID = mes.Headers.MessageId;
                    email.senderName = mes.Headers.From.DisplayName;
                    email.senderEmail = mes.Headers.From.MailAddress.Address;
                    string cc = "";
                    for (int j = 0; j < mes.Headers.Cc.Count; j++)
                    {
                        if (!String.IsNullOrEmpty(cc))
                        {
                            cc += ",";
                        }
                        cc += mes.Headers.Cc[j];
                    }
                    string mailTo = "";
                    for (int j = 0; j < mes.Headers.To.Count; j++)
                    {
                        if (!String.IsNullOrEmpty(mailTo))
                        {
                            mailTo += ",";
                        }
                        mailTo += mes.Headers.To[j];
                    }
                    email.cc = cc;
                    email.mailTo = mailTo;
                    email.subject = mes.Headers.Subject;
                    email.timeStamp = mes.Headers.Date;
                    email.body = mes.FindFirstHtmlVersion().GetBodyAsText();
                    email.receiver = user.email;
                    email.seen = false;
                    allEmails.Add(email);
                    seenUids.Add(currentUidOnServer);
                }
            }
            client.Disconnect();
            return allEmails;
        }
    }
}
