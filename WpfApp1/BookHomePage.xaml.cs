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
        public BookHomePage(Global global)
        {

            

            InitializeComponent();
            

            DataSet data = new DataSet();

            data.ReadXml(@"Library.xml");

            dtgBooksShowing.ItemsSource = data.Tables[0].DefaultView;

            //making it show the current users username when they're logged in

            _global = global;

            Username = _global.UserCurrent.username;

            lblTest.Content = Username;

            btnCheckout.IsEnabled = false;
            btnReturn.IsEnabled = false;
            btnReserve.IsEnabled = false;
            
        }

        private void btnUserProfile_Click(object sender, RoutedEventArgs e)
        {
            MemberAccount MemberAccount = new MemberAccount(_global);

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

            

            //loading the file
             XDocument doc = XDocument.Load(account);

            //filtering all the information so we're only getting everything below username
            var singleUser = doc.Descendants("user")
            .SingleOrDefault(x => x.Element("username").Value == Username);

            if (singleUser == null)
            {
                //you would need to handle if it could not find the user for some reason.
                return;
            }

            //grabbing the library card node, checkng it's value
            if (singleUser.Element("LibraryCard").Value == txtLibraryCard.Text)
            {
               //going into the bookscheckedout node, grabbing the book element and counting the total # that are in there
                if(singleUser.Element("BooksCheckedOut").Elements("book").Count() >= 6)
                {
                    //showing the user a message if the book limit has been reached
                    MessageBox.Show("Current book limit has been reached");

                    return;
                }

                //name of the element, contents of the element
                DateTime date = DateTime.Now;
                
                //adding a books checked out head element, making a blank sub directory of book, populating it with everything 
                singleUser.Element("BooksCheckedOut").Add(
                    new XElement("book", 
                        new XElement("BookCheckedOut", txtTitle.Text), 
                        new XElement("DateCheckedOut", date.ToShortDateString()), 
                        new XElement("DueDate", date.AddMonths(1).ToShortDateString())));

                //savign the file
                singleUser.Document.Save(account);

                MessageBox.Show("Save complete");
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

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            //check the users username that they logged in with

            //check the users library card # 

            /* if library card is number is valid then we want to go to the book node where it has the title of the book
               we then want to delete all of the things under that book node, book checked out, date checked out and due date */

            XDocument xdoc = XDocument.Load(account);

            var loading = xdoc.Root.Elements("user")
                .SingleOrDefault(x => x.Element("username").Value == Username && x.Element("LibraryCard").Value == txtLibraryCard.Text); 

            if(loading == null)
            {
                MessageBox.Show("couldn't find anything of this type");
            }
            else
            {
                //this is where we want to do all of our actual code - we want to delete book checked out, date checked out and due date depending on the title of the book 

                DeleteBook(txtTitle.Text, loading);

              

                MessageBox.Show("Book has has been returned");
                

            }





                
        }
        
        //passing the loading from the previous xdoc since it copies over the filtering we've already done into the function

        public void DeleteBook(string title, XElement usercollection)
        {
            

            var booktodelete = usercollection.Descendants("book")
                .Where(x => x.Element("BookCheckedOut").Value == title);
                
            booktodelete.Remove();

            usercollection.Document.Save(account);
            
        }

        private void txtLibraryCard_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtLibraryCard.Text == " ")
            {
                btnCheckout.IsEnabled = false; 
                btnReserve.IsEnabled = false;
                btnReturn.IsEnabled = false;
            }
            else
            {
                btnCheckout.IsEnabled = true;
                //reserve must go here

                btnReturn.IsEnabled = true;
            }
        }
    }
}
