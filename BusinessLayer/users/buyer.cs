using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_ap_dg_js_sm.Data_Access;

namespace TMS_ap_dg_js_sm
{
    /// \class Admin
    /// <summary>
    /// Represents a buyer user of the TMS program
    /// </summary>
    class Buyer
    {
        /// <summary>
        /// This method will allow the buyer user to fetch contracts from the Contract Marketplace database.
        /// These values will be updated in the App.config file.
        /// </summary>
        /// <param name="newIP">The requested new IP address</param>
        /// <param name="newPort">The requested new port number - default to blank in the case where the user only
        ///                       wants to update the ip address.</param>
        /// <returns>Int representing the successful change in IP/Port values.</returns>
        /// <exception cref="fileIoException">Exception during file IO</exception>
        public List<Contract> GetContracts()
        {
            List<Contract> contractList = new ExternalComm().GetContracts();
            return contractList; 
        }

        // the searchItem would be the contractID
        public Order CreateOrder(string searchItem)
        {
            Order newOrder = new LocalComm().CreateOrder(searchItem);
            return newOrder;
        }


    }
}
