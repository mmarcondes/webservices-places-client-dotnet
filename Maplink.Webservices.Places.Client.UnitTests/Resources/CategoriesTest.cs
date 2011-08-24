using System.Linq;
using Maplink.Webservices.Places.Client.Resources;
using Maplink.Webservices.Places.Client.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace Maplink.Webservices.Places.Client.UnitTests.Resources
{
    [TestClass]
    public class CategoriesTest
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
            const string categoriesResourceInXml = @"<categories>
    <category>
        <id>1</id>
        <name>first</name>
    </category>
    <category>
        <id>2</id>
        <name>second</name>
    </category>
</categories>";

            var categoriesDeserialized = _serializer.Deserialize<Categories>(categoriesResourceInXml);
            categoriesDeserialized.All.Count().Should().Be.EqualTo(2);

            var firstCategory = categoriesDeserialized.All.First();
            firstCategory.Id.Should().Be.EqualTo(1);
            firstCategory.Name.Should().Be.EqualTo("first");

            var secondCategory = categoriesDeserialized.All.Last();
            secondCategory.Id.Should().Be.EqualTo(2);
            secondCategory.Name.Should().Be.EqualTo("second");
        }
    }
}
