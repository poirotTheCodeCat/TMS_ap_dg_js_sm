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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TMS_ap_dg_js_sm
{
    /// <summary>
    /// Interaction logic for CompleteOrders.xaml
    /// </summary>
    public partial class CompleteOrders : Page
    {
        public CompleteOrders()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This event handler fires off when the user selects something on the datagrid. Once this happens, it will fill in  a local
        /// data member to store the information to be used in further UI processing 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompletedOrderGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// This event handler calls a local method which will retrieve up to date orders information from the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// This button will call a local method that will generate a report of all up to date Completed orders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Content = new CreateOrderPage();
        }
    }
}
