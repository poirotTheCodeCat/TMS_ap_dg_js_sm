using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mockTMS
{
    class TransportCorridor
    {
        private string transportCorridor_ID;
        private string city_Name;
        private int distance;
        private double time_Between;
        private string west;
        private string east;

        public string TransportCorridor_ID
        {
            get { return transportCorridor_ID; }
            set { transportCorridor_ID = value; }
        }
        public string City_Name
        {
            get { return city_Name; }
            set { city_Name = value; }
        }
        public int Distance
        {
            get { return distance; }
            set { distance = value; }
        }
        public double Time_Between
        {
            get { return time_Between; }
            set { time_Between = value; }
        }
        public string West
        {
            get { return west; }
            set { west = value; }
        }
        public string East
        {
            get { return east; }
            set { east = value; }
        }
    }
}