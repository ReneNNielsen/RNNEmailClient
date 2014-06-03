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
    /// Interaction logic for EmailShower.xaml
    /// </summary>
    public partial class EmailShower : UserControl
    {
        

        PopClient.emailStruct email;
        public EmailShower(PopClient.emailStruct _email)
        {
            InitializeComponent();
            email = _email;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            lblDate.Content = email.timeStamp;
            lblFrom.Content = email.senderName + " (" + email.senderEmail + ")";
            lblSubject.Content = email.subject;
            lblTo.Content = email.mailTo;
            if (!String.IsNullOrEmpty(email.body))
            {
                viewer.NavigateToString(email.body);   
            }
        }
    }
}
