using System.Collections.Generic;
using System.Xml.Serialization;

namespace Maplink.Webservices.Places.Client.Resources
{
    [XmlRoot(ElementName = "categories")]
    public class Categories : Resource
    {
        [XmlElement(ElementName = "category")]
        public List<Category> All { get; set; }
    }
}
