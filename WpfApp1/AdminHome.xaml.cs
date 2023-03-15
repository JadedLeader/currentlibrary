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
        public AdminHome(Global global)
        {
            InitializeComponent();

            DataSet data = new DataSet();

            data.ReadXml(@"AccountDetails.xml");

            dtgUserList.ItemsSource = data.Tables[0].DefaultView;
            
            _global = global;

            adminusername = _global.AdminCurrent.AdminUsername;   

            lblAdminName.Content = adminusername;
        }

        private void btnAddBookScreen_Click(object sender, RoutedEventArgs e)
        {
            Admins admins = new Admins();

            admins.Show();

            this.Close();
        }

        private void btnUpdateBook_Click(object sender, RoutedEventArgs e)
        {
            AdminUpdate adminupdate = new AdminUpdate();

            adminupdate.Show();

            this.Close();
        }

        private void btnRemoveUser_Click(object sender, RoutedEventArgs e)
        {
            MemberRemoval remove = new MemberRemoval();

            remove.Show();

            this.Close();
        }

        private void btnRemoveBook_Click(object sender, RoutedEventArgs e)
        {
            AdminRemoveBook remove = new AdminRemoveBook();

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
            EditMember editmember = new EditMember();

            editmember.Show();

            this.Hide();
        }
    }
}
