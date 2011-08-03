using System.Collections.Generic;
using System.Xml.Serialization;

namespace Maplink.Webservices.Places.Client.Resources
{
    [XmlRoot(ElementName = "places")]
    public class Places
    {
        public Places()
        {
            All = new List<Place>();
        }

        [XmlElement(ElementName = "place")]
        public List<Place> All { get; set; }
    }
}
