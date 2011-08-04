using Maplink.Webservices.Places.Client.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace Maplink.Webservices.Places.Client.UnitTests.Resources
{
    [TestClass]
    public class PlaceTest
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
                    .Deserialize<Webservices.Places.Client.Resources.Places>(placesResourceInXml);

            placesDeserialized.All.Should().Have.Count.EqualTo(2);
        }
    }
}
