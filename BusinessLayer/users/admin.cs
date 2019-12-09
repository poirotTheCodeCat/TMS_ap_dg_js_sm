using System;
using System.Collections.Generic;
using TMS.Business_Layer.users;

namespace TMS
{
    /// \class Admin
    /// <summary>
    /// Represents an Admin user of the TMS program. The Admin makes use of the LocalComm class
    /// for updating information stored in the TMS database.
    /// </summary>
    /// <exception cref="fileIoException">Exception during file reading or writing in the ShowLogs() function</exception>
    public class Admin
    {
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

        internal void GetBackUp(string filePath)
        {
            new LocalComm().BackUpDb(filePath);
            Logger.Log("TMS database backup.");
        }

        /// <summary>
        /// This method is used to update a the values of the route table.
        /// </summary>
        /// <param name="c">The id of the selected route</param>
        public void UpdateRouteTable(Carrier c)
        {
            
        }

        /// <summary>
        /// This method is used to update a column and row location in the carrier table database.
        /// </summary>
        /// <param name="c">The carrier to be updated</param>
        public void UpdateCarrierTable(Carrier c)
        {
            new LocalComm().UpdateCarrierFTLRate(c);
        }

        /// <summary>
        /// This method is used to update a the values of the rate table.
        /// </summary>
        /// <param name="c">The id of the selected route</param>
        public void UpdateRateTable(Carrier c)
        {

        }

        /// <summary>
        /// This method gets a Carrier to view the details of that Carrier. 
        /// </summary>

        /// <returns>Carrier that is requested.</returns>
        public List<Carrier> GetCarriers()
        {
            List<Carrier> carriers = new Planner().GetCarriers();
           
            return carriers;
        }

        /// <summary>
        /// This method gets a Carrier to view the details of that Carrier. 
        /// </summary>
        /// <returns>Routes that are requested.</returns>
        public List<TransportCorridor> GetRoutes()
        {

            List<TransportCorridor> routes = new LocalComm().GetRoutes();
            

            return routes;
        }

        /// <summary>
        /// This method gets a Carrier to view the details of that Carrier. 
        /// </summary>
        /// <returns>Rates that are requested.</returns>
        public List<double> GetRates()
        {
            List<double> rates = new LocalComm().GetRates();
            

            return rates;
        }
    }
}
