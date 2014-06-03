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
    /// Interaction logic for EmailListShower.xaml
    /// </summary>
    public partial class EmailListShower : UserControl
    {
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
