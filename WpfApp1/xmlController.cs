﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace WpfApp1
{
    public class xmlController
    {

        string path = "Library.xml";

        string paths = "AccountDetails.xml";

        public void CheckoutBook()
        {

            XmlDocument doc = new XmlDocument(); 

            doc.Load(path);

            //needs to update hte account details xml file on books checked out by adding only the title of the book

            //message box saying that the book has been checked out 




        }

        public void AddBook(Books newbook)
        {
            XmlDocument doc = new XmlDocument();

            doc.Load(path);

            XmlNode ROOT = doc.SelectSingleNode("/library");
            XmlNode book = doc.CreateElement("book");
            XmlNode title = doc.CreateElement("title");
            XmlNode author = doc.CreateElement("author");
            XmlNode year = doc.CreateElement("year");
            XmlNode publisher = doc.CreateElement("publisher");
            XmlNode isbn = doc.CreateElement("isbn");
            XmlNode category = doc.CreateElement("category");

            title.InnerText = newbook.title;
            author.InnerText = newbook.author;
            year.InnerText = newbook.year;
            publisher.InnerText = newbook.publisher;
            isbn.InnerText = newbook.isbn;
            category.InnerText = newbook.category;

            book.AppendChild(title);
            book.AppendChild(author);
            book.AppendChild(year);
            book.AppendChild(publisher);
            book.AppendChild(isbn);
            book.AppendChild(category);

            ROOT.AppendChild(book);
            doc.Save(path);
        }

        public void UpdateBook(string title, Books updatebook)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNode oldVideo = doc.SelectSingleNode("//book[title='" + title + "']"); 
            oldVideo.ChildNodes.Item(0).InnerText = updatebook.title;
            oldVideo.ChildNodes.Item(1).InnerText = updatebook.author;
            oldVideo.ChildNodes.Item(2).InnerText = updatebook.year;
            oldVideo.ChildNodes.Item(3).InnerText = updatebook.publisher;
            oldVideo.ChildNodes.Item(4).InnerText = updatebook.isbn;
            oldVideo.ChildNodes.Item(5).InnerText = updatebook.category;
            doc.Save(path);
        }

        public void BookDelete(string title)
        {
            XmlDocument doc = new XmlDocument();

            doc.Load(path);
            XmlNode nodes = doc.SelectSingleNode("//book[title='" + title + "']");

            nodes.ParentNode.RemoveChild(nodes);

            doc.Save(path);
        }

        public void MemberDelete(string username)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(paths);
            XmlNode nodes = doc.SelectSingleNode("//user[username='" +username+ "']");
            
            nodes.ParentNode.RemoveChild(nodes);
            
            doc.Save(paths);
        }

        //this currently only adds the book to the top username, not the one you're actually logged into
        public void CheckingOutBook(string title, Accounts member)
        {
            

            XmlDocument doc = new XmlDocument();

            //loadign account details
            doc.Load(paths);

            //we want to search the account xml file from the global variable that's stored in the accounts class

            XmlNode checking = doc.SelectSingleNode("//user");
            XmlNode CheckedOut = doc.CreateElement("bookscheckedout");
            XmlNode NumofBooks = doc.CreateElement("numberofbookscheckedout");
            XmlNode DueDate = doc.CreateElement("DueDate");

            //this currently only allows a user to check out one book at a time
            checking.ChildNodes.Item(5).InnerText = title;
            checking.ChildNodes.Item(7).InnerText = DateTime.Now.AddDays(14).ToString();

            //we have to check that the library card number that was entered into the text box is the same as in the file
            


            doc.Save(paths);
            
        }

        public void AddingMember(Accounts member)
        {
            //load the file 

            XDocument loading = XDocument.Load(paths);

            Accounts account = new Accounts();

           /* account.username = txtUsernameSignup.Text;
            account.password = txtPasswordSignup.Text;
            account.email = txtEmail.Text;
            account.PhoneNumber = txtPhoneNumber.Text;
            account.librarycard = randomise;
            account.bookscheckedout = "none";
            account.numberofbookscheckedout = "0";
            account.duedate = DateTime.Now; */

            loading.Element("accounts").Add(new XElement("user",
                new XElement("username", member.username),
                new XElement("password", member.password),
                new XElement("email", member.email),
                new XElement("PhoneNumber", member.PhoneNumber),
                new XElement("LibraryCard", member.librarycard),
                new XElement("numberofbookscheckedout", member.numberofbookscheckedout),
                new XElement("bookscheckedout", member.bookscheckedout),
                new XElement("duedate", member.duedate)));

            loading.Save(paths); 

        }


    }
}
