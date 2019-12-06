using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Business_Layer.users;
using TMS_ap_dg_js_sm.BusinessLayer.orderManagement;

namespace TMS
{
    /// \class Contract
    /// <summary>
    /// This class represents a Contract object for the TMS system. Contract is used in the Buyer class.
    /// </summary>
    /// <exception cref="fileIoException">Exception during file reading or writing in all functions</exception>
    public class Contract
    {
        private int contractId;///< Holds the Contract Id
        private string cName;///< Holds the Client name as a string
        private int jType;///< Holds the job type as a string
        private int vType;///< Holds the van type as an integer
        private int quant;///< Holds the quantity of vans available
        private string origin;///< Holds origin city
        private string dest;///< Holds the destination city


        /// <summary>
        /// Get and set contractId  
        /// </summary>
        /// 
        public int ContractId
        {
            get { return contractId; }
            set { contractId = value; }
        }
        /// <summary>
        /// Get and set cName  
        /// </summary>
        /// 
        public string ClientName
        {
            get { return cName; }
            set { cName = value; }
        }

        /// <summary>
        /// Gets and sets             get { return jType; }

        /// </summary>
        public int JobType
        {
            get { return jType; }
            set { jType = value; }
        }

        /// <summary>
        /// Gets and sets vType
        /// </summary>
        public int VanType
        {
            get { return vType; }
            set { vType = value; }
        }

        /// <summary>
        /// Gets and sets quant
        /// </summary>
        public int Quantity
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
        public string Destination
        {
            get { return dest; }
            set { dest = value; }
        }

        private List<Contract> CurrentContracts;
        public List<Contract> SingleContracts;
        public CombineContracts CombineContracts;




        /// <summary>
        /// This method will allow the Buyer to get contracts from the Contract Marketplace database. 
        /// </summary>
        /// <returns>Listmof the Contracts received from the Contract Marketplace.</returns>
        public List<Contract> GetContracts()
        {
            List<Contract> contractList = new ExternalComm().GetContracts();
            return contractList;
        }

        /// <summary>
        /// This method will allow the Buyer to get contracts from the Contract Marketplace database. 
        /// </summary>
        /// <returns>Listmof the Contracts received from the Contract Marketplace.</returns>
        public List<Contract> SortContracts()
        {
            
            List<Contract> contractList = new ExternalComm().GetContracts();
            return contractList;
        }

    }
}
