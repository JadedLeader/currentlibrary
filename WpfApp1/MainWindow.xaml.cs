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


            /*  XmlDocument xml = new XmlDocument();

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

              Accounts account = new Accounts(); */

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
                    _global.currentuser = new Accounts
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

                    MessageBox.Show("username and password are within the file");

                    BookHomePage home = new BookHomePage();

                    home.Show();

                    this.Hide();

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
