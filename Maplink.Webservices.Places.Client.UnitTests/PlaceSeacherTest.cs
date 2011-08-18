using System.Globalization;
using Maplink.Webservices.Places.Client.Builders;
using Maplink.Webservices.Places.Client.Converters;
using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpTestsEx;

namespace Maplink.Webservices.Places.Client.UnitTests
{
    [TestClass]
    public class PlaceSeacherTest
    {
        private IPlaceSearcher _provider;
        private Mock<IPlacesSearchRetriever> _mockedRetriever;
        private Mock<IPlacesConverter> _mockedConverter;
        private Mock<ISearchRequestBuilder> _mockedSearchRequestBuilder;
        private PlaceSearchResult _placeSearchResult;
        private Client.Resources.Places _retrievedPlaces;
        private PlaceSearchPaginationRequest _aPaginationRequest;
        private SearchRequest _aSearchRequest;
        private LicenseInfo _aLicenseInfo;
        private const double Radius = 3.0;
        private const double Latitude = -23.45;
        private const double Longitude = -43.56;
        private readonly CultureInfo _unitedStatesCultureInfo = CultureInfo.GetCultureInfo("en-us");

        [TestInitialize]
        public void SetUp()
        {
            _aSearchRequest = new SearchRequest();
            _aPaginationRequest = new PlaceSearchPaginationRequest();
            _retrievedPlaces = new Client.Resources.Places();
            _placeSearchResult = new PlaceSearchResult();
            _aLicenseInfo = new LicenseInfo { Login = "login", Key = "key" };

            _mockedSearchRequestBuilder = new Mock<ISearchRequestBuilder>();
            _mockedRetriever = new Mock<IPlacesSearchRetriever>();
            _mockedConverter = new Mock<IPlacesConverter>();

            _provider = new PlaceSearcher(
                _mockedRetriever.Object,
                _mockedConverter.Object,
                _mockedSearchRequestBuilder.Object);
        }

        [TestMethod]
        public void ShouldRetrievePlacesByRadius()
        {
            GivenTheSearchRequestWasBuilt()
                .GivenThePlacesWereRetrieved()
                .AndThePlacesWereConverted();

            _provider.ByRadius(_aLicenseInfo, Radius, Latitude, Longitude).Should().Be.EqualTo(_placeSearchResult);
        }

