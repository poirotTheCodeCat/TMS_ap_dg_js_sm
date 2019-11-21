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
    /// Represents an order object fore the TMS system
    /// </summary>
    /// <exception cref="fileIoException">Exception during file reading or writing in the CheckTripStatus() function</exception>
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
        /// This method will allow the admin user to change the current IP address used to access the TMS database.
        /// These values will be updated in the App.config file.
        /// </summary>
        /// <param name="currentDestination">The current location of the order</param>
        /// <returns>Updated or current destination</returns>
        public int CheckTripStatus(int currentDestination)
        {
            int done = 0;

            try
            {
                //open text file
                //read each line to specific WPF text box
                done = 1; //set to true for testing
            }
            catch (Exception fileIoException)
            {
                Logger.Log(fileIoException.ToString());
            }

            return done;
        }
    }
}
