using System;
using System.Collections.Generic;
using System.Data;
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


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AdminRemoveBook.xaml
    /// </summary>
    public partial class AdminRemoveBook : Window
    {
        public AdminRemoveBook()
        {
            InitializeComponent();

            DataSet data = new DataSet();

            data.ReadXml(@"Library.xml");

            dtgBookRemoval.ItemsSource = data.Tables[0].DefaultView;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            AdminRemoveBook remove = new AdminRemoveBook();

            remove.Show();

            this.Hide();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            xmlController uses = new xmlController();

            uses.BookDelete(txtTitle.Text);

            MessageBox.Show("Book deleted");
        }
    }
}
