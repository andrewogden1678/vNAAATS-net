namespace vNAAATS.NET.Models
{
    /// <summary>
    /// NAT fix (including coordinates)
    /// </summary>
    public class Fix
    {
        /// <summary>
        /// Name of fix
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Latitude of fix
        /// </summary>
        public double latitude { get; set; }

        /// <summary>
        /// Longitude of fix
        /// </summary>
        public double longitude { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="lat">Latitude</param>
        /// <param name="lon">Longitude</param>
        public Fix(string name, double lat = 0.0, double lon = 0.0)
        {
            this.name = name;
            this.latitude = lat;
            this.longitude = lon;
        }
    }

    /// <summary>
    /// NAT fix to submit to database (including coordinates)
    /// </summary>
    public class DBFix
    {
        /// <summary>
        /// Name of fix
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Latitude of fix
        /// </summary>
        public string latitude { get; set; }

        /// <summary>
        /// Longitude of fix
        /// </summary>
        public string longitude { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="lat">Latitude</param>
        /// <param name="lon">Longitude</param>
        public DBFix(string name, string lat = "0.0", string lon = "0.0")
        {
            this.name = name;
            this.latitude = lat;
            this.longitude = lon;
        }
    }
}