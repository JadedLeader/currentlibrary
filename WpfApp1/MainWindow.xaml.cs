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

            XDocument doc = XDocument.Load(path);

                var singleuser = doc.Root.Elements("user")
                .SingleOrDefault(x => x.Element("username").Value == txtUsernameInput.Text && x.Element("password").Value == txtPasswordInput.Text);

            if(singleuser == null)
            {
                MessageBox.Show("not found");
            }
            else
            {
                _global.UserCurrent = new Accounts
                {
                    username = singleuser.Element("username").Value,
                    password = singleuser.Element("password").Value,
                    email = singleuser.Element("email").Value,
                    PhoneNumber = singleuser.Element("PhoneNumber").Value, 
                    librarycard = singleuser.Element("LibraryCard").Value,

                    //talk to john about this part, breaks for some reason
                 /*   bookscheckedout = singleuser.Element("BooksCheckedOut").Elements().Select(x => 
                        new CheckedOutBooks 
                        {
                            Title = x.Element("Title").Value, 
                            checkedoutdate = x.Element("checkedoutdate").Value, 
                            duebackdate = x.Element("duebackdate").Value
                        }).ToList(), */
                };

                BookHomePage home = new BookHomePage(_global);

                home.Show();

                this.Hide();

                MessageBox.Show("username and password are within the file");
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
