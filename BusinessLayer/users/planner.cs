﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Business_Layer.users;

namespace TMS
{
    /// \class Planner
    /// <summary>
    /// Represents a Planner user object for the TMS system. The Planner makes use of the 
    /// LocalComm class to update information in the TMS Database. It also uses objects in the
    /// Order and Trip classes. 
    /// </summary>
    public class Planner
    {
        private int PalletThreshold { get; set; }
        //private Order workingOrder;
        //private bool pendingOrders;



           public Planner()
        {
            PalletThreshold = 17;
        }

        /// <summary>
        /// This method gets a Carrier to view the details of that Carrier. 
        /// </summary>
        /// <param name="searchItem">The identifier for the Carrier that will be returned</param>
        /// <returns>Carrier that is requested.</returns>
        public List<Carrier> GetCarriers()
        {
            List<Carrier> carriers = new LocalComm().GetCarriers();

            return carriers;
        }

        /// <summary>
        /// This method updates the availability of the Carrier based on the job type being requested. 
        /// </summary>
        /// <param name="carrier">The identifier for the Carrier that will be returned</param>
        /// <param name="jobType">The identifier for the Carrier that will be returned</param>
        /// <param name="quantityPallets">The LTL load being requested by the Contract(s)</param>
        /// <returns>Int representing the remaining load for a Contract</returns>
        public int UpdateCarrierAvailability(Carrier carrier, int jobType=0, int quantityPallets=0)
        {
            int remainingLoad = 0;

            // Assuming that the UI will only show Carriers with availability of the job type of the Contract(s) in the cart 
            // If the job type is an FTL, there must be available trucks 
            if (jobType == 0)
            {
                carrier.FtlAvail -= 1;
                new LocalComm().UpdateCarrierFTL(carrier);
            }
            else
            {
                if (carrier.LtlAvail < quantityPallets)
                {
                    remainingLoad = quantityPallets - carrier.LtlAvail;
                    
                        carrier.LtlAvail = 0;
                        new LocalComm().UpdateCarrierLTL(carrier);
                    
                    
                }
                else
                {
                    carrier.LtlAvail = quantityPallets;
                    new LocalComm().UpdateCarrierLTL(carrier);
                }
            }

            return remainingLoad;
        }
        /// <summary>
        /// This method allows the Planner to create a Trip that will be added to an Order.
        /// </summary>
        /// <param name="searchItem1">An identifier for the Order the Trip is required for</param>
        /// <param name="searchItem2">An identifier for the Carrier completing the Trip</param>
        /// <returns>List of trips that were created.</returns>
        public List<Trip> CreateTrip(string searchItem1, string searchItem2)
        {
            List<Trip> trip = new List<Trip>();

            return trip;
        }

        /// <summary>
        /// This method allows the Planner to mark an Order as approved.
        /// </summary>
        /// <param name="searchItem">The identifier for the Order that needs to be marked 
        ///                         approved</param>
        /// <returns>Int representing an Order was successfully marked approved.</returns>
        public int ApproveOrder(string searchItem)
        {
            int done = 1;
           
            return done;
        }

        /// <summary>
        /// This method allows the Planner to mark and Order as completed.
        /// </summary>
        /// <param name="searchItem">The identifier for the Order that needs to be marked 
        ///                         completed</param>
        /// <returns>Int representing an Order was successfully marked completed.</returns>
        public int CompleteOrder(string searchItem)//Again....Possibly orderID??
        { 
            int done = 1;

            return done;
        }

        /// <summary>
        /// This method allows the Planner to view all Orders.
        /// </summary>
        /// <returns>List of active Orders.</returns>
        public List<Order> ShowActiveOrders()
        {
            List<Order> activeOrderList = new List<Order>();

            return activeOrderList;
        }

        /// <summary>
        /// This method allows the Planner to view all pending Orders.
        /// </summary>
        /// <returns>List of pending Orders.</returns>
        public List<Contract> ShowPendingOrders()
        {
            List<Contract> pendingOrderList = new List<Contract>();

            pendingOrderList = new LocalComm().GetPendingContracts();

            return pendingOrderList;
        }

        /// <summary>
        /// This method allows the Planner to generate an invoice summary of all Orders. 
        /// </summary>
        /// <param name="searchItem">The identifier for the invoices that need to be included
        ///                         in the invoice summary</param>
        /// <returns>Int representing an invoice was successfully generated</returns>
        public int GenerateInvoiceSum(string searchItem) // 2 weeks or of all time
        {
            int done = 1;

            return done;
        }

        /// <summary>
        /// This method checks whether the Contract selected can be added to the 
        /// Order being created based on the origin city, job type, and van type.
        /// </summary>
        /// <param name="contracts">The identifier for the Contracts currently added
        ///                         to the order being created.</param>
        /// <param name="contractToAdd">The identifier for the Contract requesting 
        ///                         to be added to the order being created.</param>                         
        /// <returns>Bool representing whether the Contract can be added to the Order</returns>
        public bool CheckGroupedContracts(List <Contract> contracts, Contract contractToAdd)
        {
            if((contractToAdd.Origin == contracts[0].Origin) && (contractToAdd.JobType == 1) && (contractToAdd.VanType == contracts[0].VanType))
            {
                return true;
            }
            else
            {
                return false; 
            }
        }


        /// <summary>
        /// This method checks whether the Contract selected can be added to the 
        /// Order being created based on the origin city, job type, and van type.
        /// </summary>
        /// <param name="listOfContracts">The identifier for the Contracts currently added
        ///                         to the order being created.</param>                        
        /// <returns>Bool representing whether the Contract can be added to the Order</returns>
        public bool CheckOrder(List<Contract> listOfContracts)
        {
            int totalQuantity = 0;


            foreach(Contract c in listOfContracts)
            {
                totalQuantity += c.Quantity;
            }
            if(totalQuantity < PalletThreshold)
            {
                return false;
            }
            else
            {
                return true;
            }


        }

        /// <summary>
        /// This method checks whether the Contract selected can be added to the 
        /// Order being created based on the origin city, job type, and van type.
        /// </summary>
        /// <param name="listOfContracts">The identifier for the Contracts currently added
        ///                         to the order being created.</param>                        
        /// <returns>Bool representing whether the Contract can be added to the Order</returns>
        public void AddPendingOrder(Contract pendingContract)
        {
            new LocalComm().AddContract(pendingContract);
        }
    }
}
