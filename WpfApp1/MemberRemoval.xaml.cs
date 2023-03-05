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
    /// Interaction logic for MemberRemoval.xaml
    /// </summary>
    public partial class MemberRemoval : Window
    {
        public MemberRemoval()
        {
            InitializeComponent();

            DataSet data = new DataSet();

            data.ReadXml(@"AccountDetails.xml");

            dtgMemberDetails.ItemsSource = data.Tables[0].DefaultView;


        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            MemberRemoval remove = new MemberRemoval();

            remove.Show();

            this.Close();
        }

        private void btnDeleteMember_Click(object sender, RoutedEventArgs e)
        {
            xmlController uses = new xmlController(); 

            uses.MemberDelete(txtMemberUsername.Text);

            MessageBox.Show("Member deleted");
        }
    }
}
