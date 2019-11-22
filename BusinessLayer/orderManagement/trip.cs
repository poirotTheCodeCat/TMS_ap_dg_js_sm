using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TMS
{
    /// \class Trip
    /// <summary>
    /// This class represents a single trip and is used in the ordering/planning process.
    /// Trip requires the existence of an Order object to exist. Trips are completed by
    /// Carriers. 
    /// </summary>
    public class Trip
    {
        private DateTime startTime;///< The start date and time of the trip
        private DateTime endTime;///< The end date and time of the trip
        private double elapsedTime;///< Total elapsed trip time
        private double distanceTraveled;///< Total trip traveled
        private int numOfIntermediaryCities;///< Number of intermediary cities in trip
        private bool isComplete;///< True if order if complete and false if not complete

        /// <summary>
        /// Get and set startTime  
        /// </summary>
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        /// <summary>
        /// Gets and sets endTime
        /// </summary>
        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        /// <summary>
        /// Gets and sets elapsedTime
        /// </summary>
        public double ElapsedTime
        {
            get { return elapsedTime; }
            set { elapsedTime = value; }
        }

        /// <summary>
        /// Gets and sets distanceTraveled
        /// </summary>
        public double DistanceTraveled
        {
            get { return distanceTraveled; }
            set { distanceTraveled = value; }
        }

        /// <summary>
        /// Gets and sets numOfIntermediaryCities
        /// </summary>
        public int NumOfIntermediaryCities
        {
            get { return numOfIntermediaryCities; }
            set { numOfIntermediaryCities = value; }
        }

        /// <summary>
        /// Gets and sets isComplete
        /// </summary>
        public bool IsComplete
        {
            get { return isComplete; }
            set { isComplete = value; }
        }
    }
}
