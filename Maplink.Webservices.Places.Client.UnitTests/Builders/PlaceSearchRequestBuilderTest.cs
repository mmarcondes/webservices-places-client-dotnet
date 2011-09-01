using Maplink.Webservices.Places.Client.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpTestsEx;

namespace Maplink.Webservices.Places.Client.UnitTests.Builders
{
    [TestClass]
    public class PlaceSearchRequestBuilderTest
    {
        private IPlaceSearchRequestBuilder _builder;

        [TestInitialize]
        public void SetUp()
        {
            _builder = new PlaceSearchRequestBuilder();
        }

        [TestMethod]
        public void ShouldBuildARequestForLicense()
        {
            var requestBuilt = _builder.ForLicense("login", "key").Build();
            requestBuilt.LicenseInfo.Key.Should().Be.EqualTo("key");
            requestBuilt.LicenseInfo.Login.Should().Be.EqualTo("login");
        }

        [TestMethod]
        public void ShouldErasePreviousBuildWhenBuildingAnotherRequest()
        {
            var requestBuilt = _builder.ForLicense("login", "key").Build();

            var anotherRequestBuilt = _builder.ForLicense("login", "key").Build();

            requestBuilt.Should().Not.Be.SameInstanceAs(anotherRequestBuilt);
        }

        [TestMethod]
        public void ShouldBuildARequestBasedOnRadius()
        {
            var requestBuilt = _builder.BasedOnRadius(23.4, 25.352, 43.235).Build();

            requestBuilt.Radius.Should().Be.EqualTo(23.4);
            requestBuilt.Latitude.Should().Be.EqualTo(25.352);
            requestBuilt.Longitude.Should().Be.EqualTo(43.235);
        }

        [TestMethod]
        public void ShouldBuildARequestFilteredByTerm()
        {
            _builder
                .FilteredByTerm("term").Build()
                .Term.Should().Be.EqualTo("term");
        }

        [TestMethod]
        public void ShouldBuildARequestFilteredByCategory()
        {
            _builder
                .FilteredByCategory(42).Build()
                .CategoryId.Should().Be.EqualTo(42);
        }

        [TestMethod]
        public void ShouldBuildARequestStartingAtIndex()
        {
            _builder
                .StartingAtIndex(32).Build()
                .StartIndex.Should().Be.EqualTo(32);
        }
    }
}
