using System;

namespace Maplink.Webservices.Places.Client.Entities
{
    public class RadiusSearchRequest
    {
        public RadiusSearchRequest()
        {
            Login = String.Empty;
            Key = String.Empty;
        }

        public double Radius { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int StartsAtIndex { get; set; }
        public string Login { get; set; }
        public string Key { get; set; }
    }
}