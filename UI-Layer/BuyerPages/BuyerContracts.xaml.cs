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
    /// Interaction logic for BuyerContracts.xaml
    /// </summary>
    public partial class BuyerContracts : Page
    {
        private List<Contract> contractsToDisplay = new List<Contract>();
        private Contract selectedContract = new Contract();
        private Buyer buyer = new Buyer();

        public BuyerContracts()
        {
            InitializeComponent();
            fillContractList();
            fillContractData();
        }

        private void fillContractList()
        {
            // contractsToDisplay = buyer.getContracts();
        }

        private void fillContractData()
        {
            foreach(Contract c in contractsToDisplay)
            {
                ContractData.Items.Add(c);
            }
        }

        /// <summary>
        /// This event will fire when the user selects something on the datagrid. It will record the current row in a local variable which
        /// will be used to generate oreders from the contract marketplace
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContractsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gridSelection = (DataGrid)sender;
            Contract newContract = gridSelection.SelectedItem as Contract;

            if(newContract == null)
            {
                return;
            }

            selectedContract = newContract;
        }

        /// <summary>
        /// On this click the buyer will attempt to send the contract to be added to the database
        /// Note that once it is in the database it will be marked to be completed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RequestOrder_Click(object sender, RoutedEventArgs e)
        {
            if(selectedContract != null)
            {
                //buyer.addContract(selectedContract);
            }
        }
    }
}
