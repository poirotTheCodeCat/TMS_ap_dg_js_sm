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
    class Contract
    {
        private string cName;///< Holds the carrier name as a string
        private int jType;///< Holds the job type as a string
        private int vType;///< Holds the van type as an integer
        private int Quant;///< Holds the quantity of vans available
        private string Orig;///< Holds origin city
        private string Dest;///< Holds the destination city


        /// <summary>
        /// This method queries the Contract Marketplace to search for the desired contract 
        /// </summary>
        /// <returns>0 if successfully queried table. 1 if failed</returns>

        public int GetContract()
        {

            int done = 0;

            try
            {
                //SELECT conrtact from contract marketplace
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
