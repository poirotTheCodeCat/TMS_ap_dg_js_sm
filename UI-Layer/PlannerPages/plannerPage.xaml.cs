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
using TMS.Business_Layer.users;

namespace TMS
{
    /// <summary>
    /// Interaction logic for plannerPage.xaml
    /// </summary>
    public partial class PlannerPage : Page
    {
        private DateTime perceivedTime = new DateTime();
        private DateTime currTime = DateTime.Now;

        private Contract selectedContract = new Contract();
        private Contract deleteContract = new Contract();
        private Contract contractToComplete = new Contract();               

        private List<Contract> allContracts = new List<Contract>();        // allContracts is used to hold all existing contracts -> can be updated
        private List<Contract> orderContracts = new List<Contract>();        // Will store the list of currently selected contracts for the trip

        private List<Carrier> AllCarriers = new List<Carrier>();            // all carriers in database
        private List<Order> currentOrderList = new List<Order>();           // holds all orders selected for creating order


        private Planner planner = new Planner();                            // used to access planner logic
        private List<Carrier> carriersToDisplay = new List<Carrier>();
        private List<Carrier> currCarriers = new List<Carrier>();
        private List<Contract> activeOrders = new List<Contract>();
        private bool multipleCarriers = true;
        private bool multipleLTL = false;


        public PlannerPage()
        {
            InitializeComponent();
            try
            {
                fillLists();            // fill the allContracts list with data from the database
                generateContractData();
                perceivedTime = DateTime.Now;
            }
            catch(Exception e)
            {
                Logger.Log("TMS database connection error\n" + e);
                MessageBox.Show("Unable to connect IP or Port information");
            }
        }

        //                                      Methods 
        // **********************************************************************************************
        // **********************************************************************************************

        /// <summary>
        /// This fills the contractdata datatable with the displayContracts list
        /// </summary>
        private void generateContractData()
        {
            foreach (Contract c in allContracts)
            {
                if(c.PlannerConfirmed == 0 && !(c.EndTime.HasValue))
                {
                    ContractsGrid.Items.Add(c);
                }
                else if(c.PlannerConfirmed != 1)
                {
                    CurrentOrderGrid.Items.Add(c);
                }
            }
        }



        /// <summary>
        /// fills the lists with data that is held within the database -> does not access data directly
        /// </summary>
        private void fillLists()
        {
            // get all current orders -> orders where Completed == false
            allContracts = planner.ShowAllContracts();
        }

        /// <summary>
        /// This is called to reset everything on the page -> called when a new order is created
        /// </summary>
        private void refreshData()
        {
            // clear all lists
            allContracts.Clear();
            currCarriers.Clear();
            orderContracts.Clear();
            currentOrderList.Clear();
            ContractsGrid.Items.Clear();
            TripGrid.Items.Clear();
            CarrierGrid.Items.Clear();
            carrierSelect.Items.Clear();
            CurrentOrderGrid.Items.Clear();

            fillLists();
            generateContractData();
        }

        /// <summary>
        /// This calls a function to process the selected contract
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddContract_Click(object sender, RoutedEventArgs e)
        {
            ProcessContractSelection(selectedContract);
            // if only contract in list -> display carriers of origin city
            if (orderContracts.Count == 1)
            {
                // display the list of cities
                if(!carrierSelect.HasItems)
                {
                    displayCarriers(orderContracts[0].Origin);
                }
            }
            
        }