        [TestMethod]
        public void ShouldBuildSearchRequestForRadiusSearchWithLicenseInfo()
        {
            GivenTheSearchRequestWasBuilt();
            _provider.ByRadius(_aLicenseInfo, Radius, Latitude, Longitude);
            _mockedSearchRequestBuilder
                .Verify(it=> it.WithLicenseInfo(_aLicenseInfo.Login, _aLicenseInfo.Key), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSearchRequestForRadiusSearchWithUriPath()
        {
            GivenTheSearchRequestWasBuilt();
            _provider.ByRadius(_aLicenseInfo, Radius, Latitude, Longitude);
            _mockedSearchRequestBuilder
                .Verify(it => it.WithUriPath("places/byradius"), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSearchRequestForRadiusSearchWithStartIndex()
        {
            GivenTheSearchRequestWasBuilt();
            _provider.ByRadius(_aLicenseInfo, Radius, Latitude, Longitude);
            _mockedSearchRequestBuilder
                .Verify(it => it.WithStartIndex(0), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSearchRequestForRadiusSearchWithRadiusArgument()
        {
            GivenTheSearchRequestWasBuilt();
            _provider.ByRadius(_aLicenseInfo, Radius, Latitude, Longitude);
            _mockedSearchRequestBuilder
                .Verify(it => it.WithArgument(
                    "radius", 
                    Radius.ToString(_unitedStatesCultureInfo)), 
                    Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSearchRequestForRadiusSearchWithLatitudeArgument()
        {
            GivenTheSearchRequestWasBuilt();
            _provider.ByRadius(_aLicenseInfo, Radius, Latitude, Longitude);
            _mockedSearchRequestBuilder
                .Verify(it => it.WithArgument(
                    "latitude", 
                    Latitude.ToString(_unitedStatesCultureInfo)), 
                    Times.Once());
        }
        [TestMethod]
        public void ShouldBuildSearchRequestForRadiusSearchWithLongitudeArgument()
        {
            GivenTheSearchRequestWasBuilt();
            _provider.ByRadius(_aLicenseInfo, Radius, Latitude, Longitude);
            _mockedSearchRequestBuilder
                .Verify(it => it.WithArgument(
                    "longitude", 
                    Longitude.ToString(_unitedStatesCultureInfo)), 
                    Times.Once());
        }
        
        [TestMethod]
        public void ShouldBuildSearchRequestForRadiusSearch()
        {
            GivenTheSearchRequestWasBuilt();
            _provider.ByRadius(_aLicenseInfo, Radius, Latitude, Longitude);
            _mockedSearchRequestBuilder
                .Verify(it => it.Build(), Times.Once());
        }

        [TestMethod]
        public void ShouldRetrieveResourcesWhenSearchingByRadius()
        {
            GivenTheSearchRequestWasBuilt();

            _provider.ByRadius(_aLicenseInfo, Radius, Latitude, Longitude);
            _mockedRetriever.Verify(it => it.RetrieveFrom(_aSearchRequest), Times.Once());
        }


        [TestMethod]
        public void ShouldConvertToEntityWhenSearchingByRadius()
        {
            GivenTheSearchRequestWasBuilt()
                .GivenThePlacesWereRetrieved()
                .AndThePlacesWereConverted();

            _provider.ByRadius(_aLicenseInfo, Radius, Latitude, Longitude);
            _mockedConverter.Verify(it => it.ToEntity(_retrievedPlaces), Times.Once());
        }

        [TestMethod]
        public void ShouldRetrievePlacesByRadiusForPaginationRequest()
        {
            GivenThePlacesWereRetrieved()
                .AndThePlacesWereConverted();

            _provider.ByRadius(_aPaginationRequest).Should().Be.SameInstanceAs(_placeSearchResult);
        }

        [TestMethod]
        public void ShouldRetrieveResourcesWhenSearchingByRadiusForPaginationRequest()
        {
            _provider.ByRadius(_aPaginationRequest);
            _mockedRetriever.Verify(it => it.RetrieveFrom(_aPaginationRequest), Times.Once());
        }

        [TestMethod]
        public void ShouldConvertToEntityWhenSearchingByRadiusForPaginationRequest()
        {
            GivenThePlacesWereRetrieved()
                .AndThePlacesWereConverted();

            _provider.ByRadius(_aPaginationRequest);
            _mockedConverter.Verify(it => it.ToEntity(_retrievedPlaces), Times.Once());
        }


        private void AndThePlacesWereConverted()
        {
            _mockedConverter
                .Setup(it => it.ToEntity(It.IsAny<Client.Resources.Places>()))
                .Returns(_placeSearchResult);
        }

        private PlaceSeacherTest GivenThePlacesWereRetrieved()
        {
            _mockedRetriever
                .Setup(it => it.RetrieveFrom(It.IsAny<SearchRequest>()))
                .Returns(_retrievedPlaces);
            _mockedRetriever
                .Setup(it => it.RetrieveFrom(It.IsAny<PlaceSearchPaginationRequest>()))
                .Returns(_retrievedPlaces);
            return this;
        }

        private PlaceSeacherTest GivenTheSearchRequestWasBuilt()
        {
            _mockedSearchRequestBuilder
                .Setup(it => it.WithUriPath(It.IsAny<string>()))
                .Returns(_mockedSearchRequestBuilder.Object);
            _mockedSearchRequestBuilder
                .Setup(it => it.WithLicenseInfo(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_mockedSearchRequestBuilder.Object);
            _mockedSearchRequestBuilder
                .Setup(it => it.WithStartIndex(It.IsAny<int>()))
                .Returns(_mockedSearchRequestBuilder.Object);
            _mockedSearchRequestBuilder
                .Setup(it => it.WithArgument(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_mockedSearchRequestBuilder.Object);
            _mockedSearchRequestBuilder
                .Setup(it => it.Build())
                .Returns(_aSearchRequest);

            return this;
        }
    }
}
