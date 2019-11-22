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
    /// Interaction logic for ActiveOrdersPage.xaml
    /// </summary>
    public partial class ActiveOrdersPage : Page
    {
        public ActiveOrdersPage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContractsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// This method will be activated every 10 seconds. When it fires off, it will call a local method
        /// which will reload the datagrid with the most up to date contracts
        /// </summary>
        private void refreshTimer()
        {

        }

        /// <summary>
        /// This method will call a function from the business layer to retrieve the most up to date order information. 
        /// It will then display this information on the datagrid within the page
        /// </summary>
        public void refreshOrders()
        {

        }
    }
}
