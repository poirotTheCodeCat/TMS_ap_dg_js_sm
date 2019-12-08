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
        private Contract selectedContract = new Contract();
        private Contract deleteContract = new Contract();

        private List<Contract> allContracts = new List<Contract>();        // allContracts is used to hold all existing contracts -> can be updated
        private List<Contract> orderContracts = new List<Contract>();        // Will store the list of currently selected contracts for the trip

        private List<Carrier> AllCarriers = new List<Carrier>();
        private List<Order> currentOrderList = new List<Order>();


        private Planner planner = new Planner();                            // used to access planner logic
        private List<Carrier> carriersToDisplay = new List<Carrier>();
        private List<Carrier> currCarriers = new List<Carrier>();

        private bool isFTL;


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
            if (checkContract(selectedContract))         // insert error checking function
            {
                // add the selected item to the list
                orderContracts.Add(selectedContract);

                // Add contract to orderTabe 
                TripGrid.Items.Add(selectedContract);

                // remove contract from display table 
                ContractsGrid.Items.Remove(selectedContract);

                // reset currently selected contract
                selectedContract = new Contract();

                // if only contract in list -> display carriers of origin city
                if (orderContracts.Count == 1)
                {
                    // display the list of cities
                    displayCarriers(orderContracts[0].Origin);
                }
            }
        }


        /// <summary>
        /// This evaluates the current contract and ajusts the UI according to the business rules 
        /// </summary>
        /// <param name="contract"></param>
        public bool checkContract(Contract contract)
        {
            // if the contract is an FTL -> clear the list of contracts in the Contract GridView
            if (contract.JobType == 0)
            {
                // set isFTL = true
                isFTL = true;
                ContractsGrid.Items.Clear();
            }
            return true;
        }


        /// <summary>
        /// This displays all carriers that belong to a certain city on the screen
        /// </summary>
        /// <param name="city"></param>
        private void displayCarriers(string city)
        {
            List<Carrier> carriers = new Planner().GetCarriers();
            string displayString = "";

            foreach(Carrier c in carriers)
            {
                if(c.DepotCities.Contains(city))
                {
                    displayString = c.CarrierName + " || " + c.FtlAvail + " || " + c.LtlAvail + " || " + c.FtlRate + " || " + c.LtlAvail;
                    citySelect.Items.Add(displayString);
                    currCarriers.Add(c);
                }
            }
        }

        /// <summary>
        /// This button event fires when the submit button is pressed -> it generates an order from whatever is currently in the orderlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            // check the orderlist vs the carrier LTL/FTL rate
            // if(confirmContract())

            

            if(!AddBtn.IsEnabled)       // if the Add button has been deactivated re enable it
            {
                AddBtn.IsEnabled = true;
            }

            // refresh everything!!!
            // refresh all lists 
            ContractsGrid.Items.Clear();
            generateContractData();

            selectedContract = new Contract();

            currentOrderList.Clear();       // clear the current order list

            currCarriers.Clear();
        }


        private bool confirmContract()
        {
            if(orderContracts.Count == 0)
            { 
                return false;
            }

            if(isFTL)       // if the order is marked as an ftl or is comprised of multiple ltls
            {
                if(currCarriers[0].FtlAvail >= 1)
                { 
                    return true; 
                }
                else
                {
                    Error.Content = "FTL quantity not available";
                }
            }
            else
            {
                if(isFTL == false)
                {
                    int neededLTL = 0;
                    int availLTL = 0;
                    foreach(Contract c in orderContracts)
                    {
                        neededLTL += c.Quantity;
                    }

                    foreach(Carrier carr in currCarriers)
                    {
                        availLTL += carr.LtlAvail;
                    }

                    if(neededLTL <= availLTL)
                    {
                        return true;
                    }

                    else
                    {
                        Error.Content = "LTL quantity not available";
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// This will remove the selected item from this datagrid and the list of carriers being used
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarrierSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid selection = (DataGrid)sender;
            Carrier removeCarrier = selection.SelectedItem as Carrier;

            citySelect.Items.Remove(removeCarrier);     // remove the carrier from the datagrid
            currCarriers.Remove(removeCarrier);     // remove the carrier from the list of current carriers

            string addString = removeCarrier.CarrierName + " || " + removeCarrier.FtlAvail + " || " + removeCarrier.LtlAvail + " || " + removeCarrier.FtlRate + " || " + removeCarrier.LtlAvail;

            citySelect.Items.Add(addString);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            if (deleteContract != null)
            {
                // remove from ordertable
                TripGrid.Items.Remove(deleteContract);

                // add to display table
                ContractsGrid.Items.Add(deleteContract);

                // remove from orderList
                orderContracts.Remove(deleteContract);

                // reset deleteContact
                deleteContract = new Contract();

                //if(list is empty or null) -> clear carriers list and display
                if(orderContracts.Count == 0)
                {
                    CarrierGrid.Items.Clear();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TripGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gridSelection = (DataGrid)sender;
            Contract contractToDelete = gridSelection.SelectedItem as Contract;

            if (contractToDelete != null)
            {
                deleteContract = contractToDelete;
            }
        }

        private void GenerateInvoiceBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void citySelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;       
            int index = comboBox.SelectedIndex;         // get the index of the selected combobox
            if (index >= 0)
            {
                Carrier carrier = currCarriers[index];      // get the current carrier 


                CarrierGrid.Items.Add(carrier);             // add the carrier 
                currCarriers.Add(carrier);                  // add to the list of carriers to send to 

                string displayString = carrier.CarrierName + " || " + carrier.FtlAvail + " || " + carrier.LtlAvail + " || " + carrier.FtlRate + " || " + carrier.LtlAvail;

                citySelect.Items.Remove(displayString);     // remove the selected carrier from the combobox
            }
        }

        /// <summary>
        /// When the user selects an element from the datagrid -> delete from the datagrid and add back to the dropdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarrierGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
