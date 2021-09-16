using System;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace vNAAATS.NET {
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
        // Aircraft type
        public string type
        {
            get
            {
                return this.GetPropertyValue<string>("type");
            }
            set
            {
                this.SetPropertyValue("type", value);
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
        // Route ETAs
        public string routeEtas
        { 
            get 
            {
                return this.GetPropertyValue<string>("routeEtas");
            }

            set 
            {
                this.SetPropertyValue("routeEtas", value);
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
        // Direction
        public bool direction
        {
            get
            {
                return this.GetPropertyValue<bool>("direction");
            }
            set
            {
                this.SetPropertyValue("direction", value);
            }
        }
        // Etd
        public string etd
        {
            get
            {
                return this.GetPropertyValue<string>("etd");
            }

            set
            {
                this.SetPropertyValue("etd", value);
            }
        }
        // Selcal code
        public string selcal
        {
            get
            {
                return this.GetPropertyValue<string>("selcal");
            }

            set
            {
                this.SetPropertyValue("selcal", value);
            }
        }
        // Datalink connection status
        public bool connectedDatalink
        {
            get
            {
                return this.GetPropertyValue<bool>("connectedDatalink");
            }
            set
            {
                this.SetPropertyValue("connectedDatalink", value);
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
        public string state
        {
            get
            {
                return this.GetPropertyValue<string>("state");
            }

            set
            {
                this.SetPropertyValue("state", value);
            }
        }
        public bool relevant
        {
            get
            {
                return this.GetPropertyValue<bool>("relevant");
            }
            set
            {
                this.SetPropertyValue("relevant", value);
            }
        }
        // Aircraft target mode (ads-b, etc)
        public TargetMode targetMode
        {
            get
            {
                return (TargetMode) this.GetPropertyValue<int>("targetMode");
            }

            set
            {
                this.SetPropertyValue("targetMode", value);
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
        // Current controller tracking (sector ID)
        public string trackedById
        {
            get
            {
                return this.GetPropertyValue<string>("trackedById");
            }

            set
            {
                this.SetPropertyValue("trackedById", value);
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