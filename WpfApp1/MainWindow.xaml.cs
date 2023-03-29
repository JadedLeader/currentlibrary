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
        private Administrator administrator;

        public MainWindow()
        {
            _global = new Global();

            administrator = new Administrator();

            InitializeComponent();
        }



        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //admin path for the admin file
            string Administrator = "AdminDetails.xml";
            string path = "AccountDetails.xml";

            XDocument doc = XDocument.Load(path);

                var singleuser = doc.Root.Elements("user")
                .SingleOrDefault(x => x.Element("username").Value == txtUsernameInput.Text && x.Element("password").Value == txtPasswordInput.Text);

            if(singleuser == null)
            {
                

                XDocument xdoc = XDocument.Load(Administrator);

                var adminuser = xdoc.Root.Elements("admins")
                    .SingleOrDefault(x => x.Element("username").Value == txtUsernameInput.Text && x.Element("password").Value == txtPasswordInput.Text);

                if (adminuser == null)
                {
                    MessageBox.Show("Couldn't find the admin details");
                }
                else
                {
                    _global.AdminCurrent = new Administrator
                    {
                        AdminUsername = adminuser.Element("username").Value,
                        AdminPassword = adminuser.Element("password").Value,
                    };

                    MessageBox.Show("admin login registered");

                    AdminHome home = new AdminHome(_global);

                    home.Show();

                    this.Hide();
                }
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

                     
                }; 

                BookHomePage home = new BookHomePage(_global);

                home.Show();

                this.Hide();

                
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
            AdminHome admin = new AdminHome(_global); 

            admin.Show();

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            txtUsernameInput.Clear();
            txtPasswordInput.Clear();
        }

        private void btnForgottenPassword_Click(object sender, RoutedEventArgs e)
        {
            PasswordReset reset = new PasswordReset();

            reset.Show();

            this.Hide();
        }
    }
}
