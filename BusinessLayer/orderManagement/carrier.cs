using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_ap_dg_js_sm
{
    class Carrier
    {
        private string carrierName;
        private string depotCity;
        private int ftlAvailable;
        private int ltlAvailable;
        private double ftlRate;
        private double ltlRate;
        private int refferCharge;

        public string CarrierName
        {
            get { return carrierName; }
            set { carrierName = value; }
        }

        public string DepotCity
        {
            get { return depotCity; }
            set { depotCity = value; }
        }

        public int FtlAvailable
        {
            get { return ftlAvailable; }
            set { ftlAvailable = value; }
        }

        public int LtlAvailable
        {
            get { return ltlAvailable; }
            set { ltlAvailable = value; }
        }
        public double FtlRate
        {
            get { return ftlRate; }
            set { ftlRate = value; }
        }

        public double LtlRate
        {
            get { return ltlRate; }
            set { ltlRate = value; }
        }

        public int RefferCharge
        {
            get { return refferCharge; }
            set { refferCharge = value; }
        }

        /*
         * Function :
         * Parameters :
         * Description : 
         * Returns : 
         */
        public void GetCarrierInfo()
        {

        }
    }
}
