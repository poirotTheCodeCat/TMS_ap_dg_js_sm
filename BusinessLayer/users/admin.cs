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
    public class Admin
    {
        /// <summary>
        /// This method will create a .sql file of the current TMS database for backup.
        /// </summary>
        /// <param name="filePath">Chosen file location to save the back up file.</param>
        internal void GetBackUp(string filePath)
        {
            new LocalComm().BackUpDb(filePath);
            Logger.Log("TMS database backup.");
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
