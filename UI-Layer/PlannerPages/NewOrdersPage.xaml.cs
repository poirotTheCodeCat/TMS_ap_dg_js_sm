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
using System.Threading;

namespace TMS_ap_dg_js_sm
{
    /// <summary>
    /// Interaction logic for NewOrdersPage.xaml
    /// </summary>
    public partial class NewOrdersPage : Page
    {
        public NewOrdersPage()
        {
            InitializeComponent();
            ThreadStart ts = new ThreadStart(refreshTimer);
            Thread ordersThread = new Thread(ts);
            ordersThread.Start();
        }

        /// <summary>
        /// The refresh timer is to be set as a thread that fires every 10 seconds. When it fires, it calls the refreshOrders() method.
        /// This should automatically refresh the page every 10 seconds (or 10000 milliseconds)
        /// </summary>
        private void refreshTimer()
        {
            refreshOrders();
            Thread.Sleep(10000);        // need to put hardcoded value into external source to be accessed
        }

        /// <summary>
        /// This method calls an external method within the business logic layer, which will return a list of orders which will then be used
        /// to populate the datagrid with the most up to date orders that are marked for completion
        /// </summary>
        public void refreshOrders()
        {

        }

        /// <summary>
        /// This event fires when the user selects something in the datagrid. It loads a local data member with the information displayed 
        /// in the datagrid for use in generating an order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContractsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// This is a button click event that calls on the refreshOrders() method, which will load the most current list of orders onto the screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            refreshOrders();            // call the refreshOrders method
        }
    }
}
