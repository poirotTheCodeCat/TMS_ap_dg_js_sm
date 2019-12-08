using System;
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
        private List<TransportCorridor> transportCorridors = new List<TransportCorridor>();
        
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
            foreach(Carrier c in carriers)
            {
                c.DepotCities = new LocalComm().GetCarrierCities(c.CarrierID);
            }

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
            }
            else
            {
                if (carrier.LtlAvail < quantityPallets)
                {
                    remainingLoad = quantityPallets - carrier.LtlAvail;
                    
                        carrier.LtlAvail = 0;
                    
                    
                }
                else
                {
                    carrier.LtlAvail = quantityPallets;
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
            List<Contract> allContracts = new LocalComm().GetLocalContracts();
            //pendingOrderList = new LocalComm().GetPendingContracts();

            foreach(Contract c in allContracts)
            {
                if(c.PlannerConfirmed == 0 && c.EndTime == null)
                {
                    pendingOrderList.Add(c);
                }
            }

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

        
        double GetClientCharge(Contract contract, List<Carrier> orderCarriers, List<Carrier> originalCarriers)
        {
            double distance = CalculateTime(contract);
            double price = 0;
            double dailyCharge = 150;
            int daysTravelled = 0;
            int pallets = 0;

            double d = distance;
            while(d > 24)
            {
                d -= 24;
                ++daysTravelled;
            }
            // This means only one Carrier
            if(contract.JobType == 1)
            {
                contract.Price = (distance * orderCarriers[0].FtlRate) + (daysTravelled * dailyCharge);
            }
            else
            {
                // Finds the number of pallets carried by each carrier, bills appropriately 
                foreach(Carrier orderC in orderCarriers)
                {
                    foreach(Carrier origC in originalCarriers)
                    {
                        if(origC.CarrierID == orderC.CarrierID)
                        {
                            pallets = origC.LtlAvail - orderC.LtlAvail;
                            contract.Price += pallets * distance * orderC.LtlRate;
                        }
                    }
                }

                contract.Price += daysTravelled * dailyCharge;
            }
            return price;
        }
        

        double GetBreakevenCharge(List<Contract> contracts, List<Carrier> orderCarriers, List<Carrier> originalCarriers)
        {
            int jobType = contracts[0].JobType;
            double charge = 0.00;
            if (jobType == 0)
            {
            }

            return charge;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contracts"></param>
        /// <param name="orderCarriers"></param>
        void CreateOrder(List<Contract> contracts, List<Carrier> carriers, List<Carrier> originalCarriers)
        {
            // 1. Create a trip for each carrier and contract 
            // 2. Generate the price to be used for invoice generation 
            // 3. Add order lines for customer id, end time, status (for planner confirmation), price)
      
            // Add all carriers to each contract 
            foreach(Carrier carr in carriers)
            {
                // Update Carrier availability 
                if(contracts[0].JobType == 0)
                {
                    new LocalComm().UpdateCarrierFTL(carr);
                }
                else
                {
                    new LocalComm().UpdateCarrierLTL(carr);
                }

                // Create a trip for each carrier and Contract 
                // | CarrierID | Contract ID |  
                foreach (Contract con in contracts)
                {
                    new LocalComm().AddTrip(con.ContractID, carr.CarrierID);
                }
            }

            DateTime startTime = DateTime.Now;
            // BuyerSelected = 1 
            // PlannerSelected = 0
            // EndTime != null 
            foreach(Contract con in contracts)
            {
                con.Price = GetClientCharge(con, carriers, originalCarriers);
                con.EndTime = startTime.AddHours(CalculateTime(con));
                con.UpdateContract();
            }

        }

        /// <summary>
        /// Calculates the total time needed to complete the trip in hours
        /// </summary>
        /// <param name="startCity"></param>
        /// <param name="endCity"></param>
        /// <param name="jobType"></param>
        /// <returns></returns>
        public double CalculateTime(Contract contract)
        {

            int originIndex = -1;
            int DestIndex = -1;
            int daysAdded = 0;
            double layoverTime = 2;
            double restTime = 0;
            double totalTime = 0;
            double driveTime = 0;


            foreach (TransportCorridor t in transportCorridors)
            {
                // find the index of the start city
                // find the index of the second city
                if (contract.Origin == t.CityName)        // check if the city is the origin city
                {
                    originIndex = transportCorridors.IndexOf(t);
                }
                if (contract.Destination == t.CityName)          // check if the city is the destination city
                {
                    DestIndex = transportCorridors.IndexOf(t);
                }
                if (originIndex != -1 && DestIndex != -1)
                {
                    break;
                }
            }

            if (originIndex < DestIndex)     // if the destination is west
            {
                for (int i = originIndex; i <= DestIndex; i++)
                {
                    // Add two hours for each intermediate city if LTL, or if loading/unloading for FTL and LTL
                    if (contract.Origin == transportCorridors[i].CityName || contract.Origin == transportCorridors[i].CityName
                        || contract.JobType == 1)
                    {
                        restTime += layoverTime;
                        totalTime += restTime;
                    }
                    if (totalTime < 12 && driveTime < 8)
                    {
                        driveTime += transportCorridors[i].TimeBetween;
                        totalTime += driveTime;

                    }
                    if (totalTime >= 12 || driveTime >= 8)
                    {
                        ++daysAdded;
                        restTime = 0;
                        driveTime = 0;

                        // The final time is calculated by totalTime + (daysAdded*24), totalTime serves as the 
                        // remainder of hours if a whole day wasn't required, set to 0 if the last city 
                        // has been reached so too much time isn't added.
                        if (contract.Origin == transportCorridors[i].CityName)
                        {
                            totalTime = 0;
                        }
                    }
                }
            }
            else
            {
                if (originIndex > DestIndex)     // if the destination is east
                {
                    for (int j = originIndex; j >= DestIndex; j--)
                    {
                        if (contract.Origin == transportCorridors[j-1].CityName || contract.Origin == transportCorridors[j-1].CityName
                        || contract.JobType == 1)
                        {
                            restTime += layoverTime;
                            totalTime += restTime;
                        }
                        if (totalTime < 12 && driveTime < 8)
                        {
                            driveTime += transportCorridors[j - 1].TimeBetween;
                            totalTime += driveTime;

                        }
                        if (totalTime >= 12 || driveTime >= 8)
                        {
                            ++daysAdded;
                            restTime = 0;
                            driveTime = 0;
                            if (contract.Origin == transportCorridors[j - 1].CityName)
                            {
                                totalTime = 0;
                            }
                        }
                    }
                }
            }

            return totalTime + (daysAdded * 24);
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
        /// Order being created based on the pallet threshold
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
