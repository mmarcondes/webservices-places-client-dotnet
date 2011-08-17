using System.Linq;
using Maplink.Webservices.Places.Client.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace Maplink.Webservices.Places.Client.UnitTests.Builders
{
    [TestClass]
    public class SearchRequestBuilderTest
    {
        private ISearchRequestBuilder _searchRequestBuilder;

        [TestInitialize]
        public void SetUp()
        {
            _searchRequestBuilder = new SearchRequestBuilder();            
        }

        [TestMethod]
        public void ShouldBuildSearchRequestWithUriPath()
        {
            var requestBuilt = _searchRequestBuilder.WithUriPath("places/byradius").Build();

            requestBuilt.UriPath.Should().Be.EqualTo("places/byradius");
        }

        [TestMethod]
        public void ShouldBuildSearchRequestWithStartIndex()
        {
            var requestBuilt = _searchRequestBuilder.WithStartIndex(10).Build();

            requestBuilt.StartsAtIndex.Should().Be.EqualTo(10);
        }

        [TestMethod]
        public void ShouldBuildSearchRequestWithLicenseInfo()
        {
            var requestBuilt = _searchRequestBuilder.WithLicenseInfo("login", "key").Build();

            requestBuilt.LicenseLogin.Should().Be.EqualTo("login");
            requestBuilt.LicenseKey.Should().Be.EqualTo("key");
        }

        [TestMethod]
        public void ShouldBuildSearchRequestWithArguments()
        {
            var requestBuilt = _searchRequestBuilder
                .WithArgument("latitude", "-23.45")
                .WithArgument("longitude", "-43.56")
                .Build();

            requestBuilt.Arguments.Should().Have.Count.EqualTo(2);
            requestBuilt.Arguments.First().Key.Should().Be.EqualTo("latitude");
            requestBuilt.Arguments.First().Value.Should().Be.EqualTo("-23.45");
            requestBuilt.Arguments.Last().Key.Should().Be.EqualTo("longitude");
            requestBuilt.Arguments.Last().Value.Should().Be.EqualTo("-43.56");
        }
    }
}
