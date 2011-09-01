using Maplink.Webservices.Places.Client.Entities;

namespace Maplink.Webservices.Places.Client.Arguments
{
    public class PlaceSearchRequest
    {
        public LicenseInfo LicenseInfo { get; set; }
        public double Radius { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Term { get; set; }
        public int CategoryId { get; set; }
        public int StartIndex { get; set; }
    }
}