using Maplink.Webservices.Places.Client.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maplink.Webservices.Client.Places.UnitTests.Resources
{
    [TestClass]
    public class PlacesTest
    {
        private IXmlSerializerWrapper _serializer;

        [TestInitialize]
        public void SetUp()
        {
            _serializer = new XmlSerializerWrapper();
        }

        [TestMethod]
        public void ShouldBeDeserialized()
        {
        }
    }
}
