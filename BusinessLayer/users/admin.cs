using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TMS_ap_dg_js_sm.Business_Layer.users;

namespace TMS_ap_dg_js_sm
{
    /// \class Admin
    /// <summary>
    /// Represents an admin user of the TMS program
    /// </summary>
    /// <exception cref="fileIoException">Exception during file reading or writing in the ShowLogs() function</exception>
    class Admin
    {
        /// <summary>
        /// This method will allow the admin user to change the current IP address used to access the TMS database.
        /// These values will be updated in the App.config file.
        /// </summary>
        /// <param name="newIP">The requested new IP address</param>
        /// <param name="newPort">The requested new port number - default to blank in the case where the user only
        ///                       wants to update the ip address.</param>
        /// <returns>Int representing the successful change in IP/Port values.</returns>
        public int ChangeTmsConnSettings(string newIP, string newPort = "")
        {
            int done = 0;

            done = 1; //set to true for testing

            return done;
        }

        /// <summary>
        /// This method will allow the admin user to change the current IP address used to access the Contract
        /// Marketplace database. These values will be updated in the App.config file.
        /// </summary>
        /// <param name="newIP">The requested new IP address</param>
        /// <param name="newPort">The requested new port number - default to blank in the case where the user only
        ///                       wants to update the ip address.</param>
        /// <returns>Int representing the successful change in IP/Port values.</returns>
        public int ChangeCmpConnSettings(string newIP, string newPort = "")
        {
            int done = 0;

            done = 1; //set to true for testing

            return done;
        }

        /// <summary>
        /// This method will allow the admin user to change the current save location of the Logger class log file.
        /// These values will be updated in the App.config file.
        /// </summary>
        /// <param name="newFileLocation">The requested new log save location</param>
        /// <returns>Int representing the successful change of the log save location.</returns>
        public int ChangeLogLocation(string newFileLocation)
        {
            int done = 0;

            done = 1; //set to true for testing

            return done;
        }

        /// <summary>
        /// This method is used to show the current logs saved in the log text file to the screen.
        /// </summary>
        /// <returns>Int representing the successful printing of log file, else exception occured.</returns>
        public int ShowLogs()
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

        /// <summary>
        /// This method is used to update a column and row location in the route table database.
        /// </summary>
        /// <param name="routeID">The id of the selected route</param>
        /// <param name="whatColumn">The name of the selected column</param>
        /// <param name="newValue">The new value to insert in that location</param>
        /// <returns>Int representing successful addition to the database.</returns>
        public int UpdateRouteTable(int routeID, string whatColumn, string newValue)
        {
            int done = 0;

            done = 1; //set to true for testing

            return done;
        }

        /// <summary>
        /// This method is used to update a column and row location in the carrier table database.
        /// </summary>
        /// <param name="carrierID">The id of the selected carrier</param>
        /// <param name="whatColumn">The name of the selected column</param>
        /// <param name="newValue">The new value to insert in that location</param>
        /// <returns>Int representing successful addition to the database.</returns>
        public int UpdateCarrierTable(int carrierID, string whatColumn, string newValue)
        {
            int done = 0;

            done = 1; //set to true for testing

            return done;
        }

        /// <summary>
        /// This method is used to update a column and row location in the rate table database.
        /// </summary>
        /// <param name="rateID">The id of the selected route</param>
        /// <param name="whatColumn">The name of the selected column</param>
        /// <param name="newValue">The new value to insert in that location</param>
        /// <returns>Int representing successful addition to the database.</returns>
        public int UpdateRateTable(int rateID, string whatColumn, string newValue)
        {
            int done = 0;

            done = 1; //set to true for testing

            return done;
        }
    }
}
