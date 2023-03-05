using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Accounts
    {

        public string username { get; set; }

        public string password { get; set; }

        public string email { get; set; }

        public string PhoneNumber { get; set; }

        public string librarycard { get; set; }

        public string numberofbookscheckedout { get; set; }
        
        public string bookscheckedout { get; set; }

        public DateTime duedate { get; set; }

        



        public static Accounts activeUser = new Accounts();

    }
}
