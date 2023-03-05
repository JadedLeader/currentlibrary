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
    /// Interaction logic for Admins.xaml
    /// </summary>
    public partial class Admins : Window
    {
        public Admins()
        {
            InitializeComponent();

            DataSet data = new DataSet();

            data.ReadXml(@"Library.xml");

            dtgCurrentBooks.ItemsSource = data.Tables[0].DefaultView;
        }

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            xmlController uses = new xmlController();

            Books book = new Books();

            book.title = txtTitle.Text;
            book.author = txtAuthor.Text;
            book.year = txtYear.Text;
            book.publisher = txtPublisher.Text;
            book.isbn = txtISBN.Text;
            book.category = txtCategory.Text;

            uses.AddBook(book);
            MessageBox.Show("Book added");
        }

        private void btnRefresh__Click(object sender, RoutedEventArgs e)
        {
            Admins admins = new Admins();

            admins.Show();

            this.Close();
        }
    }
}
