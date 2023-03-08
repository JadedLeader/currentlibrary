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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AdminCreation.xaml
    /// </summary>
    public partial class AdminCreation : Window
    {
        public AdminCreation()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            //we need to create new element in the file when the user clicks the button where the username node and the password node are equal to the text box . text

            string path = "AdminDetails.xml";

            Administrator admins = new Administrator();

            XDocument doc = XDocument.Load(path);

            admins.AdminUsername = txtUsernameCreation.Text; 
            admins.AdminPassword = txtPasswordCreation.Text;

            doc.Element("Admin").Add(new XElement("admins",
                new XElement("username", admins.AdminUsername),
                new XElement("password", admins.AdminPassword)));

            doc.Save(path);

            MessageBox.Show("New admin account created");

            MainWindow main = new MainWindow();

            main.Show();

            this.Hide();


        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            txtUsernameCreation.Clear();
            txtPasswordCreation.Clear();
        }
    }
}
