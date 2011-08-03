using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Maplink.Webservices.Places.Client.Wrappers
{
    public class XmlSerializerWrapper : IXmlSerializerWrapper
    {
        public string Serialize(object objectToBeSerialized, Encoding encoding, XmlSerializerNamespaces namespaces)
        {
            var xmlSerializer = new XmlSerializer(objectToBeSerialized.GetType());

            using (var memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(
                    memoryStream,
                    objectToBeSerialized,
                    namespaces);

                return encoding.GetString(memoryStream.ToArray());
            }
        }

        public string Serialize(object objectToBeSerialized)
        {
            var xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add(String.Empty, String.Empty);

            return Serialize(objectToBeSerialized, Encoding.UTF8, xmlSerializerNamespaces);
        }

        public T Deserialize<T>(string xml, Encoding encoding)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var memoryStream = new MemoryStream(encoding.GetBytes(xml)))
            {
                return (T)xmlSerializer.Deserialize(memoryStream);
            }
        }

        public T Deserialize<T>(string xml)
        {
            return Deserialize<T>(xml, Encoding.UTF8);
        }
    }
}
