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
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AdminHome.xaml
    /// </summary>
    public partial class AdminHome : Window
    {

        private Global _global;
        public String adminusername;

        string usersdeets = "AccountDetails.xml";
        public AdminHome(Global globals)
        {
            InitializeComponent();

            _global = globals;

            DataSet data = new DataSet();

            data.ReadXml(usersdeets);

            dtgUserList.ItemsSource = data.Tables[0].DefaultView;
            
            adminusername = _global.AdminCurrent.AdminUsername;   

            lblAdminName.Content = "Welcome, " + adminusername;
        }

        private void btnAddBookScreen_Click(object sender, RoutedEventArgs e)
        {
            Admins admins = new Admins(_global);

            admins.Show();

            this.Close();
        }

        private void btnUpdateBook_Click(object sender, RoutedEventArgs e)
        {
            AdminUpdate adminupdate = new AdminUpdate(_global);

            adminupdate.Show();

            this.Close();
        }

        private void btnRemoveUser_Click(object sender, RoutedEventArgs e)
        {
            MemberRemoval remove = new MemberRemoval(_global);

            remove.Show();

            this.Close();
        }

        private void btnRemoveBook_Click(object sender, RoutedEventArgs e)
        {
            AdminRemoveBook remove = new AdminRemoveBook(_global);

            remove.Show();

            this.Hide(); 
        }

        private void btnCreatingNewAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminCreation adminCreation = new AdminCreation();

            adminCreation.Show();

            this.Hide();
        }

        private void btnCreateMember_Click(object sender, RoutedEventArgs e)
        {
            SignupPage signup = new SignupPage();

            signup.Show();

            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EditMember editmember = new EditMember(_global);

            editmember.Show();

            this.Hide();
        }
    }
}
