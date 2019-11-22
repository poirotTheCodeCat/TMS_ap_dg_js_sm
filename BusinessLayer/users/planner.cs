using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_ap_dg_js_sm.Business_Layer.users;

namespace TMS_ap_dg_js_sm
{
    /// \class Planner
    /// <summary>
    /// Represents planner user object for the TMS system
    /// </summary>
    class Planner
    {
        private Order workingOrder;
        private bool pendingOrders;

        /// <summary>
        /// This method selects carrier to view the details of that carrier.  It adds the information to a list to allow for fields to be auto populated 
        /// for easy viewing
        /// </summary>
        /// <param name="carrierName">The carrierName parameter provides the function with the name of the carrier being searched.</param>
        /// <returns>Returns the list of carrier information</returns>
        public List<Carrier> SelectCarrier(string carrierName)
        {
            var carrier = new List<Carrier>();

            return  carrier;
        }

        /// <summary>
        /// This method generates the trip origin and destination cities required by the order.
        /// </summary>
        /// <param name="origin">The origin city ID number</param>
        /// <param name="destination">The destination city ID number</param>
        /// <returns>A list of the trip information</returns>
        public List<Trip> GenerateTrip(int origin, int destination)
        {

            var trip = new List<Trip>();


            return trip;
        }

        /// <summary>
        /// This method marks the order as approved by the planner
        /// </summary>
        /// <param name="orderNumber">The order ID number</param>
        /// <returns>True if approved false if not yet approved</returns>
        public int ApproveOrder(int orderNumber)//Possibly order ID??
        {
            int done = 0;

            return done;
        }

        /// <summary>
        /// This method marks the order as completed by the planner
        /// </summary>
        /// <param name="orderNumber">The order ID number</param>
        /// <returns>True if approved false if not yet apporved</returns>
        public int CompleteOrder(int orderNumber)//Again....Possibly orderID??
        {

            int done = 0;

            return done;
        }

        /// <summary>
        /// This order queries the database for all active orders in the TMS system
        /// </summary>
        /// <returns>A list of active orders</returns>
        public List<Order> ShowActiveOrders()
        {
            var activeOrders = new List<Order>();


            return activeOrders;
        }

        /// <summary>
        /// This order queries the database for all pending orders in the TMS system
        /// </summary>
        /// <returns>A list of pending orders</returns>
        public List<Order> ShowPendingOrders()
        {
            var pendingOrders = new List<Order>();


            return pendingOrders;
        }

        /// <summary>
        /// This method queries the database for the order values and calculates the sum
        /// </summary>
        /// <param name="orderNumber">The order ID number</param>
        /// <returns>True if approved false if not yet approved</returns>
        public int GenerateInvoiceSum(int orderNumber)//OrderID??
        {
            int done = 0;

            return done;
        }


    }
}
