namespace Maplink.Webservices.Places.Client.Entities
{
    public class RadiusSearchRequest
    {
        public int Radius { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Login { get; set; }
        public string Key { get; set; }
    }
}