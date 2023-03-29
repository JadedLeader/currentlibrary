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
using System.Runtime.InteropServices;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AdminUpdate.xaml
    /// </summary>
    public partial class AdminUpdate : Window
    {

        private Global _global;
        public AdminUpdate(Global globals)
        {
            InitializeComponent();

            _global = globals;

            DataSet data = new DataSet();

            data.ReadXml(@"Library.xml");

            dtgUpdateGrid.ItemsSource = data.Tables[0].DefaultView;
            
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            xmlController uses = new xmlController();

            Books UpdatingBook = new Books();

            UpdatingBook.title = txtTitle.Text;
            UpdatingBook.author = txtAuthor.Text;
            UpdatingBook.year = txtYear.Text;
            UpdatingBook.publisher = txtPublisher.Text;
            UpdatingBook.isbn = txtISBN.Text;
            UpdatingBook.category = txtCategory.Text;

            uses.UpdateBook(txtTitle.Text, UpdatingBook);
            MessageBox.Show("recorded updated");
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            AdminUpdate admins = new AdminUpdate(_global);

            admins.Show();

            this.Close();
        }

        private void btnAdminHome_Click(object sender, RoutedEventArgs e)
        {
            AdminHome home = new AdminHome(_global);

            home.Show();

            this.Hide();
        }

        private void txtSearchBar_TextChanged(object sender, TextChangedEventArgs e)
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
                sb.AppendFormat("[{0}] Like '%{1}%' OR ", column.ColumnName, txtSearchBar.Text);
            }
            sb.Remove(sb.Length - 3, 3);
            dv.RowFilter = sb.ToString();
            dtgUpdateGrid.ItemsSource = dv;
            dtgUpdateGrid.Items.Refresh();
        }
    }
}
