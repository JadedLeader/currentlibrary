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

        private Global _global;

        public EditMember(Global globals)
        {
            InitializeComponent();

            _global = globals;

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
            EditMember edit = new EditMember(_global);

            edit.Show();

            this.Hide();
        }

        private void btnAdminhome_Click(object sender, RoutedEventArgs e)
        {
            AdminHome home = new AdminHome(_global);

            home.Show();

            this.Hide();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
           /* DataSet dataSet = new DataSet();
            //Reads the XML file into the dataset
            dataSet.ReadXml(@"AccountDetails.xml");
            //Sets the datasource for the datagrid to be the dataset
            //dgVideos.ItemsSource = dataSet.Tables[0].DefaultView;

            DataView dv = dataSet.Tables[0].DefaultView;

            StringBuilder sb = new StringBuilder();
            foreach (DataColumn column in dv.Table.Columns)
            {
                sb.AppendFormat("[{0}] Like '%{1}%' OR ", column.ColumnName, txtSearch.Text);
            }
            sb.Remove(sb.Length - 3, 3);
            dv.RowFilter = sb.ToString();
            dtgMemberData.ItemsSource = dv;
            dtgMemberData.Items.Refresh(); */
        }
    }
}
