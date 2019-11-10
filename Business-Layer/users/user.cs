using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_ap_dg_js_sm.UI_Layer
{
    class user
    {
        private string userID;
        private string password;
        private string username;
        private string userType;

        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public string UserType
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
         public void getUserInfo()
        {

        }

        /*
         * Function :
         * Parameters :
         * Description : 
         * Returns : 
         */
         public void VerifyLogin()
        {

        }
    }
}
