/*
 * File Name: Planner.cs
 * Program Name: TMS_ap_dg_js_sm
 * Programmers: Arron Perry, Daniel Grew, John Stanley, Sasha Malesevic
 * First Version: 2019-12-09
 */
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

        public Planner()
        {
            PalletThreshold = 17;
            transportCorridors = GetRoutes();
        }

        /// <summary>
        /// This method gets all Routes from the local database.
        /// </summary>
        /// <returns>All Routes.</returns>
        public List<TransportCorridor> GetRoutes()
        {

            List<TransportCorridor> routes = new LocalComm().GetRoutes();


            return routes;
        }

        /// <summary>
        /// This method gets all Carriers from the local database
        /// </summary>
        /// <returns>List of all Carriers</returns>
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
        /// This method allows the Planner to mark an Order as approved.
        /// </summary>
        /// <param name="contract">The identifier for the Order that needs to be marked 
        ///                         approved</param>
        /// <returns>Int representing an Order was successfully marked approved.</returns>
        public void ConfirmOrder(Contract contract)
        {
            contract.PlannerConfirmed = 1;
            new LocalComm().UpdateContract(contract);
        }

        /// <summary>
        /// This method allows the Planner to view all pending Orders.
        /// </summary>
        /// <returns>List of pending Orders.</returns>
        public List<Contract> ShowPendingOrders()
        {
            List<Contract> pendingOrderList = new List<Contract>();
            List<Contract> allContracts = new LocalComm().GetLocalContracts();

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
        /// This method shows all Contracts from the local database. 
        /// </summary>
        /// <returns>List of all Contracts</returns>
        public List<Contract> ShowAllContracts()
        {
            List<Contract> allContracts = new LocalComm().GetLocalContracts();
            return allContracts;
        }

        /// <summary>
        /// This method allows the Planner to generate an invoice summary of requested Orders
        /// </summary>
        /// <param name="sumStartTime">The current time in the application</param>
        /// <param name="summaryTime">The requested time interval for the invoice summary.</param>
        /// <returns>List of Contracts for the invoice summary.</returns>
        public List<Contract> GenerateInvoiceSum(DateTime sumStartTime, int summaryTime = 0) // 2 weeks or of all time
        {
            DateTime? conStartTime = new DateTime();
            DateTime conEndTime = new DateTime();
            List<Contract> localContracts = new LocalComm().GetLocalContracts();
            List<Contract> summaryContracts = new List<Contract>();
            
            
            sumStartTime = sumStartTime.AddHours(24 * 14 * -1);
            foreach (Contract c in localContracts)
            {
                if(c.PlannerConfirmed == 1)     // for 2 weeks
                {   
                    if(summaryTime == 1)
                    {
                        conEndTime = Convert.ToDateTime(c.EndTime);
                        if (conEndTime >= sumStartTime)
                        {
                            summaryContracts.Add(c);
                        }
                    }
                    else
                    {
                        summaryContracts.Add(c);
                    }
                }
            }

            return summaryContracts;
        }

        /// <summary>
        /// THis calculates the amount that we charge the client
        /// </summary>
        /// <param name="contract"></param>
        /// <param name="orderCarriers"> The Carriers assosicated with the order</param>
        /// <param name="multipleCarr"> Indicates whether multiple Carriers are associated with a Contract</param>
        /// <returns>The price of the Contract</returns>
        public double GetClientCharge(Contract contract, List<Carrier> orderCarriers, int multipleCarr=0)
        {
            List<double> markUp = new LocalComm().GetRates();
            int distance = CalculateDistance(contract.Origin, contract.Destination);
            double hours = CalculateTime(contract);
            double dailyCharge = 150;
            int daysTravelled = 0;
            double h = hours;
            while(h > 24)
            {
                h -= 24;
                ++daysTravelled;
            }
            
            if (contract.JobType == 0)  // FTL 
            {
                contract.Price = (orderCarriers[0].FtlRate * distance)      // Original Rate
                    + (orderCarriers[0].FtlRate * distance * markUp[0]);    // Add OSHT mark up
            }
            else if (multipleCarr == 1)
            {
                // Finds the number of pallets carried by each carrier, bills appropriately 
                foreach (Carrier orderC in orderCarriers)
                {
                    if (contract.VanType == 0)   // Dry van 
                    {
                        contract.Price += 
                                (orderC.Pallets * distance * orderC.LtlRate)                // Original Rate
                            + (orderC.Pallets * distance * orderC.LtlRate * markUp[1]);     // Add OSHT mark up
                    }
                    else // Reefer van 
                    {
                        contract.Price += 

                                (orderC.Pallets * distance * orderCarriers[0].LtlRate)      // Original Rate 
                            + (orderC.Pallets * distance * orderC.ReefRate)                 // Add Reef mark up 
                            + (orderC.Pallets * distance * orderC.LtlRate * markUp[1]);     // Add OSHT mark up
                    }
                }
            }
            else // LTL single contracts
            {
                if(contract.VanType == 0)   // Dry van 
                {
                    contract.Price = 
                            (contract.Quantity * distance * orderCarriers[0].LtlRate)
                        + (contract.Quantity * distance * orderCarriers[0].LtlRate * markUp[1]);
                }
                else // Reefer van 
                {
                    contract.Price = 
                            (contract.Quantity * distance * orderCarriers[0].LtlRate)
                        + (contract.Quantity * distance * orderCarriers[0].ReefRate)
                        + (contract.Quantity * distance * orderCarriers[0].LtlRate * markUp[1]);
                }
            }

            contract.Price += daysTravelled * dailyCharge;
            return contract.Price;
        }
        

        /// <summary>
        /// This creates an order and inserts it into the database
        /// </summary>
        /// <param name="contracts"></param>
        /// <param name="orderCarriers"></param>
        public void CreateOrder(List<Contract> contracts, List<Carrier> carriers, DateTime currTime)
        {
            // 1. Create a trip for each carrier and contract 
            // 2. Generate the price to be used for invoice generation 
            // 3. Add order lines for customer id, end time, status (for planner confirmation), price)

            int remaining = 0; 
            // Add all carriers to each contract 
            // Update Carrier availability 
            if(contracts.Count == 1 && contracts[0].JobType == 1)
            {
                remaining = contracts[0].Quantity;
                for (int i = 0; i < carriers.Count; i++)
                {
                    // If it is not the last carrier added 
                    if (i == carriers.Count - 1)
                    {
                        carriers[i].LtlAvail -= remaining;
                        carriers[i].Pallets = remaining;
                        new LocalComm().UpdateCarrierLTL(carriers[i]);
                    }
                    else
                    {
                        carriers[i].Pallets = carriers[i].LtlAvail; // The carrier is using up all it's Ltl availability
                        remaining -= carriers[i].LtlAvail;
                        carriers[i].LtlAvail = 0;
                        new LocalComm().UpdateCarrierLTL(carriers[i]);
                    }
                }
            }
            else // FTL or combined LTLs, only one carrier  
            {
                carriers[0].FtlAvail -= 1;
                new LocalComm().UpdateCarrierFTL(carriers[0]);
            }
            

            DateTime startTime = currTime;
            // BuyerSelected = 1 
            // PlannerSelected = 0
            // EndTime != null 
            foreach(Contract con in contracts)
            {
                con.EndTime = currTime.AddHours(CalculateTime(con));

                if(carriers.Count > 1)
                {
                    con.Price = GetClientCharge(con, carriers, 1);
                }
                else
                {
                    con.Price = GetClientCharge(con, carriers);
                }

                // Add a trip for each carrier and contract
                foreach (Carrier carr in carriers)
                {
                    new LocalComm().AddTrip(con.ContractID, carr.CarrierID);
                }
                con.UpdateContract();
            }

        }
        /// <summary>
        /// This function calculates the distance required to travel between an origin city and a destination city
        /// </summary>
        /// <param name="startCity"></param>
        /// <param name="endCity"></param>
        /// <returns></returns>
        public int CalculateDistance(string startCity, string endCity)
        {
            int originIndex = -1;
            int DestIndex = -1;
            int distance = 0;

            foreach (TransportCorridor t in transportCorridors)
            {
                // find the index of the start city
                // find the index of the second city
                if (startCity == t.CityName)        // check if the city is the origin city
                {
                    originIndex = transportCorridors.IndexOf(t);
                }
                if (endCity == t.CityName)          // check if the city is the destination city
                {
                    DestIndex = transportCorridors.IndexOf(t);
                }
                if (originIndex != -1 && DestIndex != -1)
                {
                    break;
                }
            }
            // now that the indexes have been found we can compare and see what direction we should travel in
            if (originIndex < DestIndex)     // if the destination is west
            {
                for (int i = originIndex; i < DestIndex; i++)
                {
                    if(transportCorridors[i].CityName != endCity)
                    {
                        distance += transportCorridors[i].Distance;
                    }
                }
            }
            else
            {
                for (int j = originIndex; j > DestIndex; j--)
                {
                    if (transportCorridors[j].CityName != endCity)
                    {
                        distance += transportCorridors[j - 1].Distance;

                    }
                }
            }
            return distance;
        }
        /// <summary>
        /// Calculates the total time needed to complete the trip in hours
        /// </summary>
        /// <param name="contract"></param>
        /// <returns>The total time to complete the Contract.</returns>
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
                    if (contract.Origin == transportCorridors[i].CityName || contract.Destination == transportCorridors[i].CityName
                        || contract.JobType == 1)
                    {
                        restTime += layoverTime;
                    }
                    if (totalTime < 12 && driveTime < 8)
                    {
                        driveTime += transportCorridors[i].TimeBetween;

                    }
                    if ((restTime + driveTime) >= 12 || driveTime >= 8)
                    {
                        ++daysAdded;
                        restTime = 0;
                        driveTime = 0;

                        // The final time is calculated by totalTime + (daysAdded*24), totalTime serves as the 
                        // remainder of hours if a whole day wasn't required, set to 0 if the last city 
                        // has been reached so too much time isn't added.
                      
                    }
                }
            }
            else
            {
                if (originIndex > DestIndex)     // if the destination is east
                {
                    for (int j = originIndex; j >= DestIndex; j--)
                    {
                        if (contract.Origin == transportCorridors[j].CityName || contract.Destination == transportCorridors[j].CityName
                        || contract.JobType == 1)
                        {
                            restTime += layoverTime;
                            //totalTime += restTime;
                        }
                        if (totalTime < 12 && driveTime < 8 && (contract.Destination != transportCorridors[j].CityName))
                        {
                            driveTime += transportCorridors[j - 1].TimeBetween;
                            //totalTime += driveTime;

                        }
                        if ((driveTime + restTime) >= 12 || driveTime >= 8)
                        {
                            ++daysAdded;
                            restTime = 0;
                            driveTime = 0;
                        }
                    }
                }
            }

            return (driveTime+restTime) + (daysAdded * 24);
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
    }
}
