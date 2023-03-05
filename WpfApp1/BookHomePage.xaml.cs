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
using System.Xml;
using System.Security.Principal;
using System.Management.Instrumentation;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for BookHomePage.xaml
    /// </summary>
    public partial class BookHomePage : Window
    {
        string path = "AccountDetails.xml";

        public BookHomePage()
        {
            InitializeComponent();

            lblTest.Content = Accounts.activeUser.username;

            DataSet data = new DataSet();

            data.ReadXml(@"Library.xml");

            dtgBooksShowing.ItemsSource = data.Tables[0].DefaultView;

            //making it show the current users username when they're logged in



            





        }

        private void btnUserProfile_Click(object sender, RoutedEventArgs e)
        {
            MemberAccount MemberAccount = new MemberAccount();

            MemberAccount.Show(); 

            this.Close();
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            

          
        }

        private void btnCheckout_Click(object sender, RoutedEventArgs e)
        {
            //we need to take the value of the title text box and update the users info with the title of that text box

            //call checkout

            xmlController uses = new xmlController();

            Accounts account = new Accounts();

            uses.CheckingOutBook(txtTitle.Text, account);

            MessageBox.Show("book has been checked out");



        }

        private void dtgBooksShowing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = dtgBooksShowing.SelectedItem as DataRowView;

            txtTitle.Text = row.Row.ItemArray[0].ToString();
            txtAuthor.Text = row.Row.ItemArray[1].ToString();
            txtYear.Text = row.Row.ItemArray[2].ToString();
            txtPublisher.Text = row.Row.ItemArray[3].ToString();
            txtIsbn.Text = row.Row.ItemArray[4].ToString();
            txtCategory.Text = row.Row.ItemArray[5].ToString();
        }

        DataGrid grid;
        private void txtSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            

        }
    }
}
