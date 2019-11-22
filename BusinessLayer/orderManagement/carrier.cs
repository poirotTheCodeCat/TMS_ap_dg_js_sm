using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Business_Layer.users;

namespace TMS
{
    /// \class Carrier
    /// <summary>
    /// This class represents a Carrier object for the TMS system
    /// Carrier requires the use of the Order and Trip classes
    /// </summary>
    /// <exception cref="fileIoException">Exception during file reading or writing in all functions</exception>
    public class Carrier
    {
        private string carrierName;///< Holds the carrier name as a string
        private string depotCity;///< Holds the depot city as a string
        private int ftlAvailable;///< Holds the number of available FTL trucks for the specified carrier
        private int ltlAvailable;///< Holds the number of available LTL trucks for the specified carrier
        private double ftlRate;///< Holds the rate of FTL for the specified carrier
        private double ltlRate;///< Holds the rate of LTL for the specified carrier
        private int refferCharge;///< Holds the rate of a reffer for the specified carrier

        /// <summary>
        /// Gets and sets carrierName
        /// </summary>
        public string CarrierName
        {
            get { return carrierName; }
            set { carrierName = value; }
        }
        /// <summary>
        /// Gets and sets depotCity
        /// </summary>

        public string DepotCity
        {
            get { return depotCity; }
            set { depotCity = value; }
        }
        /// <summary>
        /// Gets and sets ftlAvailable
        /// </summary>
        public int FtlAvailable
        {
            get { return ftlAvailable; }
            set { ftlAvailable = value; }
        }

        /// <summary>
        /// Gets and sets ltlAvailable
        /// </summary>
        public int LtlAvailable
        {
            get { return ltlAvailable; }
            set { ltlAvailable = value; }
        }

        /// <summary>
        /// Gets and sets ftlRate
        /// </summary>
        public double FtlRate
        {
            get { return ftlRate; }
            set { ftlRate = value; }
        }

        /// <summary>
        /// Gets and sets ltlRate
        /// </summary>
        public double LtlRate
        {
            get { return ltlRate; }
            set { ltlRate = value; }
        }

        /// <summary>
        /// Gets and sets refferCharge
        /// </summary>
        public int RefferCharge
        {
            get { return refferCharge; }
            set { refferCharge = value; }
        }

        /// <summary>
        /// This method queries the DBMS for the selected carrier infromation. 
        /// </summary>
        /// <param name="carrierName">carrier name ID number</param>
        /// <returns>0 if successfully queried table. 1 if failed</returns>
        public int GetCarrierInfo(int carrierName)
        {
            int done = 0;

            try
            {
                //Select Carrier from table and display info
                done = 1; //For testing
            }
            catch (Exception fileIoException)
            {
                Logger.Log(fileIoException.ToString());
            }

            return done;
        }
    }
}
