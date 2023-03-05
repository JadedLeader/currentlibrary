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
            int counter = 0;

            string path = "AccountDetails.xml";

            XmlDocument xml = new XmlDocument();

            xml.Load(path);

            //grab the username and password from the xml file 

            string usernames = txtUsernameInput.Text;
            string passwords = txtPasswordInput.Text;

            

            XmlNodeList nodelist = xml.GetElementsByTagName("username");
            string username = string.Empty; 

            foreach (XmlNode node in nodelist)
            {
                username = node.InnerText;

                if(usernames == username)
                {
                    counter++;
                    MessageBox.Show("username worked");
                }
            }

            XmlNodeList nodelist2 = xml.GetElementsByTagName("password"); 
            string password = string.Empty;

            foreach(XmlNode node in nodelist2)
            {
                password = node.InnerText;

                if(passwords == password)
                {
                    counter++;
                    MessageBox.Show("password worked");
                }
              
            }


            if(counter == 2)
            {
                BookHomePage home = new BookHomePage();

                home.Show();

                this.Hide();
            }

            Accounts account = new Accounts(); 


            

        }

        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            //if they press login they can move to the signup window
            SignupPage signup = new SignupPage();

            signup.Show();
            this.Close();
        }

        private void btnAdmins_Click(object sender, RoutedEventArgs e)
        {
            AdminHome admin = new AdminHome(); 

            admin.Show();

            this.Close();
        }
    }
}
