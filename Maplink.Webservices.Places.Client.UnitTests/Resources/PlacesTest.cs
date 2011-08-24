using Maplink.Webservices.Places.Client.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace Maplink.Webservices.Places.Client.UnitTests.Resources
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
            const string placesResourceInXml = 
                "<places><count></count><place><id>42</id></place><place><id>43</id></place></places>";

            var placesDeserialized = 
                _serializer
                    .Deserialize<Client.Resources.Places>(placesResourceInXml);

            placesDeserialized.Retrieved.Should().Have.Count.EqualTo(2);
        }
    }
}
