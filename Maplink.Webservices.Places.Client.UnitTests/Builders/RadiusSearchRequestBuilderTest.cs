using Maplink.Webservices.Places.Client.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace Maplink.Webservices.Client.Places.UnitTests.Builders
{
    [TestClass]
    public class RadiusSearchRequestBuilderTest
    {
        [TestMethod]
        public void ShouldBuildARadiusSearchRequest()
        {
            var requestBuilt = new RadiusSearchRequestBuilder()
                .WithKey("key")
                .WithLogin("login")
                .WithLatitude(23.435)
                .WithLongitude(-46.235)
                .WithRadius(10)
                .Build();

            requestBuilt.Key.Should().Be.EqualTo("key");
            requestBuilt.Login.Should().Be.EqualTo("login");
            requestBuilt.Latitude.Should().Be.EqualTo(23.435);
            requestBuilt.Longitude.Should().Be.EqualTo(-46.235);
            requestBuilt.Radius.Should().Be.EqualTo(10);
        }
    }
}
