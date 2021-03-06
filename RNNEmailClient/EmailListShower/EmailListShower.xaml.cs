﻿using System;
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

namespace RNNEmailClient
{
    /// <summary>
    /// Interaction logic for EmailListShower.xaml
    /// Shows all the emails for the folder
    /// </summary>
    public partial class EmailListShower : UserControl
    {
        /// <summary>
        /// Shows all the emails for the folder
        /// </summary>
        /// <param name="emailList">the email to bee shown</param>
        /// <param name="_headerText">the header text of this usercontrol</param>
        /// <param name="mw">the main window</param>
        public EmailListShower(List<PopClient.emailStruct> emailList, string _headerText, MainWindow mw)
        {
            InitializeComponent();
            headerText.Content = _headerText;
            foreach (PopClient.emailStruct email in emailList)
            {
                EmailListItems newItem = new EmailListItems(email, mw);
                contentPanel.Children.Add(newItem);
            }
        }
    }
}
