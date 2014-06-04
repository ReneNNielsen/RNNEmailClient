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

namespace RNNEmailClient
{
    /// <summary>
    /// Interaction logic for EmilListItems.xaml
    /// an item for EmailListShower
    /// </summary>
    public partial class EmailListItems : UserControl
    {
        PopClient.emailStruct email;
        MainWindow mw;
        /// <summary>
        /// an item for EmailListShower
        /// </summary>
        /// <param name="_email">PopClient.emailStruct</param>
        /// <param name="_mw">MainWindow</param>
        public EmailListItems(PopClient.emailStruct _email, MainWindow _mw)
        {
            InitializeComponent();
            email = _email;
            mw = _mw;
        }
        /// <summary>
        /// Adds this email to the controll when control has been loaded
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            lblDate.Content = email.timeStamp;
            lblFrom.Content = email.senderName + " (" + email.senderEmail + ")";
            lblSubject.Content = email.subject;
        }
        /// <summary>
        /// Update the emailShower when an email has been clicked()
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            EmailShower em = new EmailShower(email);
            mw.rightContent.Children.Clear();
            mw.rightContent.Children.Add(em);
        }
    }
}
