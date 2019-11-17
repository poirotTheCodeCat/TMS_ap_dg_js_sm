using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS_ap_dg_js_sm.Business_Layer.users
{
    /// <summary>
    /// This Login class is used to store and pass basic information about a user during the login process
    /// The class also contains a method that calls a function in the data access layer to retrieve the basic 
    /// login information about a user
    /// </summary>
    class loginInfo
    {
        private string username;
        private string password;
        private bool usernameValid;
        private bool passwordValid;
        private string userType;
    }
}
