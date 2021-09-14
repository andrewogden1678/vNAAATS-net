using System;
using Newtonsoft.Json;

namespace vNAAATS.API {
    public class FlightData {
        // Callsign
        public string callsign { get;  set; } 
        // Aircraft type
        public string type { get; set; }
        // Assigned level
        public int assignedLevel { get; set; }
        // Assigned speed
        public int assignedMach { get; set; }
        // Track
        public string track { get; set; }
        // Route
        public string route { get; set; }
        // Route
        public string routeEtas { get; set; }
        // Departure airport
        public string departure { get; set; }
        // Arrival airport
        public string arrival { get; set; }
        // Direction
        public bool direction { get; set; } // True: eastbound, False: westbound
        // Etd
        public string etd { get; set; }
        // SELCAL code
        public string selcal { get; set; }
        // Datalink connected?
        public bool datalinkConnected { get; set; }
        // ADS-B Equipment status
        public bool isEquipped { get; set; }
        // State
        public string state { get; set; }
        // Are they relevant? (i.e. not offline and will enter airspace/already in airspace
        public bool relevant { get; set; }
        // Aircraft target mode (ADS-B, radar, etc)
        public TargetMode targetMode { get; set; }
        // Current controller tracking
        public string trackedBy { get; set; }
        // Tracked by sector id
        public string trackedById { get; set; }
        // Last updated
        public DateTime lastUpdated { get; set; }
    }

    // Radar target mode
    public enum TargetMode
    {
        PRIMARY = 0,
        SECONDARY_S = 1,
        SECONDARY_C = 2,
        ADS_B = 3
    }
}