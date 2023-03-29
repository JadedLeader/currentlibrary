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
using System.Data;
using System.Reflection.Emit;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MemberAccount.xaml
    /// </summary>
    public partial class MemberAccount : Window
    {

        string path = "AccountDetails.xml";
        string library = "Library.xml";

        private Global _global;
        private String Username; 
        public MemberAccount(Global global )
        {
            InitializeComponent();

            _global = global;

            Username = _global.UserCurrent.username;

            xmlController xmlController = new xmlController();

            List<CheckedOutBooks> data = xmlController.Getuserscheckedoutbooks(Username);

            dtgMemberAccount.ItemsSource = data;
               
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            BookHomePage home = new BookHomePage(_global);

            home.Show();

            this.Hide();
        }

        private void btnCurrentBooksCheckedOut_Click(object sender, RoutedEventArgs e)
        {
            CheckedOutMembersBooks checkedOutMembersBooks = new CheckedOutMembersBooks(_global);

            checkedOutMembersBooks.Show();

            this.Hide();
        }

        private void btnReturnedBooks_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            
            MemberAccount member = new MemberAccount(_global);

            member.Show();

            this.Close();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
