using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Accounts
    {

        public String username { get; set; }

        public String password { get; set; }

        public String email { get; set; }

        public String PhoneNumber { get; set; }

        public String librarycard { get; set; }

        public List<CheckedOutBooks> bookscheckedout { get; set; }

        public int numberofbookscheckedout => bookscheckedout.Count;




    }

    public class CheckedOutBooks
    {
        public String Title { get; set; }

        public string duebackdate { get; set; }

        public string checkedoutdate { get; set; }

    }
}
