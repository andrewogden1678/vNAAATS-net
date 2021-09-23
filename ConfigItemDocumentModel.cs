using System;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace vNAAATS.NET {
    // Model to permit updating of data
    public class ConfigItemDocument : Document {
        // Id
        public string id 
        { 
            get 
            {
                return this.GetPropertyValue<string>("id");
            }

            set 
            {
                this.SetPropertyValue("id", value);
            }
        }
        // Content
        public string content 
        { 
            get 
            {
                return this.GetPropertyValue<string>("content");
            }

            set 
            {
                this.SetPropertyValue("content", value);
            }
        }
    }
}