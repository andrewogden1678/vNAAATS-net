using System;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace vNAAATS.NET {
    // Model to permit updating of data
    public class WaypointDocument : Document {
        // Name
        public string name 
        { 
            get 
            {
                return this.GetPropertyValue<string>("name");
            }

            set 
            {
                this.SetPropertyValue("name", value);
            }
        }
        // Latitude
        public double latitude 
        { 
            get 
            {
                return this.GetPropertyValue<double>("name");
            }

            set 
            {
                this.SetPropertyValue("name", value);
            }
        }
        // Longitude
        public double longitude 
        { 
            get 
            {
                return this.GetPropertyValue<double>("name");
            }

            set 
            {
                this.SetPropertyValue("name", value);
            }
        }
    }
}