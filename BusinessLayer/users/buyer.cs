using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Business_Layer;
using TMS;

namespace TMS
{
    /// \class Buyer
    /// <summary>
    /// Represents a Buyer user of the TMS program. The Buyer makes use of the LocalComm class 
    /// to update the TMS database and the ExternalComm class to communicate with the Contract
    /// Marketplace. It also makes use of the Order class for managing Orders.
    /// </summary>
    public class Buyer
    {
        /// <summary>
        /// This method will allow the Buyer to get contracts from the Contract Marketplace database. 
        /// </summary>
        /// <returns>Listmof the Contracts received from the Contract Marketplace.</returns>
        public List<Contract> GetContracts()
        {
            List<Contract> contractList = new ExternalComm().GetContracts();
            return contractList; 
        }

        /// <summary>
        /// This method will allow the Buyer to create a new Order that will be added to
        /// the local database. 
        /// <param name="searchItem">The identifier for the Contract that will be used 
        ///                         to create the new Order</param>
        /// </summary>
        /// <returns>Order that has been created.</returns>
        public Order CreateOrder(string searchItem)
        {
            Order newOrder = new LocalComm().CreateOrder(searchItem);
            return newOrder;
        }

        /// <summary>
        /// This method will allow the Buyer to view Orders marked by completion by the Planner.
        /// </summary>
        /// <returns>List of the completed Orders.</returns>
        public List<Order> GetCompletedOrders()
        {
            string searchItem = "";
            List<Order> completedOrderList = new LocalComm().GetOrders(searchItem);
            return completedOrderList;
        }

        /// <summary>
        /// This method will allow the Buyer to generate an invoice as a text file for the 
        /// requested completedOrder.
        /// </summary>
        /// /// <param name="searchItem">The identifier for the Order that requires
        ///                             an invoice</param>
        /// <returns>Int representing invoice was successfully generated.</returns>
        public int GenerateInvoice(string searchItem)
        {
            int done = 0;

            done = 1; //set to true for testing
            string invoiceInformation = new LocalComm().GetInvoice(searchItem);

            return done;
        }

    }
}
