using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_ap_dg_js_sm.Business_Layer.users;

namespace TMS_ap_dg_js_sm
{
    /// \class Order
    /// <summary>
    /// Represents an order object for the TMS system
    /// </summary>
    public class Order
    {
        private string cName;///< Holds the customer name as a string
        private int contractID;///< Holds the contract ID as an int 
        private int carrierID;///< Holds the carrier ID as a int
        private List<Trip> orderTrip;///< Holds a list of trip objects for the current order
        private int markup;///< Holds the markup rate as a int
        private bool isApproved;///< True if order has been approved and false if it is still pending
        private bool isComplete;///< True if order if conmplete and false if not complete

        /// <summary>
        /// Get and set cName  
        /// </summary>
        public string CName
        {
            get { return cName; }
            set { cName = value; }
        }

        /// <summary>
        /// Gets and sets contractID
        /// </summary>
        public int ContractID
        {
            get { return contractID; }
            set { contractID = value; }
        }

        /// <summary>
        /// Gets and sets carrierID
        /// </summary>
        public int CarrierID
        {
            get { return carrierID; }
            set { carrierID = value; }
        }

        /// <summary>
        /// Gets and sets orderTrip
        /// </summary>
        public List<Trip> OrderTrip
        {
            get { return orderTrip; }
            set { orderTrip = value; }
        }

        /// <summary>
        /// Gets and sets markup
        /// </summary>
        public int Markup
        {
            get { return markup; }
            set { markup = value; }
        }

        /// <summary>
        /// Gets and sets isApproved
        /// </summary>
        public bool IsApproved
        {
            get { return isApproved; }
            set { isApproved = value; }
        }

        /// <summary>
        /// Gets and sets isComplete
        /// </summary>
        public bool IsComplete
        {
            get { return isComplete; }
            set { isComplete = value; }
        }

        /// <summary>
        /// This method will check the current location of the order along its route via a trip object.
        /// </summary>
        /// <param name="currentDestination">The current location of the order</param>
        /// <returns>Updated or current destination</returns>
        public int CheckTripStatus(int currentDestination)
        {
            int done = 0;

            done = 1;

            return done;
        }
    }
}
