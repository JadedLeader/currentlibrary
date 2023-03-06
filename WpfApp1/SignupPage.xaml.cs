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

            /*
                Accounts account = new Accounts(); 

                account.username = txtUsernameSignup.Text;
                account.password = txtPasswordSignup.Text;
                account.email = txtEmail.Text;
                account.PhoneNumber = txtPhoneNumber.Text;
                account.librarycard = randomise;
                account.bookscheckedout = "none";
                account.numberofbookscheckedout = "0";
                account.duedate = DateTime.Now; 

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

                XmlElement LibraryCard = doc.CreateElement("LibraryCard"); 
                LibraryCard.InnerText = account.librarycard;

                XmlElement bookscheckedout = doc.CreateElement("BooksCheckedOut");
                bookscheckedout.InnerText = account.bookscheckedout;

                XmlElement numberofbookscheckedout = doc.CreateElement("NumberOfBooksCheckedOut");
                numberofbookscheckedout.InnerText = account.numberofbookscheckedout;

                XmlElement duedate = doc.CreateElement("DueDate");
                duedate.InnerText = account.duedate.ToString();



                user.AppendChild(username);
                user.AppendChild(password);
                user.AppendChild(email);
                user.AppendChild(PhoneNumber);
                user.AppendChild(LibraryCard);
                user.AppendChild(bookscheckedout);
                user.AppendChild(numberofbookscheckedout);
                user.AppendChild(duedate);

                doc.DocumentElement.AppendChild(user);
                doc.Save("AccountDetails.xml"); */

            /* xmlController xmlc = new xmlController(); 
             Accounts account = new Accounts();
             xmlc.AddingMember(account); */

            string paths = "AccountDetails.xml";

            XDocument loading = XDocument.Load(paths); 

            Accounts account = new Accounts();

            account.username = txtUsernameSignup.Text;
            account.password = txtPasswordSignup.Text;
            account.email = txtEmail.Text;
            account.PhoneNumber = txtPhoneNumber.Text;
            account.librarycard = randomise;
            account.bookscheckedout = "none";
            account.numberofbookscheckedout = "0";
            account.duedate = "";

            loading.Element("accounts").Add(new XElement("user",
                new XElement("username", account.username),
                new XElement("password", account.password),
                new XElement("email", account.email),
                new XElement("PhoneNumber", account.PhoneNumber),
                new XElement("LibraryCard", account.librarycard),
                new XElement("NumberOfBooksCheckedOut", account.numberofbookscheckedout),
                new XElement("BooksCheckedOut", account.bookscheckedout),
                new XElement("DueDate", account.duedate)));

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
