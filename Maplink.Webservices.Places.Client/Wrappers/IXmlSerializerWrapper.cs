using System.Text;
using System.Xml.Serialization;

namespace Maplink.Webservices.Places.Client.Wrappers
{
    public interface IXmlSerializerWrapper
    {
        string Serialize(object objectToBeSerialized, Encoding encoding, XmlSerializerNamespaces namespaces);
        string Serialize(object objectToBeSerialized);
        T Deserialize<T>(string xml, Encoding encoding);
        T Deserialize<T>(string xml);
    }
}