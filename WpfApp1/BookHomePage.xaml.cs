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
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.Remoting.Messaging;
using System.Globalization;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for BookHomePage.xaml
    /// </summary>
    public partial class BookHomePage : Window
    {
        string account = "AccountDetails.xml";
        string library = "library.xml";

        private Global _global;
        public String Username;
        public String Librarycard;

        public string bookreturned = "Returned";
        public string bookout = "CheckedOut";
        public string bookrenewed = "Renewed";
        public string reserved = "Reserved";

        public string counter;
        public BookHomePage(Global global)
        {

            _global = new Global();

            InitializeComponent();
            

            DataSet data = new DataSet();

            data.ReadXml(@"Library.xml");

            dtgBooksShowing.ItemsSource = data.Tables[0].DefaultView;

            //making it show the current users username when they're logged in

            _global = global;

            Username = _global.UserCurrent.username;
            Librarycard = _global.UserCurrent.librarycard;

            lblTest.Content = "Welcome, "  + Username;
            lblLibraryCardNum.Content = "Library Card # " +  Librarycard;

            btnCheckout.IsEnabled = false;
            btnReturn.IsEnabled = false;
            //btnReserve.IsEnabled = false;
            //btnRenew.IsEnabled = false; 

            //this block of code is for checking if the member has a reserved book that has come back in stock, letting them know accordingly
            XDocument xdoc = XDocument.Load(account);

            var grabbinginfo = xdoc.Descendants("user")
                .SingleOrDefault(x => x.Element("username").Value == Username);

            if (grabbinginfo != null)
            {
                var grabbingvalue = grabbinginfo.Descendants("book")
                    .Where(x => x.Element("status").Value == "Reserved")
                    .Select(x => x.Element("BookCheckedOut"));

                if (grabbinginfo != null)
                {
                    XDocument thing = XDocument.Load(library);

                    var bookstockvalue = thing.Descendants("book")
                        .Where(x => x.Element("stock").Value == 1.ToString())
                        .Select(x => x.Element("title"));

                    if (bookstockvalue == null)
                    {

                        MessageBox.Show("This error'd somehow");

                    }
                    else
                    {
                        

                       foreach (var books in bookstockvalue)
                       {
                         if (grabbingvalue.Any(x => x.Value == books.Value))
                         {
                                lblReservedBooksStockReturn.Content += books.Value + "\n";
                         }

                       }
                    }
                }

            }

            //this is to let the user know that they have a book currently checked out in their name that has gone over their due date

            XDocument docx = XDocument.Load(account); 

            var grabbing = docx.Descendants("user")
                .Where(x => x.Element("username").Value == Username);

            if(grabbing != null)
            {
                DateTime day = DateTime.Now;

                var checking = grabbing.Descendants("book")
                    .Where(x => x.Element("status").Value == bookout)
                    .Select(x => x.Element("DueDate").Value);

                if(checking != null)
                {
                    foreach (var val in checking)
                    {
                        if (DateTime.TryParse(val, out DateTime dueDate) && dueDate < day)
                        {

                            MessageBox.Show("You have a book overdue, please check your account details to see which book is out of date");
                            
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("there is not a user of this type");
            }

        }

        private void btnUserProfile_Click(object sender, RoutedEventArgs e)
        {
            MemberAccount MemberAccount = new MemberAccount(_global);

            MemberAccount.Show(); 

            this.Close();
        }

        //button that allows the user to checkout a book, 
        private void btnCheckout_Click(object sender, RoutedEventArgs e)
        {
           
            //loading the file
             XDocument doc = XDocument.Load(account);

            //filtering all the information so we're only getting everything below username
            var singleUser = doc.Descendants("user")
            .SingleOrDefault(x => x.Element("username").Value == Username);

            if (singleUser == null)
            {
                //you would need to handle if it could not find the user for some reason.

                MessageBox.Show("Sorry, we couldn't find this user. Try entering your details again and if that doesn't work make sure you have an account");
               
            }

            //grabbing the library card node, checkng it's value, two functions are used, "grabbinglibrarystock" and "removingreservedbook"
            if (singleUser.Element("LibraryCard").Value == txtLibraryCard.Text)
            {

                XDocument docx = XDocument.Load(library);

                var thing = docx.Descendants("book")
                    .SingleOrDefault(x => x.Element("stock").Value == 0.ToString() && x.Element("title").Value == txtTitle.Text);

                if (thing == null)
                {
                  

                    //name of the element, contents of the element
                    DateTime date = DateTime.Now;

                    //adding a books checked out head element, making a blank sub directory of book, populating it with everything 
                    singleUser.Element("BooksCheckedOut").Add(
                        new XElement("book",
                            new XElement("BookCheckedOut", txtTitle.Text),
                            new XElement("DateCheckedOut", date.ToShortDateString()),
                            new XElement("DueDate", date.AddMonths(1).ToShortDateString()),
                            new XElement("status", bookout)));

                    //saving the file
                    singleUser.Document.Save(account);

                    //function being used
                    GrabbingLibraryStock(txtTitle.Text, _global);

                    MessageBox.Show("Book has been checked out!");

                    //function being used
                    RemovingReservedbook(singleUser, txtTitle.Text);
                }
                else
                {
                    MessageBox.Show("This book currently has no stock, please try reserving");
                }

               
            }
            else
            {
                MessageBox.Show("library card is not within the system");
            }


        }
       
        private void RemovingReservedbook(XElement usercheck, string title)
        {
            var thing = usercheck.Descendants("book")
                .Where(x => x.Element("status").Value == reserved && x.Element("BookCheckedOut").Value == title);

            thing.Remove();

            usercheck.Document.Save(account);

            MessageBox.Show("Reserved book for this user has now been deleted since it's checked out");
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
                MessageBox.Show("We couldn't find any details that match this, please make sure you're inputting the correct details for your account!");
            }
            else
            {
                //this is where we want to do all of our actual code - we want to delete book checked out, date checked out and due date depending on the title of the book 

                //DeleteBook(txtTitle.Text, loading);

                UpdatingBookReturn(loading, txtTitle.Text);

                //grabbing library stock shouldn't be here since it decrements by 1 when it should increment when the book is handed back in

                //GrabbingLibraryStock(txtTitle.Text, _global);
                GrabbingLibraryStockIncrement(txtTitle.Text, _global);

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

        public void UpdatingBookReturn(XElement bookreturn, string title)
        {

            var bookupdated = bookreturn.Descendants("book")
                .SingleOrDefault(x => x.Element("BookCheckedOut").Value == title && x.Element("status").Value != null);

            bookupdated.Element("status").Value = bookreturned;
            bookupdated.Element("DueDate").Value = "Due date removed";

            bookreturn.Document.Save(account);

            
        }

        public void GrabbingLibraryStock(string title, Global globals)
        {
           
            Books book = new Books();

            _global = globals;

            XDocument xdoc = XDocument.Load(library);

            var BookGrab = xdoc.Descendants("book")
                .SingleOrDefault(x => x.Element("title").Value == title);

            if(BookGrab == null)
            {
                MessageBox.Show("We couldn't find this book, please try selecting a different one");
            }
            else
            {
                var stock = Convert.ToInt32(BookGrab.Element("stock").Value);
                stock--;

                BookGrab.Element("stock").Value = stock.ToString();

                BookGrab.Document.Save(library);

            }

        }

        public void GrabbingLibraryStockIncrement(string title, Global globals)
        {
            
            Books book = new Books();

            _global = globals;

            XDocument xdoc = XDocument.Load(library);

            var BookGrab = xdoc.Descendants("book")
                .SingleOrDefault(x => x.Element("title").Value == title);

            if (BookGrab == null)
            {
                MessageBox.Show("We couldn't find this book, please try selecting a different one");
            }
            else
            {
                var stock = Convert.ToInt32(BookGrab.Element("stock").Value);
                stock++;

                BookGrab.Element("stock").Value = stock.ToString();

                BookGrab.Document.Save(library);
            }
        }

        private void txtLibraryCard_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtLibraryCard.Text == "")
            {
                btnCheckout.IsEnabled = false; 
               // btnReserve.IsEnabled = false;
                //btnReturn.IsEnabled = false;
                btnRenew.IsEnabled = false;
                
            }
            else
            {
                btnCheckout.IsEnabled = true;
               // btnReserve.IsEnabled = true;
                //btnRenew.IsEnabled = true;
                btnReturn.IsEnabled = true;
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();

            main.Show();

            this.Hide();
        }

        private void btnRenew_Click(object sender, RoutedEventArgs e)
        {
            //to be able to renew a book it must already be checked out, we need to filter by username and library card #, cant have a status of renewed or returned, due date must be increased by 2 weeks

            XDocument xdoc = XDocument.Load(account);

            var Renewal = xdoc.Descendants("user")
                .SingleOrDefault(x => x.Element("username").Value == Username && x.Element("LibraryCard").Value == txtLibraryCard.Text);

            if(Renewal == null)
            {
                MessageBox.Show("Sorry, we couldn't find a book under this name that's checked out on your account, make sure to checkout the book first!"); 
            }
            else
            {
                Renewingbook(txtTitle.Text, Renewal);
            }
        }

        //this is a function that takes a string and the filter above as parameters and renews the book due date and status of the book
        public void Renewingbook(string title, XElement renewalfilters)
        {
            DateTime date = DateTime.Now;

            //this currently isn't working, says i have multiple copies of the same book 
            var renewingbook = renewalfilters.Descendants("book")
                .SingleOrDefault(x => x.Element("status").Value == bookout && x.Element("BookCheckedOut").Value == title); 

            if(renewingbook == null)
            {
                MessageBox.Show("We either found nothing on this result or you don't have a book checked out");
            }
            else
            {
                renewingbook.Element("DueDate").Value = date.AddDays(42).ToShortDateString();
                renewingbook.Element("status").Value = bookrenewed;

                renewalfilters.Document.Save(account);

                MessageBox.Show("Book has been renewed");
            }
        }

        private void btnReserve_Click(object sender, RoutedEventArgs e)
        {
            //to reserve a book there must be 0 stock on the title of that book left, also have to check the username and library card # of the user that's logged in like usual

            //for reserving a book we want to check the books stock value in the library file, make sure that the title in the text box is the same as the value 

            XDocument doc = XDocument.Load(library);

            var reservation = doc.Descendants("book")
                .SingleOrDefault(x => x.Element("stock").Value == "0" && x.Element("title").Value == txtTitle.Text);

            if(reservation == null)
            {
                MessageBox.Show("This book currently has stock, please checkout the book instead!");
            }
            else
            {
                DateTime date = DateTime.Now;

                XDocument xdoc = XDocument.Load(account);

                var userdetails = xdoc.Descendants("user")
                    .SingleOrDefault(x => x.Element("username").Value == Username && x.Element("LibraryCard").Value == txtLibraryCard.Text);

                if(userdetails == null)
                {
                    MessageBox.Show("These user details couldn't be found, please make sure that you're using the correct details for your account!");
                }
                else
                {
                    userdetails.Element("BooksCheckedOut").Add(
                    new XElement("book",
                        new XElement("BookCheckedOut", txtTitle.Text),
                        new XElement("DateCheckedOut", "Not yet confirmed"),
                        new XElement("DueDate", "Date to be confirmed"),
                        new XElement("status", reserved)));
                }

                userdetails.Document.Save(account);

                MessageBox.Show("Book has been reserved");
            }

         
        }

  
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            BookHomePage home = new BookHomePage(_global);

            home.Show();

            this.Hide();

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataSet dataSet = new DataSet();
            //Reads the XML file into the dataset
            dataSet.ReadXml(library);
            //Sets the datasource for the datagrid to be the dataset
            dtgBooksShowing.ItemsSource = dataSet.Tables[0].DefaultView;

            DataView dv = dataSet.Tables[0].DefaultView;

            StringBuilder sb = new StringBuilder();
            foreach (DataColumn column in dv.Table.Columns)
            {
                sb.AppendFormat("[{0}] Like '%{1}%' OR ", column.ColumnName, txtSearch.Text);
            }
            sb.Remove(sb.Length - 3, 3);
            dv.RowFilter = sb.ToString();
            dtgBooksShowing.ItemsSource = dv;
            dtgBooksShowing.Items.Refresh();
        }
    }
}
