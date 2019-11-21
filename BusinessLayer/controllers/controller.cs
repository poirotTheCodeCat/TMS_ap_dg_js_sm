using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_ap_dg_js_sm
{
    /// \class Controller
    /// <summary>
    /// This class will provide the business logic for all the user functionalities and acts as a central router to all internal 
    /// and external obects.
    /// </summary>
    class Controller
    {
        private bool isConnected; /// Checks whether the user is connected to the database
       

        public bool IsConnected     // mutator and accessor for isConnectd
        {
            get { return isConnected; }
            set { isConnected = value; }
        }

        /// <summary>
        /// This method establishes a connectin to the database required by the current user
        /// </summary>
        /// <param name="currentIP">The requested IP address</param>
        /// <param name="currentPort">The requested PORT number</param>
        /// <returns>0 if no connection is established and 1 if connection exists</returns>
        public int ConnectToDB(string currentIP, string currentPort)
        {

            return 0;
        }

        /// <summary>
        /// This method queries the orders table for all active orders
        /// </summary>
        /// <returns>0 if the query was successful and 1 is qeury failed.</returns>
        
        public int ShowActiveOrders()
        {
            return 0;
        }

        /// <summary>
        /// This function queries the orders table for all completed orders
        /// </summary>
        /// <returns>0 if the query was successful and 1 is qeury failed.</returns>
        public int ShowCompletedOrders()
        {
            return 0;
        }

        /// <summary>
        /// This method enables the Admin user to be able to change the target IP addess
        /// </summary>
        /// <param name="newIP">Holds the new IP address from the admin user</param>
        public void UpdateIP(string newIP)
        {
            
        }

        /// <summary>
        /// This method enables the Admin user to be able to change the target IP addess
        /// </summary>
        /// <param name="newPort">Holds the new PORT number from the admin user</param>
        public void UpdatePort(string newPort)
        {

        }

        /*
         * Function :
         * Parameters :
         * Description : 
         * Returns : 
         */
        public void UpdateCarrier()
        {

        }

        /*
         * Function :
         * Parameters :
         * Description : 
         * Returns : 
         */
        public void UpdateRateTable()
        {

        }

        /*
         * Function :
         * Parameters :
         * Description : 
         * Returns : 
         */
        public void UpdateRoutTable()
        {

        }

        /*
         * Function :
         * Parameters :
         * Description : 
         * Returns : 
         */
        public void ValidateLogin()
        {

        }

        /*
         * Function :
         * Parameters :
         * Description : 
         * Returns : 
         */
        public void SendAnswer()
        {

        }
    }
}
