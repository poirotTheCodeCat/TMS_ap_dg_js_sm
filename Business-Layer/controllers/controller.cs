using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_ap_dg_js_sm.Business_Layer.users;

namespace TMS_ap_dg_js_sm
{
    /// \class Controller
    /// <summary>
    /// This class will provide the business logic for all the user functionalities and acts as a central router to all internal 
    /// and external obects.
    /// </summary>
    class Controller
    {
        
        /// <summary>
        /// This method queries the orders table for all active orders
        /// </summary>
        /// <returns>0 if the query was successful and 1 is qeury failed.</returns>
        
        public int ShowActiveOrders()
        {
            int done = 0;

            try
            {
                done = 1;
                //Try and connect to DB
            }
            catch(Exception e)
            {
                Logger.Log("Failed", 1);
            }

            return done;
        }

        /// <summary>
        /// This function queries the orders table for all completed orders
        /// </summary>
        /// <returns>0 if the query was successful and 1 is qeury failed.</returns>
        public int ShowCompletedOrders()
        {
            int done = 0;

            try
            {
                done = 1;
                //Try and connect to DB
            }
            catch (Exception e)
            {
                Logger.Log("Failed", 1);
            }

            return done;
        }

        /// <summary>
        /// This method enables the Admin user to be able to change the target IP addess
        /// </summary>
        /// <param name="newIP">Holds the new IP address from the admin user</param>
        public int UpdateIP(string newIP)
        {
            int done = 0;

            try
            {
                done = 1;
                //Try and connect to DB
            }
            catch (Exception e)
            {
                Logger.Log("Failed", 1);
            }

            return done;
        }

        /// <summary>
        /// This method enables the Admin user to be able to change the target IP addess
        /// </summary>
        /// <param name="newPort">Holds the new PORT number from the admin user</param>
        public int UpdatePort(string newPort)
        {
            int done = 0;

            try
            {
                done = 1;
                //Try and connect to DB
            }
            catch (Exception e)
            {
                Logger.Log("Failed", 1);
            }

            return done;
        }

        /// <summary>
        /// This method updates the selected carrier's information
        /// </summary>
        /// <param name="carrierID">Holds the carrier ID of the selected carrier</param>
        /// <returns>0 if successfully updated, 1 if update fails.</returns>
        
        public int UpdateCarrier(int carrierID)
        {
            int done = 0;
            try
            {
                done = 1;
            }
            catch(Exception e)
            {
                Logger.Log("Failed", 1);
            }
            return done;
        }


        /// <summary>
        /// This method allows the Admin to update the van rates table
        /// </summary>
        /// <param name="rateID">Holds the rateID for the selected vant rate which needs to be updated</param>
        /// <returns>0 if successfully updated, 1 if update fails.</returns>
        public int UpdateRateTable(string rateID)
        {
            int done = 0;

            try
            {
                done = 1;
            }
            catch(Exception e)
            {
                Logger.Log("Failed", 1);
            }
            return done;
        }

        /// <summary>
        /// This method allows the admin to update the route table
        /// </summary>
        /// <param name="routeID">Holds the routeID of the route which needs to be updated</param>
        /// <returns>0 if successfully updated, 1 if update fails.</returns>
        public int UpdateRoutTable(int routeID)
        {
            int done = 0;

            try
            {
                done = 1;
            }
            catch(Exception e)
            {
                Logger.Log("Failed", 1);
            }

            return done;
        }
    }
}
