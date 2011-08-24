using Maplink.Webservices.Places.Client.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace Maplink.Webservices.Places.Client.UnitTests.Builders
{
    [TestClass]
    public class PlaceSearchPaginationRequestBuilderTest
    {
        private PlaceSearchPaginationRequestBuilder placeSearchPaginationRequestBuilder;

        [TestInitialize]
        public void SetUp()
        {
            placeSearchPaginationRequestBuilder = new PlaceSearchPaginationRequestBuilder();
        }

        [TestMethod]
        public void ShouldBuildAPlaceSearchPaginationRequestWithLicenseInfo()
        {
            var placeSearchPaginationRequest = placeSearchPaginationRequestBuilder
                .WithLicenseInfo("login", "key")
                .Build();

            placeSearchPaginationRequest.Key.Should().Be.EqualTo("key");
            placeSearchPaginationRequest.Login.Should().Be.EqualTo("login");
        }

        [TestMethod]
        public void ShouldBuildAPlaceSearchPaginationRequestWithUri()
        {
            var placeSearchPaginationRequest = placeSearchPaginationRequestBuilder
                .WithUri("uri")
                .Build();

            placeSearchPaginationRequest.UriPathAndQuery.Should().Be.EqualTo("uri");
        }
    }
}
