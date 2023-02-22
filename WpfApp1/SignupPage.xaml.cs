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
using System.Xml;

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

            Accounts account = new Accounts(); 

            account.username = txtUsernameSignup.Text;
            account.password = txtPasswordSignup.Text;
            account.email = txtEmail.Text;
            account.PhoneNumber = txtPhoneNumber.Text;
            account.ISBN = randomise;

            XmlDocument doc = new XmlDocument();

            doc.Load("AccountDetails.xml");

            XmlElement user = doc.CreateElement("user");

            //adding a new username to the xml file 
            XmlElement username= doc.CreateElement("username");
            username.InnerText = account.username;

            XmlElement password = doc.CreateElement("password");
            password.InnerText = account.password;
            
            XmlElement email = doc.CreateElement("email");
            email.InnerText = account.email;

            XmlElement PhoneNumber = doc.CreateElement("PhoneNumber");
            PhoneNumber.InnerText = account.PhoneNumber;

            XmlElement ISBN = doc.CreateElement("ISBN"); 
            ISBN.InnerText = account.ISBN;

            user.AppendChild(username);
            user.AppendChild(password);
            user.AppendChild(email);
            user.AppendChild(PhoneNumber);
            user.AppendChild(ISBN);

            doc.DocumentElement.AppendChild(user);
            doc.Save("AccountDetails.xml");

            MainWindow window = new MainWindow(); 

            window.Show();
            this.Close();


        }

        private void txtPasswordSignup_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
