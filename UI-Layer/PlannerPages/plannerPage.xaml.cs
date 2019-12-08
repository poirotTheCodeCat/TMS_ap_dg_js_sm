﻿using System;
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
            

            // add the selected item to the list
            orderContracts.Add(selectedContract);

            // Add contract to orderTabe 
            TripGrid.Items.Add(selectedContract);

            // remove contract from display table 
            ContractsGrid.Items.Remove(selectedContract);

            // reset currently selected contract
            selectedContract = new Contract();

            // if only contract in list -> display carriers of origin city
            if(orderContracts.Count == 1)
            {
                // display the list of cities
                displayCarriers(orderContracts[0].Origin);
            } 
        }

        /// <summary>
        /// This evaluates the current contract and ajusts the UI according to the business rules 
        /// </summary>
        /// <param name="contract"></param>
        public void checkContract(Contract contract)
        {

        }

        /// <summary>
        /// This displays all carriers that belong to a certain city on the screen
        /// </summary>
        /// <param name="city"></param>
        private void displayCarriers(string city)
        {
            List<Carrier> carriers = new Planner().GetCarriers();
            List<Carrier> displayCarriers = new List<Carrier>();
            foreach(Carrier c in carriers)
            {
                if(c.DepotCities.Contains(city))
                {
                    displayCarriers.Add(c);
                }
            }
            foreach(Carrier display in displayCarriers)
            {
                CarrierGrid.Items.Add(display);
            }
        }

        /// <summary>
        /// This button event fires when the submit button is pressed -> it generates an order from whatever is currently in the orderlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            // planner.createOrder(selectedContracts)
            if(!AddBtn.IsEnabled)       // if the Add button has been deactivated re enable it
            {
                AddBtn.IsEnabled = true;
            }
        }

       

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
    }
}