        /// <summary>
        /// This evaluates the current contract and ajusts the UI according to the business rules 
        /// </summary>
        /// <param name="contract"></param>
        public void ProcessContractSelection(Contract contract)
        {
            // check if it is ftl
            // check for other items in orderContract list
            if(contract.JobType == 0 || orderContracts.Count > 0)
            {
                if (contract.JobType == 1)        // if there are already items in the order list then we cannot add an FTL
                {
                    if (!planner.CheckGroupedContracts(orderContracts, contract))
                    {
                        Error.Content = "Cannot add LTL to Order";
                        return;
                    }
                }
                else
                {
                    ContractsGrid.IsEnabled = false;        // do not allow user to add more
                    if (currCarriers.Count >= 1)
                    {
                        Error.Content = "You cannot combine FTL Contracts with other Contracts";
                        return;
                    }
                }
            }
            orderContracts.Add(contract);
            TripGrid.Items.Add(contract);
            ContractsGrid.Items.Remove(selectedContract);
            multipleCarriers = false;
            multipleLTL = false;
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
                    carrierSelect.Items.Add(displayString);
                    carriersToDisplay.Add(c);
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
            // planner.CreateOrder(orderContracts, currCarriers);
            if(currCarriers.Count == 0)
            {
                Error.Content = "You must enter a Carrier";
                return;
            }

            planner.CreateOrder(orderContracts, currCarriers);

            if (!AddBtn.IsEnabled)       // if the Add button has been deactivated re enable it
            {
                AddBtn.IsEnabled = true;
            }

            // refresh everything!!!
            // refresh all lists 
            refreshData();
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
            if(removeCarrier != null)
            {
                string addString = removeCarrier.CarrierName + " || " + removeCarrier.FtlAvail + " || " + removeCarrier.LtlAvail + " || " + removeCarrier.FtlRate + " || " + removeCarrier.LtlAvail;

                CarrierGrid.Items.Remove(removeCarrier);     // remove the carrier from the datagrid
                currCarriers.Remove(removeCarrier);     // remove the carrier from the list of current carriers

                carrierSelect.Items.Add(addString);
            }
            
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if(contractToComplete != null)
            {
                // check if the order is actually complete
                if (contractToComplete.EndTime <= perceivedTime)
                {
                    planner.ConfirmOrder(contractToComplete);
                }
                
            }
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
                    carrierSelect.Items.Clear();
                }

                if(!ContractsGrid.IsEnabled)
                {
                    ContractsGrid.IsEnabled = true;
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
        private void carrier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;       
            int index = comboBox.SelectedIndex;         // get the index of the selected combobox
            if (index >= 0)
            {
                Carrier carrier = carriersToDisplay[index];      // get the current carrier 

                if(((orderContracts[0].JobType == 1) && carrier.LtlAvail > 0) || ((orderContracts[0].JobType == 0) && carrier.FtlAvail > 0))
                {
                    CarrierGrid.Items.Add(carrier);             // add the carrier 
                    currCarriers.Add(carrier);                  // add to the list of carriers to send to 

                    string displayString = carrier.CarrierName + " || " + carrier.FtlAvail + " || " + carrier.LtlAvail + " || " + carrier.FtlRate + " || " + carrier.LtlAvail;

                    carrierSelect.Items.Remove(displayString);     // remove the selected carrier from the combobox
                    Error.Content = "";
                }
                else
                {
                    Error.Content = "This Carrier has no availability";
                }
               
            }
        }

        /// <summary>
        /// When the user selects an element from the datagrid -> delete from the datagrid and add back to the dropdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarrierGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gridSelection = (DataGrid)sender;
            Contract contract = gridSelection.SelectedItem as Contract;

            if(contract != null)
            {
                contractToComplete = contract;
            }
        }

        /// <summary>
        /// This button will simulate a day moving forward and will check against all current orders on the go to 
        /// mark for completion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SimulateDayBtn_Click(object sender, RoutedEventArgs e)
        {
            perceivedTime = perceivedTime.AddDays(1);
            foreach(Contract contract in allContracts)      
            {
                if((contract.PlannerConfirmed == 0) && (contract.EndTime.HasValue))
                {
                    if(contract.EndTime <= perceivedTime)       // check if the order has finished
                    {
                        markComplete(contract);         // Hulk it out
                    }
                }
            }
        }

        /// <summary>
        ///  This indicates the complete status of an order as complete by changing the row color to green within the dataGrid in 
        ///  which it is stored
        /// </summary>
        /// <param name="contract"></param>
        private void markComplete(Contract contract)
        {
            List<Contract> temp = planner.ShowAllContracts();
            CurrentOrderGrid.Items.Remove(contract);
            
            contract.Status = "COMPLETE";

            CurrentOrderGrid.Items.Add(contract);
        }

        private void CurrentOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gridSelection = (DataGrid)sender;
            Contract contract = gridSelection.SelectedItem as Contract;
            if(contract != null)
            {
                if (string.Equals(contract.Status, "COMPLETE"))
                {
                    contract.PlannerConfirmed = 1;
                    contract.UpdateContract();
                    CurrentOrderGrid.Items.Remove(contract);
                }
            }
        }
    }
}
