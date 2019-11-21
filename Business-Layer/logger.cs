using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace TMS_ap_dg_js_sm.Business_Layer.users
{
    /// <summary>
    /// Logs program events and exceptions into a text file.
    /// </summary>
    class Logger
    {
        private string uName;
        //private DateTime loginDateTime;
        //private DateTime logoutDateTime;
        //private string logFileLocation;

        //create a path variable to the location of the service exe
        private static string pathString = AppDomain.CurrentDomain.BaseDirectory + "TMSLog.txt";

        //public logger(string userName)
        //{
        //    uName = userName;
        //    //get log save location from the config file
        //    //logFileLocation = ConfigurationManager.AppSettings.Get("logLocation");
        //}

        /// <summary>
        /// Writes any amount of text to a text file along with the time and date of the input.
        /// If log is an exception(via ex = 1) then also print a stack trace. File logged to
        /// TSMLog.txt in same location as exe.
        /// </summary>
        /// <param name="message">Log information.</param>
        /// <param name="ex">Bool to tell if exception or just basic log.</param>

        public static void Log(string message, int ex = 0)
        {
            string[] lines;

            //generate the input string -> takes multiple lines
            if (ex == 0)
            {
                string[] buildLine =
                {
                    DateTime.Now.ToString("dd/mm/yyyy hh:mm:ss ->\n"), message + "\n",
                    "----------------------------------------------------\n\n"
                };
                lines = buildLine;
            }
            else
            {
                string[] buildLine =
                {
                    DateTime.Now.ToString("dd/mm/yyyy hh:mm:ss ->\n"), message + "\n\n", 
                    "StackTrace\n" + StackTraceFrame() + "\n",
                    "----------------------------------------------------\n\n"
                };
                lines = buildLine;
            }

            //if file does not exist then create it
            if (!System.IO.File.Exists(pathString))
            {
                var newFile = System.IO.File.Create(pathString);
                newFile.Close();
            }

            //else open file and write each line to it
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(pathString, true))
            {
                foreach (var line in lines)
                {
                    file.Write(line);
                }
            }
            return;
        }

        /// <summary>
        /// Get a copy of the current stack trace to help in identifying errors.
        /// </summary>
        /// <returns>A string of the current stack trace.</returns>

        public static string StackTraceFrame()
        {
            //get current stack trace
            string stackTrace = Environment.StackTrace;
            return stackTrace;
        }

        /// <summary>
        /// Read all lines form a text file for display in a WPF UI location.
        /// </summary>

        public void DisplayLogs()
        {
            //read from file
            string line;

            // Read the file and display it line by line.  
            System.IO.StreamReader file = 
                new System.IO.StreamReader(pathString);
            while ((line = file.ReadLine()) != null)
            {
                //write to WPF UI location
                //System.Console.WriteLine(line);
                //->>>>>>>>>>>>>nameOfWPFTextBox.Text = line;
            }

            file.Close();
        }
    }
}
