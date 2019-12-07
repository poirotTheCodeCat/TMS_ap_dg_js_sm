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

namespace TMS
{
    /// <summary>
    /// Interaction logic for BuyerPage.xaml
    /// </summary>
    public partial class BuyerPage : Page
    {
        List<Contract> displayContracts;
        Buyer buyer = new Buyer();
        Contract selectedContract = new Contract();

        public BuyerPage()
        {
            InitializeComponent();
            // call function that will fill the datagrid
            fillList();
            // create thread that automatically refreshes datagrid for new contracts from the marketplace
            fillGrid();
        }

        /// <summary>
        /// This event handler will go to the create order page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedContract != null)
            {
                buyer.AddContract(selectedContract);
            }
        }

        /// <summary>
        /// This refreshes the datagrid with the most up to date contracts from the marketplace
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            // call refresh function
            refreshContracts();
        }

        /// <summary>
        /// This method calls upon other local private methods to refresh the list with the most up to date information within the datagrid
        /// then fills the datagrid with the updated data
        /// </summary>
        private void refreshContracts()
        {
            // call function from buyer to retrieve all contracts and store it in a list of contracts
            fillList();
            // iterate through the contracts list and add each item to the datagrid
            fillGrid();

        }

        /// <summary>
        /// retrieves the complete list of contracts from the marketplace
        /// </summary>
        private void fillList()
        {
            displayContracts = buyer.GetMarketplaceContracts();
        }

        /// <summary>
        /// Fills the datagrid with contracts from the contract marketplace - stored in a local data member list<>
        /// </summary>
        private void fillGrid()
        {
            foreach (Contract c in displayContracts)
            {
                ContractGrid.Items.Add(c);
            }
        }

        /// <summary>
        /// Event handler that checks the element that is currently selected and sets the local data member equal 
        /// responsible for tracking the selection to the selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContractGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // set current item = sender
            DataGrid gridSelection = (DataGrid)sender;
            Contract newItem = gridSelection.SelectedItem as Contract;

            if (newItem != null)
            {
                selectedContract = newItem;
            }
            CreateBtn.IsEnabled = true;
        }
    }
}
