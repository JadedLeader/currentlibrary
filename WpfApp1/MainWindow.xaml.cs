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
        private Global _global;

        public MainWindow()
        {
            _global = new Global();

            InitializeComponent();
        }

        

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            string path = "AccountDetails.xml";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            XmlNodeList nodelist = xmlDoc.DocumentElement.SelectNodes("/accounts/user");

            foreach (XmlNode node in nodelist)
            {
                //to get the values of nodes within the user node
                XmlNode username = node.SelectSingleNode("username");
                XmlNode password = node.SelectSingleNode("password");

                if (txtUsernameInput.Text == username.InnerText && txtPasswordInput.Text == password.InnerText)
                {
                    _global.UserCurrent = new Accounts
                    {
                        username = username.InnerText,
                        password = password.InnerText,
                        email = node.SelectSingleNode("email").InnerText,
                        PhoneNumber = node.SelectSingleNode("PhoneNumber").InnerText,
                        librarycard = node.SelectSingleNode("LibraryCard").InnerText,
                        numberofbookscheckedout = node.SelectSingleNode("NumberOfBooksCheckedOut").InnerText,
                        bookscheckedout = node.SelectSingleNode("BooksCheckedOut").InnerText,
                        duedate = node.SelectSingleNode("DueDate").InnerText, 


                    };

                    BookHomePage home = new BookHomePage(_global);

                    home.Show();

                    this.Hide();

                    MessageBox.Show("username and password are within the file");

                    

                }
            }





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
