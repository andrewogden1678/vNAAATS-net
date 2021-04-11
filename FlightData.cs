using System;
using Newtonsoft.Json;

namespace vNAAATS.API {
    public class FlightData {
        // Callsign
        public string callsign { get;  set; } 
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
        // ADS-B Equipment status
        public bool isEquipped { get; set; }
        // Current controller tracking
        public string trackedBy { get; set; }
        // Last updated
        public DateTime lastUpdated { get; set; }
    }
}