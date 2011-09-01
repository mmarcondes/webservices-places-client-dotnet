using System.Globalization;
using Maplink.Webservices.Places.Client.Arguments;
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
        private Mock<IResourceRetriever> _mockedRetriever;
        private Mock<IPlacesConverter> _mockedConverter;
        private Mock<IRequestBuilder> _mockedSearchRequestBuilder;
        private PlaceSearchResult _placeSearchResult;
        private Client.Resources.Places _retrievedPlaces;
        private Request _aRequest;
        private LicenseInfo _aLicenseInfo;
        private const double Radius = 3.0;
        private const double Latitude = -23.45;
        private const double Longitude = -43.56;
        private readonly CultureInfo _unitedStatesCultureInfo = CultureInfo.GetCultureInfo("en-us");
        private PlaceSearchResult _placeSearchRetrievedByProvider;
        private PlaceSearchRequest _placeSearchRequest;
        private PaginationRequest _paginationRequest;
        private const int CategoryId = 10;
        private const string Term = "term";
        private const string PaginationUri = "pagination-uri";
        private const int StartIndex = 30;

        [TestInitialize]
        public void SetUp()
        {
            _aRequest = new Request();
            _retrievedPlaces = new Client.Resources.Places();
            _placeSearchResult = new PlaceSearchResult();
            _aLicenseInfo = new LicenseInfo { Login = "login", Key = "key" };

            _mockedSearchRequestBuilder = new Mock<IRequestBuilder>();
            _mockedRetriever = new Mock<IResourceRetriever>();
            _mockedConverter = new Mock<IPlacesConverter>();

            _provider = new PlaceSearcher(
                _mockedRetriever.Object,
                _mockedConverter.Object,
                _mockedSearchRequestBuilder.Object);
        }

        [TestMethod]
        public void ShouldRetrievePlacesByRadius()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted()
                .WhenFindingByRadius();

            _placeSearchRetrievedByProvider.Should().Be.EqualTo(_placeSearchResult);
        }

        [TestMethod]
        public void ShouldBuildSearchRequestForRadiusSearchWithLicenseInfo()
        {
            GivenTheSearchRequestCanBeBuilt()
               .AndThePlacesCanBeRetrieved()
               .AndThePlacesCanBeConverted()
                .WhenFindingByRadius();

            _mockedSearchRequestBuilder
                .Verify(it=> it.WithLicenseInfo(_aLicenseInfo), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSearchRequestForRadiusSearchWithUriPath()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted()
                .WhenFindingByRadius();

            _mockedSearchRequestBuilder
                .Verify(it => it.WithUriPath("/places/byradius"), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSearchRequestForRadiusSearchWithStartIndex()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted()
                .WhenFindingByRadius();

            _mockedSearchRequestBuilder
                .Verify(it => it.WithStartIndex(StartIndex), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSearchRequestForRadiusSearchWithRadiusArgument()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted()
                .WhenFindingByRadius();

            _mockedSearchRequestBuilder
                .Verify(it => it.WithArgument(
                    "radius", 
                    Radius.ToString(_unitedStatesCultureInfo)), 
                    Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSearchRequestForRadiusSearchWithLatitudeArgument()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted()
                .WhenFindingByRadius();

            _mockedSearchRequestBuilder
                .Verify(it => it.WithArgument(
                    "latitude", 
                    Latitude.ToString(_unitedStatesCultureInfo)), 
                    Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSearchRequestForRadiusSearchWithLongitudeArgument()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted()
                .WhenFindingByRadius();

            _mockedSearchRequestBuilder
                .Verify(it => it.WithArgument(
                    "longitude", 
                    Longitude.ToString(_unitedStatesCultureInfo)), 
                    Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSearchRequestForRadiusSearchWithTermArgument()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted()
                .WhenFindingByRadius();

            _mockedSearchRequestBuilder
                .Verify(it => it.WithArgument("term", Term), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSearchRequestForRadiusSearchWithCategoryArgument()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted()
                .WhenFindingByRadius();

            _mockedSearchRequestBuilder
                .Verify(it => it.WithArgument("category", CategoryId.ToString()), Times.Once());
        }
        
        [TestMethod]
        public void ShouldBuildSearchRequestForRadiusSearch()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted()
                .WhenFindingByRadius();

            _mockedSearchRequestBuilder
                .Verify(it => it.Build(), Times.Once());
        }

        [TestMethod]
        public void ShouldRetrieveResourcesWhenSearchingByRadius()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted()
                .WhenFindingByRadius();

            _mockedRetriever.Verify(it => it.From<Client.Resources.Places>(_aRequest), Times.Once());
        }


        [TestMethod]
        public void ShouldConvertToEntityWhenSearchingByRadius()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted()
                .WhenFindingByRadius();

            _mockedConverter.Verify(it => it.ToEntity(_retrievedPlaces), Times.Once());
        }

        [TestMethod]
        public void ShouldRetrievePlacesForPaginationRequest()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted()
                .WhenSearchingByPaginationUri();

           _placeSearchRetrievedByProvider.Should().Be.SameInstanceAs(_placeSearchResult);
        }

        [TestMethod]
        public void ShouldCreateARequestWithLicenseInfoWhenSearchingForPaginationRequest()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted()
                .WhenSearchingByPaginationUri();

            _mockedSearchRequestBuilder.Verify(it => it.WithLicenseInfo(_aLicenseInfo), Times.Once());
        }

        [TestMethod]
        public void ShouldCreateARequestWithUriPathAndQueryWhenSearchingForPaginationRequest()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted()
                .WhenSearchingByPaginationUri();

            _mockedSearchRequestBuilder.Verify(it => it.WithUriPathAndQuery(PaginationUri), Times.Once());
        }

        [TestMethod]
        public void ShouldCreateARequestWhenSearchingForPaginationRequest()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted()
                .WhenSearchingByPaginationUri();

            _mockedSearchRequestBuilder.Verify(it => it.Build(), Times.Once());
        }

        [TestMethod]
        public void ShouldRetrieveResourcesWhenSearchingForPaginationRequest()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted()
                .WhenSearchingByPaginationUri();

            _mockedRetriever.Verify(it => it.From<Client.Resources.Places>(_aRequest), Times.Once());
        }

        [TestMethod]
        public void ShouldConvertToEntityWhenSearchingForPaginationRequest()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted()
                .WhenSearchingByPaginationUri();

            _mockedConverter.Verify(it => it.ToEntity(_retrievedPlaces), Times.Once());
        }

        private PlaceSeacherTest AndThePlacesCanBeConverted()
        {
            _mockedConverter
                .Setup(it => it.ToEntity(It.IsAny<Client.Resources.Places>()))
                .Returns(_placeSearchResult);

            return this;
        }

        private PlaceSeacherTest AndThePlacesCanBeRetrieved()
        {
            _mockedRetriever
                .Setup(it => it.From<Client.Resources.Places>(It.IsAny<Request>()))
                .Returns(_retrievedPlaces);
            return this;
        }

        private PlaceSeacherTest GivenTheSearchRequestCanBeBuilt()
        {
            _mockedSearchRequestBuilder
                .Setup(it => it.WithUriPath(It.IsAny<string>()))
                .Returns(_mockedSearchRequestBuilder.Object);
            _mockedSearchRequestBuilder
                .Setup(it => it.WithUriPathAndQuery(It.IsAny<string>()))
                .Returns(_mockedSearchRequestBuilder.Object);
            _mockedSearchRequestBuilder
                .Setup(it => it.WithLicenseInfo(It.IsAny<LicenseInfo>()))
                .Returns(_mockedSearchRequestBuilder.Object);
            _mockedSearchRequestBuilder
                .Setup(it => it.WithStartIndex(It.IsAny<int>()))
                .Returns(_mockedSearchRequestBuilder.Object);
            _mockedSearchRequestBuilder
                .Setup(it => it.WithArgument(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_mockedSearchRequestBuilder.Object);
            _mockedSearchRequestBuilder
                .Setup(it => it.Build())
                .Returns(_aRequest);

            return this;
        }

        private void WhenFindingByRadius()
        {
            _placeSearchRequest = new PlaceSearchRequest
                                      {
                                          CategoryId = CategoryId,
                                          Latitude = Latitude,
                                          LicenseInfo = _aLicenseInfo,
                                          Longitude = Longitude,
                                          Radius = Radius,
                                          Term = Term,
                                          StartIndex = StartIndex
                                      };

            _placeSearchRetrievedByProvider = _provider.ByRadius(_placeSearchRequest);
        }

        private void WhenSearchingByPaginationUri()
        {
            _paginationRequest = new PaginationRequest
                                     {
                                         LicenseInfo = _aLicenseInfo,
                                         UriPathAndQuery = PaginationUri
                                     };

            _placeSearchRetrievedByProvider = _provider.ByUri(_paginationRequest);
        }
    }
}
