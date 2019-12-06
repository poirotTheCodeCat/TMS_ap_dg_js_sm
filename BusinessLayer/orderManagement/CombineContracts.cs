using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS;

namespace TMS_ap_dg_js_sm.BusinessLayer.orderManagement
{
    public class CombineContracts
    {
       
        public int JobType { get; set; }

        public int VanType { get; set; }
        public int Quantity { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public List<Contract> ContractList { get; set; }


    }
}
