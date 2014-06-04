using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Net;

namespace RNNEmailClient
{
    /// <summary>
    /// Contain all the database connection
    /// </summary>
    public class MailDatabase
    {
        SQLiteConnection sqlCon;
        SQLiteCommand sqlCom;
        /// <summary>
        /// Connect to the database
        /// </summary>
        public MailDatabase()
        {
            sqlCon = new SQLiteConnection(@"Data Source=EmailSqlite.s3db;Version=3;");
        }
        /// <summary>
        /// Add an email to the database
        /// </summary>
        /// <param name="email">the email there should be added</param>
        public void addEmailToDB(PopClient.emailStruct email)
        {
            if (getEmailByMessageID(email.messageID).Count == 0)
            {
                sqlCon.Open();
                string sql = @"INSERT INTO Email (MessageID, SenderName, Body, TimeStamp, Cc, Subject, MailTo, Attachment, SenderEmail, Receiver, Seen) VALUES 
                            (@messageID,@senderName,@body,@timeStamp,@cc,@subject,@mailTo,@attachment,@senderEmail,@receiver,@seen)";
                sqlCom = new SQLiteCommand(sql, sqlCon);
                if (String.IsNullOrEmpty(email.messageID))
                {
                    email.messageID = "NULL";
                }
                sqlCom.Parameters.Add(new SQLiteParameter("@messageID", email.messageID));
                sqlCom.Parameters.Add(new SQLiteParameter("@senderName", email.senderName));
                sqlCom.Parameters.Add(new SQLiteParameter("@body", WebUtility.HtmlEncode(email.body)));
                sqlCom.Parameters.Add(new SQLiteParameter("@timeStamp", email.timeStamp));
                sqlCom.Parameters.Add(new SQLiteParameter("@cc", email.cc));
                sqlCom.Parameters.Add(new SQLiteParameter("@subject", email.subject));
                sqlCom.Parameters.Add(new SQLiteParameter("@mailTo", email.mailTo));
                sqlCom.Parameters.Add(new SQLiteParameter("@attachment", email.attachment));
                sqlCom.Parameters.Add(new SQLiteParameter("@senderEmail", email.senderEmail));
                sqlCom.Parameters.Add(new SQLiteParameter("@receiver", email.receiver));
                sqlCom.Parameters.Add(new SQLiteParameter("@seen", email.seen));
                sqlCom.ExecuteNonQuery();
                sqlCon.Close();
            }
        }
        /// <summary>
        /// Get emails from the database by the messageID
        /// returns only more then 1 email if its a spam mail
        /// </summary>
        /// <param name="messageID">the messageID</param>
        /// <returns>the list of emails</returns>
        public List<PopClient.emailStruct> getEmailByMessageID(string messageID)
        {
            List<PopClient.emailStruct> emails = new List<PopClient.emailStruct>();
            PopClient.emailStruct email = new PopClient.emailStruct();
            sqlCon.Open();
            string sql = "SELECT * FROM Email WHERE MessageID='" + messageID + "'";
            sqlCom = new SQLiteCommand(sql, sqlCon);
            SQLiteDataReader reader = sqlCom.ExecuteReader();

            while (reader.Read())
            {
                email.messageID = reader["MessageID"].ToString();
                email.senderName = reader["SenderName"].ToString();
                email.senderEmail = reader["SenderEmail"].ToString();
                email.body = WebUtility.HtmlDecode(reader["Body"].ToString());
                email.timeStamp = reader["TimeStamp"].ToString();
                email.cc = reader["Cc"].ToString();
                email.subject = reader["Subject"].ToString();
                email.mailTo = reader["MailTo"].ToString();
                email.attachment = reader["Attachment"].ToString();
                email.receiver = reader["Receiver"].ToString();
                email.seen = Convert.ToBoolean(reader["Seen"].ToString());
                emails.Add(email);
            }
            sqlCon.Close();
            return emails;
        }
        /// <summary>
        /// get all users mails messageID
        /// </summary>
        /// <param name="receiverEmail">the user email address</param>
        /// <returns>the list of messageIDs</returns>
        public List<string> getUserMessageIDList(string receiverEmail)
        {
            List<string> MessageIDList = new List<string>();

            sqlCon.Open();
            string sql = "SELECT MessageID FROM Email WHERE Receiver='" + receiverEmail + "'";
            sqlCom = new SQLiteCommand(sql, sqlCon);
            try
            {
                SQLiteDataReader reader = sqlCom.ExecuteReader();
                while (reader.Read())
                {
                    MessageIDList.Add(reader["MessageID"].ToString());
                }
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            
            return MessageIDList;
        }
        /// <summary>
        /// Gets all the emails for the user
        /// </summary>
        /// <param name="receiverEmail">the email address of the user</param>
        /// <returns>the list of emails</returns>
        public List<PopClient.emailStruct> getUserEmailList(string receiverEmail)
        {
            List<PopClient.emailStruct> emailList = new List<PopClient.emailStruct>();

            sqlCon.Open();
            string sql = "SELECT * FROM Email WHERE Receiver='" + receiverEmail + "'";
            sqlCom = new SQLiteCommand(sql, sqlCon);
            SQLiteDataReader reader = sqlCom.ExecuteReader();
            while (reader.Read())
            {
                PopClient.emailStruct email = new PopClient.emailStruct();
                email.messageID = reader["MessageID"].ToString();
                email.senderName = reader["SenderName"].ToString();
                email.senderEmail = reader["SenderEmail"].ToString();
                email.body = WebUtility.HtmlDecode(reader["Body"].ToString());
                email.timeStamp = reader["TimeStamp"].ToString();
                email.cc = reader["Cc"].ToString();
                email.subject = reader["Subject"].ToString();
                email.mailTo = reader["MailTo"].ToString();
                email.attachment = reader["Attachment"].ToString();
                email.receiver = reader["Receiver"].ToString();
                email.seen = Convert.ToBoolean(reader["Seen"].ToString());
                emailList.Add(email);
            }
            sqlCon.Close();
            return emailList;
        }
        /// <summary>
        /// Get all emails by the user by the user email address and sender mail
        /// </summary>
        /// <param name="receiverEmail">user email address</param>
        /// <param name="senderEmail">sender email address</param>
        /// <returns>the list of emails</returns>
        public List<PopClient.emailStruct> getUserEmailListByreciverEmailAndSenderEmail(string receiverEmail, string senderEmail)
        {
            List<PopClient.emailStruct> emailList = new List<PopClient.emailStruct>();

            sqlCon.Open();
            string sql = "SELECT * FROM Email WHERE Receiver='" + receiverEmail + "' AND SenderEmail='" + senderEmail + "'";
            sqlCom = new SQLiteCommand(sql, sqlCon);
            SQLiteDataReader reader = sqlCom.ExecuteReader();
            while (reader.Read())
            {
                PopClient.emailStruct email = new PopClient.emailStruct();
                email.messageID = reader["MessageID"].ToString();
                email.senderName = reader["SenderName"].ToString();
                email.senderEmail = reader["SenderEmail"].ToString();
                email.body = WebUtility.HtmlDecode(reader["Body"].ToString());
                email.timeStamp = reader["TimeStamp"].ToString();
                email.cc = reader["Cc"].ToString();
                email.subject = reader["Subject"].ToString();
                email.mailTo = reader["MailTo"].ToString();
                email.attachment = reader["Attachment"].ToString();
                email.receiver = reader["Receiver"].ToString();
                email.seen = Convert.ToBoolean(reader["Seen"].ToString());
                emailList.Add(email);
            }
            sqlCon.Close();
            return emailList;
        }
        
    }
}
