using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_ap_dg_js_sm
{
    class Controller
    {
        private bool isConnected;
        private string lastCommand;
        private string lastAnswer;
        private string userType;

        public bool IsConnected     // mutator and accessor for isConnectd
        {
            get { return isConnected; }
            set { isConnected = value; }
        }

        public string LastCommand       // mutator and accessor for lastCommand
        {
            get { return lastCommand; }
            set { lastCommand = value; }
        }

        public string LastAnswer            // mutator and accessor for lastAnswer
        {
            get { return lastAnswer; }
            set { lastAnswer = value; }
        }

        public string UserType          // mutator and accessor for userType
        {
            get { return userType; }
            set { userType = value; }
        }

        /*
         * Function :
         * Parameters :
         * Description : 
         * Returns : 
         */
        public void ConnectToDB()
        {

        }

        /*
         * Function :
         * Parameters :
         * Description : 
         * Returns : 
         */
        public void ShowActiveOrders()
        {

        }

        /*
         * Function :
         * Parameters :
         * Description : 
         * Returns : 
         */
        public void ShowCompletedOrders()
        {

        }

        /*
         * Function :
         * Parameters :
         * Description : 
         * Returns : 
         */
        public void UpdateIP()
        {

        }

        /*
         * Function :
         * Parameters :
         * Description : 
         * Returns : 
         */
        public void UpdatePort()
        {

        }

        /*
         * Function :
         * Parameters :
         * Description : 
         * Returns : 
         */
        public void UpdateCarrier()
        {

        }

        /*
         * Function :
         * Parameters :
         * Description : 
         * Returns : 
         */
        public void UpdateRateTable()
        {

        }

        /*
         * Function :
         * Parameters :
         * Description : 
         * Returns : 
         */
        public void UpdateRoutTable()
        {

        }

        /*
         * Function :
         * Parameters :
         * Description : 
         * Returns : 
         */
        public void ValidateLogin()
        {

        }

        /*
         * Function :
         * Parameters :
         * Description : 
         * Returns : 
         */
        public void SendAnswer()
        {

        }
    }
}
