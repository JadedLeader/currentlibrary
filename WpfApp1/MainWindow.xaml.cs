using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            BookHomePage yes = new BookHomePage();

            yes.Show();
            this.Close();

            int counter = 0;

            //this is the button where we're going to have to check nodes in the xml file to see if the information that has been entered into the text box is inside the xml file

            string usernames = txtUsernameInput.Text;
            string passwords = txtPasswordInput.Text;

            XmlDocument doc = new XmlDocument();

            doc.Load("AccountDetails.xml");

            //i want to read the username and the password and then compare what was inputted in the text box, if both are true, then let the user enter

            

            

            

        }

        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            //if they press login they can move to the signup window
            SignupPage signup = new SignupPage();

            signup.Show();
            this.Close();
        }
    }
}
