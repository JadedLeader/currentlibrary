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
    /// Interaction logic for AdminUpdate.xaml
    /// </summary>
    public partial class AdminUpdate : Window
    {
        public AdminUpdate()
        {
            InitializeComponent();

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
            AdminUpdate admins = new AdminUpdate();

            admins.Show();

            this.Close();
        }
    }
}
