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
    /// Interaction logic for EditMember.xaml
    /// </summary>
    public partial class EditMember : Window
    {
        string account = "AccountDetails.xml";
        public EditMember()
        {
            InitializeComponent();

            DataSet data = new DataSet(); 

            data.ReadXml(account);

            dtgMemberData.ItemsSource = data.Tables[0].DefaultView;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Accounts account = new Accounts();

            xmlController control = new xmlController();

            account.username = txtUsername.Text;
            account.password = txtPassword.Text;
            account.email = txtEmail.Text;
            account.PhoneNumber = txtPhoneNumber.Text;
            account.librarycard = txtLibraryCard.Text;

            control.UpdatingMemberInfo(txtUsername.Text, account);

            MessageBox.Show("Member credentials updated"); 
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            EditMember edit = new EditMember();

            edit.Show();

            this.Hide();
        }
    }
}
