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
using System.Xml;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for CheckedOutMembersBooks.xaml
    /// </summary>
    public partial class CheckedOutMembersBooks : Window
    {
        string account = "AccountDetails.xml";
        private Global _global;
        string username;
        public CheckedOutMembersBooks(Global globals)
        {
            InitializeComponent();

            _global = globals;

            username = _global.UserCurrent.username;

            //on initiation we want to fill the text block with all of the list information

            //we want to query the xml doc for the current username and password that's logged in then display the info of the book title and status into the text block

            XDocument xdoc = XDocument.Load(account);

            var Grabbinguserinfo = xdoc.Root.Elements("user")
                .SingleOrDefault(x => x.Element("username").Value == username);

            if(Grabbinguserinfo == null)
            {
                MessageBox.Show("this was null");

            }
            else
            {
                
                thing(Grabbinguserinfo);

            }
                
            

        }

        public void thing(XElement userinfo)
        {

            var grabbingthings = userinfo.Descendants("book")
                .Where(x => x.Element("status").Value == "CheckedOut");
                

            //working, currently shows title of the book, date checked out, due date, status

            foreach (var item in grabbingthings)
            {
                txtinfo_.Text += item.Value + "\n";
            }

            MessageBox.Show("possible it worked");

            
        }
    }
}
