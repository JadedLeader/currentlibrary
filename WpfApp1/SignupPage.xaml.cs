using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
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
using System.Xml;
using System.Xml.Linq;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for SignupPage.xaml
    /// </summary>
    public partial class SignupPage : Window
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        private void txtUsernameSignup_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnSignupConfirmation_Click(object sender, RoutedEventArgs e)
        {
            //generating a number for the ISBN number when a new user is added
                Random rand = new Random();
                string randomise = Convert.ToString(rand.Next(1, 300));


            string paths = "AccountDetails.xml";

            XDocument loading = XDocument.Load(paths); 

            Accounts account = new Accounts();

            account.username = txtUsernameSignup.Text;
            account.password = txtPasswordSignup.Text;
            account.email = txtEmail.Text;
            account.PhoneNumber = txtPhoneNumber.Text;
            account.librarycard = randomise;

            loading.Element("accounts").Add(new XElement("user",
                new XElement("username", account.username),
                new XElement("password", account.password),
                new XElement("email", account.email),
                new XElement("PhoneNumber", account.PhoneNumber),
                new XElement("LibraryCard", account.librarycard),
                new XElement("BooksCheckedOut")));
               

            loading.Save(paths);

            MainWindow window = new MainWindow(); 

                window.Show();
                this.Close(); 

            






        }

        private void txtPasswordSignup_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
