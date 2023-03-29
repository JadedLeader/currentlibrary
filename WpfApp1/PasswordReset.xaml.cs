using System;
using System.Collections.Generic;
using System.Data.Odbc;
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
    /// Interaction logic for PasswordReset.xaml
    /// </summary>
    public partial class PasswordReset : Window
    {

        string account = "AccountDetails.xml";
        public PasswordReset()
        {
            InitializeComponent();

            //txtPasswordReset.IsEnabled = false;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Hide(); 
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            XDocument xdoc = XDocument.Load(account); 

            var checking = xdoc.Descendants("user")
                .SingleOrDefault(x => x.Element("username").Value == txtConfirmUsername.Text && x.Element("LibraryCard").Value == txtConfirmPassword.Text);

            if(checking == null)
            {
                MessageBox.Show("There is no account registered with these details, please make sure your details are correct");
            }
            else
            {

                checking.Element("password").Value = txtPasswordReset.Text;

                checking.Document.Save(account);

                MessageBox.Show("Password has been reset!");



            }    
        }

    }
}
