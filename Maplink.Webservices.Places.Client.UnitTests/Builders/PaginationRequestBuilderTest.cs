using Maplink.Webservices.Places.Client.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace Maplink.Webservices.Places.Client.UnitTests.Builders
{
    [TestClass]
    public class PaginationRequestBuilderTest
    {
        private IPaginationRequestBuilder _builder;

        [TestInitialize]
        public void SetUp()
        {
            _builder = new PaginationRequestBuilder();
        }

        [TestMethod]
        public void ShouldCreateAPaginationRequestWithLicenseInfo()
        {
            var request = _builder.ForLicense("login", "key").Build();

            request.LicenseInfo.Login.Should().Be.EqualTo("login");
            request.LicenseInfo.Key.Should().Be.EqualTo("key");
        }

        [TestMethod]
        public void ShouldErasePreviousRequestWhenBuildingAnotherRequest()
        {
            var firstRequestBuiltFromSameInstance = _builder.ForLicense("login", "key").Build();
            var anotherRequestBuiltFromSameInstance = _builder.Build();

            firstRequestBuiltFromSameInstance.Should().Not.Be.SameInstanceAs(anotherRequestBuiltFromSameInstance);
        }

        [TestMethod]
        public void ShouldBuildAPaginationRequestWithUriPathAndQuery()
        {
            _builder
                .WithUriPathAndQuery("uri-path-and-query").Build()
                .UriPathAndQuery.Should().Be.EqualTo("uri-path-and-query");
        }
    }
}
