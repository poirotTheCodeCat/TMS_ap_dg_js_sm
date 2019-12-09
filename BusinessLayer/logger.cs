using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace TMS.Business_Layer.users
{
    /// <summary>
    /// This class records exceptions that occur during use of the program. It is used in all
    /// user classes as needed in the event of an error.
    /// </summary>
    class Logger
    {
        //create a path variable to the location of the service exe
        private static string pathString = ConfigurationManager.AppSettings["logLocation"];

        /// <summary>
        /// Writes any amount of text to a text file along with the time and date of the input.
        /// If log is an exception(via ex = 1) then also print a stack trace. File logged to
        /// TSMLog.txt in same location as exe.
        /// </summary>
        /// <param name="message">Log information.</param>
        /// <param name="ex">Bool to tell if exception or just basic log.</param>
        /// <returns>void</returns>

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
    }
    /// \mainpage TMS Project      
    /// 
    ///
    /// \section intro Program Introduction
    /// <b>TMS Project</b> application is being in production and being created for OMNICorp.
    ///
    ///
    ///
    /// <hr>
    /// \section requirements Project Requirements
    /// - Track, Manage and Update transportation operations for the OMNIcorp Shipping Handling and Transportation (OSHT) system.
    ///
    /// <hr> 
    /// \todo 
    /// N/A
    /// <hr>
    /// \bug 
    /// N/A
    ///
    /// <hr>
    /// \section version Current version of the TMS Project  :
    /// <ul>
    /// <li>\authors   <b><i>The History Buffs</i></b>
    /// <li>\version   0.0</li>
    /// <li>\date      2019-11-21</li>
    /// <li>\pre       In order to use the <i>TMS Project</i> - you must be involved in the development of operations of the OSHT system</li>
    /// <li>\warning   Improper use of the <i>TMS Project</i> may result in incorrect results</li>
    /// <li>\copyright SENG2020</li>
    /// </ul>
    ///
}