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
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Xml.Linq;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for BookHomePage.xaml
    /// </summary>
    public partial class BookHomePage : Window
    {
        string account = "AccountDetails.xml";

        private Global _global;
        public String Username;

        public string counter;
        public BookHomePage(Global global )
        {
            InitializeComponent();
            

            DataSet data = new DataSet();

            data.ReadXml(@"Library.xml");

            dtgBooksShowing.ItemsSource = data.Tables[0].DefaultView;

            //making it show the current users username when they're logged in

            _global = global;

            Username = _global.UserCurrent.username;

            lblTest.Content = Username;
            
            
            



            





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
            /* checking out a book:

            search by the users username

            get the descendants of the username

            make sure that the library card # they entered is in the same child node as the username 

            add the title of the book into the books checked out element
    
            add a month to the due date of when the book was checked out  */

             XDocument doc = XDocument.Load(account);

          /*   bool isLibraryCardInSystem = doc.Descendants("user")
            .Where(x => x.Element("username").Value == Username)
            .Any(x => x.Element("LibraryCard").Value == txtLibraryCard.Text);

            if (isLibraryCardInSystem)
            {
                //if library card is within the system this executes
                MessageBox.Show("library card is within the system");

                //adding the book title to the books checked out part of the xml file
                isLibraryCardInSystem.Element("BooksCheckedOut").Value == txtTitle.Text;
            }
            else
            {
                MessageBox.Show("library card is not within the system");
            } */

            //getting all of the descendants of user
            //filtering via the username

            var singleUser = doc.Descendants("user")
            .SingleOrDefault(x => x.Element("username").Value == Username);

            if (singleUser == null)
            {
                //you would need to handle if it could not find the user for some reason.
                return;
            }

            if (singleUser.Element("LibraryCard").Value == txtLibraryCard.Text)
            {
                DateTime date = DateTime.Now;

                MessageBox.Show("library card is within the system");

                singleUser.Element("BooksCheckedOut").Value = txtTitle.Text;

                singleUser.Element("NumberOfBooksCheckedOut").Value = counter += 1;

                singleUser.Element("DueDate").Value = date.AddMonths(1).ToString();

                singleUser.Document.Save(account);
            }
            else
            {
                MessageBox.Show("library card is not within the system");
            }









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

        
        private void txtSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            

        }

        


    }
}
