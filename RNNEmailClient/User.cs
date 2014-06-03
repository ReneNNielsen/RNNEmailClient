using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNNEmailClient
{
    public static class User
    {
        /// <summary>
        /// the struct of an user
        /// </summary>
        public struct user
        {
            public string email;
            public string password;
            public string pop3;
            public int port;
            public bool ssl;
        }
        /// <summary>
        /// The list of users
        /// </summary>
        public static List<user> users = new List<user>();

        /// <summary>
        /// Add an user to settings
        /// </summary>
        /// <param name="newUser">the user</param>
        public static void AddUser(user newUser)
        {
            if (!String.IsNullOrEmpty(Properties.Settings.Default.users))
            {
                Properties.Settings.Default.users = ";";
            }
            Properties.Settings.Default.users = newUser.email + "," + newUser.password + "," + newUser.pop3 + "," + newUser.port.ToString() + "," + newUser.ssl.ToString();
            Properties.Settings.Default.Save();
            users.Add(newUser);
        }
        /// <summary>
        /// Add users to the user list from settings
        /// </summary>
        public static void GetUsersFromSettings()
        {
            string[] allSavedUsers = Properties.Settings.Default.users.Split(';');
            foreach (string savedUser in allSavedUsers)
            {
                if (String.IsNullOrEmpty(savedUser))
                {
                    break;
                }
                string[] savedUserArr = savedUser.Split(',');
                user _user = new user();
                _user.email = savedUserArr[0];
                _user.password = savedUserArr[1];
                _user.pop3 = savedUserArr[2];
                _user.port = int.Parse(savedUserArr[3]);
                _user.ssl = bool.Parse(savedUserArr[4]);
                users.Add(_user);
            }

        }
    }
}
