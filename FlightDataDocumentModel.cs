using System;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace vNAAATS.API {
    // Model to permit updating of data
    public class FlightDataDocument : Document {
        // Callsign
        public string callsign 
        { 
            get 
            {
                return this.GetPropertyValue<string>("callsign");
            }

            set 
            {
                this.SetPropertyValue("callsign", value);
            }
        } 
        // Assigned level
        public int assignedLevel 
        { 
            get 
            {
                return this.GetPropertyValue<int>("assignedLevel");
            }

            set 
            {
                this.SetPropertyValue("assignedLevel", value);
            }
        } 
        // Assigned speed
        public int assignedMach 
        { 
            get 
            {
                return this.GetPropertyValue<int>("assignedMach");
            }

            set 
            {
                this.SetPropertyValue("assignedMach", value);
            }
        } 
        // Track
        public string track 
        { 
            get 
            {
                return this.GetPropertyValue<string>("track");
            }

            set 
            {
                this.SetPropertyValue("track", value);
            }
        } 
        // Route
        public string route 
        { 
            get 
            {
                return this.GetPropertyValue<string>("route");
            }

            set 
            {
                this.SetPropertyValue("route", value);
            }
        } 
        // Departure airport
        public string departure 
        { 
            get 
            {
                return this.GetPropertyValue<string>("departure");
            }

            set 
            {
                this.SetPropertyValue("departure", value);
            }
        } 
        // Arrival airport
        public string arrival 
        { 
            get 
            {
                return this.GetPropertyValue<string>("arrival");
            }

            set 
            {
                this.SetPropertyValue("arrival", value);
            }
        } 
        // ADS-B Equipment status
        public bool isEquipped 
        { 
            get 
            {
                return this.GetPropertyValue<bool>("isEquipped");
            }

            set 
            {
                this.SetPropertyValue("isEquipped", value);
            }
        } 
        // Current controller tracking
        public string trackedBy 
        { 
            get 
            {
                return this.GetPropertyValue<string>("trackedBy");
            }

            set 
            {
                this.SetPropertyValue("trackedBy", value);
            }
        } 
        // Last updated
        public DateTime lastUpdated 
        { 
            get 
            {
                return this.GetPropertyValue<DateTime>("lastUpdated");
            }

            set 
            {
                this.SetPropertyValue("lastUpdated", value);
            }
        } 
    }
}