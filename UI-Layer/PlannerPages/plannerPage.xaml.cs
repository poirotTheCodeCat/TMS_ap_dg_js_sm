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
    /// Interaction logic for plannerPage.xaml
    /// </summary>
    public partial class PlannerPage : Page
    {
        private List<Contract> allContracts = new List<Contract>();        // allContracts is used to hold all existing contracts -> can be updated
        private Contract selectedContract = new Contract();
        private List<Contract> selectedContracts = new List<Contract>();        // Will store the list of currently selected contracts for the trip

        private List<Carrier> AllCarriers = new List<Carrier>();
        private List<Order> currentOrderList = new List<Order>();

        private List<string> cities = new List<string>();       // this holds all of the city names

        private Planner planner = new Planner();                            // used to access planner logic


        public PlannerPage()
        {
            InitializeComponent();
            fillLists();            // fill the allContracts list with data from the database
            generateContractData();
        }

        /// <summary>
        /// This fills the contractdata datatable with the displayContracts list
        /// </summary>
        private void generateContractData()
        {
            foreach (Contract c in allContracts)
            {
                ContractsGrid.Items.Add(c);
            }
        }



        /// <summary>
        /// fills the lists with data that is held within the database -> does not access data directly
        /// </summary>
        private void fillLists()
        {
            // get all current orders -> orders where Completed == false
            allContracts = planner.ShowPendingOrders();
        }


        private void updateContracts()
        {

        }

        /// <summary>
        /// This calls a function to process the selected contract
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddContract_Click(object sender, RoutedEventArgs e)
        {
            // insert error checking function


            ContractsGrid.Items.Remove(selectedContract);           // remove the item from the contract datagrid

            TripGrid.Items.Add(selectedContract);
            // move the contract into the second thingy
            // make contract into order
            selectedContract = new Contract();      // reset the selected button

        }

        private void checkGrid()
        {

        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            // planner.createOrder(selectedContracts)
        }

        /*
        /// <summary>
        /// if the FTL button is checked then switch the content presented on the screen to only show FTL contracts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FTLBtn_Checked(object sender, RoutedEventArgs e)
        {
            if(showType == App.LTL)
            {
                showType = App.FTL;
            }
            // call display function
        }

        /// <summary>
        /// if the LTL button is checked then switch the content presented on the screen to only show LTL contracts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LTLBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (showType == App.FTL)
            {
                showType = App.LTL;
            }
            // call display function
        }
        */

        private void CarrierSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ContractsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gridSelection = (DataGrid)sender;
            Contract contract = gridSelection.SelectedItem as Contract;

            if (contract != null)
            {
                selectedContract = contract;
            }
        }

        private void CompleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
