﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Business_Layer.orderManagement;

namespace TMS.Data_Access
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
            List<Contract> contracts = null;
            return contracts;
        }
    }
}
