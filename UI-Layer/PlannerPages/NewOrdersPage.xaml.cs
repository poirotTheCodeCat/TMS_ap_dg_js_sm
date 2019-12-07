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

namespace TMS
{
    /// <summary>
    /// Interaction logic for NewOrdersPage.xaml
    /// </summary>
    public partial class NewOrdersPage : Page
    {
        public NewOrdersPage()
        {
            InitializeComponent();
            refreshOrders();
        }


        /// <summary>
        /// This method calls an external method within the business logic layer, which will return a list of orders which will then be used
        /// to populate the datagrid with the most up to date orders that are marked for completion
        /// </summary>
        public void refreshOrders()
        {
            ContractsGrid.Items.Clear();
            var pending = new Planner().ShowPendingOrders();

            foreach (var p in pending)
            {
                ContractsGrid.Items.Add(p);
            }
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
