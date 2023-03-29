using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
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


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AdminRemoveBook.xaml
    /// </summary>
    public partial class AdminRemoveBook : Window
    {
        private Global _global;
        public AdminRemoveBook(Global globals)
        {
            InitializeComponent();

            _global = globals;

            DataSet data = new DataSet();

            data.ReadXml(@"Library.xml");

            dtgBookRemoval.ItemsSource = data.Tables[0].DefaultView;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            AdminRemoveBook remove = new AdminRemoveBook(_global);

            remove.Show();

            this.Hide();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            xmlController uses = new xmlController();

            uses.BookDelete(txtTitle.Text);

            MessageBox.Show("Book deleted");
        }

        private void btnAdminHome_Click(object sender, RoutedEventArgs e)
        {
            AdminHome home = new AdminHome(_global);

            home.Show();

            this.Hide();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
             DataSet dataSet = new DataSet();
            //Reads the XML file into the dataset
            dataSet.ReadXml(@"Library.xml");
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
            dtgBookRemoval.ItemsSource = dv;
            dtgBookRemoval.Items.Refresh();
        }

        private void dtgBookRemoval_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = dtgBookRemoval.SelectedItem as DataRowView;

            txtTitle.Text = row.Row.ItemArray[0].ToString();
        }
    }
}
