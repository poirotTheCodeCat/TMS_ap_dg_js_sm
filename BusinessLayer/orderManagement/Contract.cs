using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_ap_dg_js_sm
{
    public class Contract
    {
        private string cName;
        private int jType;
        private int vType;
        private int Quant;
        private string Orig;
        private string Dest;


        public Contract(string NewContract)
        {
            string[] words = NewContract.Split(',');

            cName = words[0];
            //Potentially implement try/catch block.
            jType = Int32.Parse(words[1]);
            vType = Int32.Parse(words[2]);
            Quant = Int32.Parse(words[3]);
            Orig = words[4];
            Dest = words[5];
        }
    }
}
