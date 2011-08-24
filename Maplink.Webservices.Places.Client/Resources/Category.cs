using System.Xml.Serialization;

namespace Maplink.Webservices.Places.Client.Resources
{
    [XmlRoot(ElementName = "category")]
    public class Category
    {
        [XmlElement(ElementName = "id")]
        public int Id { get; set; }
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
    }
}