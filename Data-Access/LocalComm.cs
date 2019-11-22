using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_ap_dg_js_sm
{
    /// \class LocalComm
    /// <summary>
    /// Responsible for any communications with the local TMS database.
    /// </summary>
    class LocalComm
    {
        /// <summary>
        /// This method adds a new Order to the TMS database.
        /// <param name="searchItem">The identifier for the Contract that will be used 
        ///                         to create the new Order</param>
        /// </summary>
        /// <returns>Order that has been created.</returns>
        public Order CreateOrder(string searchItem)
        {
            Order newOrder = null;
            return newOrder;
        }

        /// <summary>
        /// This method queries the TMS database for completed Orders.
        /// </summary>
        /// <returns>List<Order></Order> of the completed Orders.</returns>
        public List<Order> GetCompletedOrders()
        {
            List<Order> completedOrderList = null;
            return completedOrderList;
        }

        /// <summary>
        /// This method queries the TMS database for the invoice requested.
        /// </summary>
        /// <param name="searchItem">The identifier for the Order that requires
        ///                             an invoice</param>
        /// <returns>String containing the information required for an invoice.</returns>
        public string GetInvoice(string searchItem)
        {
            string invoice = "";
            return invoice;
        }
    }
}
