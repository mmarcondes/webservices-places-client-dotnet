using System.Collections.Generic;
using System.Xml.Serialization;

namespace Maplink.Webservices.Places.Client.Resources
{
    [XmlRoot(ElementName = "places")]
    public class Places : Resource
    {
        public Places()
        {
            Retrieved = new List<Place>();
            Links = new List<AtomLink>();
        }

        [XmlElement(ElementName = "place")]
        public List<Place> Retrieved { get; set; }

        [XmlElement("total-found")]
        public int TotalFound { get; set; }

        [XmlElement(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
        public List<AtomLink> Links { get; set; }
    }
}
