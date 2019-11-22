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
    /// Interaction logic for BuyerContracts.xaml
    /// </summary>
    public partial class BuyerContracts : Page
    {
        public BuyerContracts()
        {
            InitializeComponent();
            ThreadStart ts = new ThreadStart(refreshTimer);
            Thread orderThread = new Thread(ts);
            orderThread.Start();
        }

        /// <summary>
        /// This method runs as a thread which activates every 10 seconds.
        /// When activated the method calls the refreshOrders function to refresh the orders that are displayed
        /// in the grid
        /// </summary>
        private void refreshTimer()
        {
            refreshContracts();            // refresh orders displayed
            Thread.Sleep(10000);        // wait 10 seonds for the next refresh
        }

        /// <summary>
        /// This function will call a method from a class within the Business layer to retrieve the most up to date contracts
        /// This will then display these contracts in the datagrid
        /// </summary>
        public void refreshContracts()
        {

        }

        /// <summary>
        /// This event will fire when the user selects something on the datagrid. It will record the current row in a local variable which
        /// will be used to generate oreders from the contract marketplace
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContractsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
