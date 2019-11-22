using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_ap_dg_js_sm.Business_Layer.users;

namespace TMS_ap_dg_js_sm.Business_Layer.orderManagement
{
    /// \class Contract
    /// <summary>
    /// This class represents a Contract object for the TMS system
    /// </summary>
    /// <exception cref="fileIoException">Exception during file reading or writing in all functions</exception>
    public class Contract
    {
        private string cName;///< Holds the carrier name as a string
        private int jType;///< Holds the job type as a string
        private int vType;///< Holds the van type as an integer
        private int quant;///< Holds the quantity of vans available
        private string origin;///< Holds origin city
        private string dest;///< Holds the destination city

        /// <summary>
        /// Get and set cName  
        /// </summary>
        public string CName
        {
            get { return cName; }
            set { cName = value; }
        }

        /// <summary>
        /// Gets and sets             get { return jType; }

        /// </summary>
        public int JType
        {
            get { return jType; }
            set { jType = value; }
        }

        /// <summary>
        /// Gets and sets vType
        /// </summary>
        public int VType
        {
            get { return vType; }
            set { vType = value; }
        }

        /// <summary>
        /// Gets and sets quant
        /// </summary>
        public int Quant
        {
            get { return quant; }
            set { quant = value; }
        }

        /// <summary>
        /// Gets and sets origin
        /// </summary>
        public string Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        /// <summary>
        /// Gets and sets dest
        /// </summary>
        public string Dest
        {
            get { return dest; }
            set { dest = value; }
        }

        /// <summary>
        /// This method queries the Contract Marketplace to search for the desired contract 
        /// </summary>
        /// <returns>0 if successfully queried table. 1 if failed</returns>

        public int GetContract()
        {

            int done = 0;

            try
            {
                //SELECT contract from contract marketplace
                done = 1;//Set to 1 for testing
            }
            catch (Exception fileIoException)
            {
                Logger.Log(fileIoException.ToString());
            }
            return done;

        }
    }
}
