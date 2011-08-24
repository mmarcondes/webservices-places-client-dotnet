using Maplink.Webservices.Places.Client.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace Maplink.Webservices.Places.Client.UnitTests.Builders
{
    [TestClass]
    public class CustomRequestBuilderTest
    {
        private CustomRequestBuilder customRequestBuilder;

        [TestInitialize]
        public void SetUp()
        {
            customRequestBuilder = new CustomRequestBuilder();
        }

        [TestMethod]
        public void ShouldBuildAPlaceSearchPaginationRequestWithLicenseInfo()
        {
            var placeSearchPaginationRequest = customRequestBuilder
                .WithLicenseInfo("login", "key")
                .Build();

            placeSearchPaginationRequest.Key.Should().Be.EqualTo("key");
            placeSearchPaginationRequest.Login.Should().Be.EqualTo("login");
        }

        [TestMethod]
        public void ShouldBuildAPlaceSearchPaginationRequestWithUri()
        {
            var placeSearchPaginationRequest = customRequestBuilder
                .WithUriPathAndQuery("uri")
                .Build();

            placeSearchPaginationRequest.UriPathAndQuery.Should().Be.EqualTo("uri");
        }
    }
}
