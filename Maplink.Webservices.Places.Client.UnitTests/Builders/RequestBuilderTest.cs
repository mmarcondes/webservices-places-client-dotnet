using System.Linq;
using Maplink.Webservices.Places.Client.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace Maplink.Webservices.Places.Client.UnitTests.Builders
{
    [TestClass]
    public class RequestBuilderTest
    {
        private IRequestBuilder _requestBuilder;

        [TestInitialize]
        public void SetUp()
        {
            _requestBuilder = new RequestBuilder();            
        }

        [TestMethod]
        public void ShouldBuildSearchRequestWithUriPath()
        {
            var requestBuilt = _requestBuilder.WithUriPath("places/byradius").Build();

            requestBuilt.UriPath.Should().Be.EqualTo("places/byradius");
        }

        [TestMethod]
        public void ShouldBuildSearchRequestWithStartIndex()
        {
            var requestBuilt = _requestBuilder.WithStartIndex(10).Build();

            requestBuilt.StartsAtIndex.Should().Be.EqualTo(10);
        }

        [TestMethod]
        public void ShouldBuildSearchRequestWithLicenseInfo()
        {
            var requestBuilt = _requestBuilder.WithLicenseInfo("login", "key").Build();

            requestBuilt.LicenseLogin.Should().Be.EqualTo("login");
            requestBuilt.LicenseKey.Should().Be.EqualTo("key");
        }

        [TestMethod]
        public void ShouldBuildSearchRequestWithArguments()
        {
            var requestBuilt = _requestBuilder
                .WithArgument("latitude", "-23.45")
                .WithArgument("longitude", "-43.56")
                .Build();

            requestBuilt.Arguments.Should().Have.Count.EqualTo(2);
            requestBuilt.Arguments.First().Key.Should().Be.EqualTo("latitude");
            requestBuilt.Arguments.First().Value.Should().Be.EqualTo("-23.45");
            requestBuilt.Arguments.Last().Key.Should().Be.EqualTo("longitude");
            requestBuilt.Arguments.Last().Value.Should().Be.EqualTo("-43.56");
        }

        [TestMethod]
        public void ShouldBuildWithUriPathAndQuery()
        {
            var requestBuilt = _requestBuilder
                .WithUriPathAndQuery("/path?first-argument=first-value&second-argument=second-value")
                .Build();

            requestBuilt.UriPath.Should().Be.EqualTo("/path");
            requestBuilt.Arguments.Should().Have.Count.EqualTo(2);

            var firstArgument = requestBuilt.Arguments.FirstOrDefault();
            firstArgument.Key.Should().Be.EqualTo("first-argument");
            firstArgument.Value.Should().Be.EqualTo("first-value");

            var secondArgument = requestBuilt.Arguments.LastOrDefault();
            secondArgument.Key.Should().Be.EqualTo("second-argument");
            secondArgument.Value.Should().Be.EqualTo("second-value");
        }

        [TestMethod]
        public void ShouldBuildWithUriPathAndQueryWithoutQueryString()
        {
            var requestBuilt = _requestBuilder
                .WithUriPathAndQuery("/path")
                .Build();

            requestBuilt.UriPath.Should().Be.EqualTo("/path");
            requestBuilt.Arguments.Should().Have.Count.EqualTo(0);
        }

        [TestMethod]
        public void ShouldBuildWithUriPathAndQuerySettingStartingAtIndexWhenItIsOnQueryString()
        {
            var requestBuilt = _requestBuilder
                .WithUriPathAndQuery("/path?start=60")
                .Build();

            requestBuilt.UriPath.Should().Be.EqualTo("/path");
            requestBuilt.Arguments.Should().Have.Count.EqualTo(0);
            requestBuilt.StartsAtIndex.Should().Be.EqualTo(60);
        }

        [TestMethod]
        public void ShouldErasePreviousBuildWhenBuildingARequest()
        {
            _requestBuilder.WithStartIndex(10).Build();
            _requestBuilder.WithLicenseInfo("login", "key").Build().StartsAtIndex.Should().Be.EqualTo(0);
        }
    }
}
