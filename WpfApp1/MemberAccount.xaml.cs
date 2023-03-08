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
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MemberAccount.xaml
    /// </summary>
    public partial class MemberAccount : Window
    {

        string path = "AccountDetails.xml";

        private Global _global;
        private String Username; 
        public MemberAccount(Global global )
        {
            InitializeComponent();

            _global = global;

            Username = _global.UserCurrent.username;

            //when the page loads up we want to filter by the username that they logged in with then initiliase a data grid with that info

            DataSet data = new DataSet();

            data.ReadXml(path);

            dtgMemberAccount.ItemsSource = data.Tables[0].DefaultView;

            //worst comes to worst if you can't get the data grid view to work to show you the books a user currently has checked out just filter the file using xdoc and put it in a txt box/block


        }
    }
}
