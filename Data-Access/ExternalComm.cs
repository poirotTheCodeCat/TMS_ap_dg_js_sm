using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Business_Layer;

namespace TMS
{
    /// \class ExternalComm
    /// <summary>
    /// Responsible for any communications outside of the TMS System such as the Contract Marketplace database.
    /// This class is used by the Buyer class which will be requesting Contracts from the Contract Marketplace.
    /// </summary>
    public class ExternalComm
    {
        /// <summary>
        /// This method will query the Contract Marketplace database for Contracts. 
        /// </summary>
        /// <returns>List<Contract> of the contracts from the Contract Marketplace.</returns>
        public List<Contract> GetContracts()
        {
            return new List<Contract>();
        }
    }
}
