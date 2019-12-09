/*
 * File Name:TransportCorridor
 * Program Name: TMS_ap_dg_js_sm
 * Programmers: Arron Perry, Daniel Grew, John Stanley, Sasha Malesevic
 * First Version: 2019-12-09
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS
{
    public class TransportCorridor
    {
        // private data members
        private string transportCorridorID;
        private string cityName;
        private int distance;
        private double timeBetween;
        private string west;
        private string east;
        private List<TransportCorridor> routes = new List<TransportCorridor>();

        // accessors and mutators
        public string TransportCorridorID
        {
            get { return transportCorridorID; }
            set { transportCorridorID = value; }
        }
        public string CityName
        {
            get { return cityName; }
            set { cityName = value; }
        }
        public int Distance
        {
            get { return distance; }
            set { distance = value; }
        }
        public double TimeBetween
        {
            get { return timeBetween; }
            set { timeBetween = value; }
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

        public List<TransportCorridor> Routes
        {
            get { return routes; }
            set { routes = value; }
        }

        
    }
}