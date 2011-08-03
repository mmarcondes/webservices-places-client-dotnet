namespace Maplink.Webservices.Places.Client.Entities
{
    public class Place
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string PrimaryPhone { get; set; }
        public string SecondaryPhone { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double DistanceInKilometers { get; set; }
    }
}
