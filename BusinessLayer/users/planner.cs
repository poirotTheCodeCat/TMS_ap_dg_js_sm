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
    /// Represents a Planner user object for the TMS system. The Planner makes use of the 
    /// LocalComm class to update information in the TMS Database. It also uses objects in the
    /// Order and Trip classes. 
    /// </summary>
    public class Planner
    {
        //private Order workingOrder;
        //private bool pendingOrders;

        /// <summary>
        /// This method selects Carrier to view the details of that Carrier. 
        /// </summary>
        /// <param name="searchItem">The identifier for the Carrier that will be returned</param>
        /// <returns>Carrier that is requested.</returns>
        public Carrier GetCarrier(string searchItem)
        {
            Carrier carrier = new Carrier();

            return carrier;
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
        public List<Order> ShowPendingOrders()
        {
            List<Order> pendingOrderList = new List<Order>();

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


    }
}
