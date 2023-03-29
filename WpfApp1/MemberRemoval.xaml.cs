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
    /// Interaction logic for MemberRemoval.xaml
    /// </summary>
    public partial class MemberRemoval : Window
    {

        private Global _global; 
        public MemberRemoval(Global globals)
        {
            InitializeComponent();

            _global = globals;

            DataSet data = new DataSet();

            data.ReadXml(@"AccountDetails.xml");

            dtgMemberDetails.ItemsSource = data.Tables[0].DefaultView;


        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            MemberRemoval remove = new MemberRemoval(_global);

            remove.Show();

            this.Close();
        }

        private void btnDeleteMember_Click(object sender, RoutedEventArgs e)
        {
            xmlController uses = new xmlController(); 

            uses.MemberDelete(txtMemberUsername.Text);

            MessageBox.Show("Member deleted");
        }

        private void btnAdminHome_Click(object sender, RoutedEventArgs e)
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
            dtgMemberDetails.ItemsSource = dv;
            dtgMemberDetails.Items.Refresh(); */
        }

        private void dtgMemberDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = dtgMemberDetails.SelectedItem as DataRowView; 

            txtMemberUsername.Text = row.Row.ItemArray[0].ToString();
            
        }
    }
}
