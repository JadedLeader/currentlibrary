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
        public AdminHome()
        {
            InitializeComponent();

            DataSet data = new DataSet();

            data.ReadXml(@"AccountDetails.xml");

            dtgUserList.ItemsSource = data.Tables[0].DefaultView;
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
    }
}
