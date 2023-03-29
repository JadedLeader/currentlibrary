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
using System.Xml.Linq;
using System.Xaml.Permissions;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Admins.xaml
    /// </summary>
    public partial class Admins : Window
    {

        private Global _global;

        string library = "library.xml";
        public Admins(Global globals)
        {
            InitializeComponent();

            _global = globals;
            DataSet data = new DataSet();

            data.ReadXml(@"Library.xml");

            dtgCurrentBooks.ItemsSource = data.Tables[0].DefaultView;
        }

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            

            Books book = new Books();

            XDocument loading = XDocument.Load(library); 



            book.title = txtTitle.Text;
            book.author = txtAuthor.Text;
            book.year = txtYear.Text;
            book.publisher = txtPublisher.Text;
            book.isbn = txtISBN.Text;
            book.category = txtCategory.Text;
            book.stocks = Convert.ToInt32(txtStock.Text);

            loading.Element("library").Add(new XElement("book",
                new XElement("title", book.title),
                new XElement("author", book.author),
                new XElement("publisher", book.publisher),
                new XElement("year", book.year),
                new XElement("isbn", book.isbn),
                new XElement("category", book.category),
                new XElement("stock", book.stocks)));

            loading.Save(library);

            
            MessageBox.Show("Book added");
        }

        private void btnRefresh__Click(object sender, RoutedEventArgs e)
        {
            Admins admins = new Admins(_global);

            admins.Show();

            this.Close();
        }

        private void btnAdminHome_Click(object sender, RoutedEventArgs e)
        {
            AdminHome home = new AdminHome(_global);

            home.Show();

            this.Hide();
        }

        private void btnAdminHome_Click_1(object sender, RoutedEventArgs e)
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
            dtgCurrentBooks.ItemsSource = dv;
            dtgCurrentBooks.Items.Refresh();
        }
    }
}
