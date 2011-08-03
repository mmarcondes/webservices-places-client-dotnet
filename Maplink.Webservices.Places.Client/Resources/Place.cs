using System.Xml.Serialization;

namespace Maplink.Webservices.Places.Client.Resources
{
    [XmlRoot(ElementName = "place")]
    public class Place
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("address")]
        public string Address { get; set; }

        [XmlElement("district")]
        public string District { get; set; }

        [XmlElement("city")]
        public string City { get; set; }

        [XmlElement("state")]
        public string State { get; set; }

        [XmlElement("country")]
        public string Country { get; set; }

        [XmlElement("zip-code")]
        public string ZipCode { get; set; }

        [XmlElement("phone")]
        public string PrimaryPhone { get; set; }

        [XmlElement("secondary-phone")]
        public string SecondaryPhone { get; set; }

        [XmlElement("category")]
        public string Category { get; set; }

        [XmlElement("sub-category")]
        public string SubCategory { get; set; }

        [XmlElement("latitude")]
        public double Latitude { get; set; }

        [XmlElement("longitude")]
        public double Longitude { get; set; }

        [XmlElement("distance")]
        public double Distance { get; set; }
    }
}
