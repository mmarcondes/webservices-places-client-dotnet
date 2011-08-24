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
        private Mock<IRequestBuilder> _mockedSearchRequestBuilder;
        private PlaceSearchResult _placeSearchResult;
        private Client.Resources.Places _retrievedPlaces;
        private Request _aRequest;
        private LicenseInfo _aLicenseInfo;
        private const double Radius = 3.0;
        private const double Latitude = -23.45;
        private const double Longitude = -43.56;
        private readonly CultureInfo _unitedStatesCultureInfo = CultureInfo.GetCultureInfo("en-us");
        private const int CategoryId = 10;
        private const string Term = "term";
        private const string PaginationUri = "pagination-uri";

        [TestInitialize]
        public void SetUp()
        {
            _aRequest = new Request();
            _retrievedPlaces = new Client.Resources.Places();
            _placeSearchResult = new PlaceSearchResult();
            _aLicenseInfo = new LicenseInfo { Login = "login", Key = "key" };

            _mockedSearchRequestBuilder = new Mock<IRequestBuilder>();
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
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();

            _provider.ByRadius(_aLicenseInfo, Radius, Latitude, Longitude).Should().Be.EqualTo(_placeSearchResult);
        }

        [TestMethod]
        public void ShouldBuildSearchRequestForRadiusSearchWithLicenseInfo()
        {
            GivenTheSearchRequestCanBeBuilt()
               .AndThePlacesCanBeRetrieved()
               .AndThePlacesCanBeConverted(); 
            
            _provider.ByRadius(_aLicenseInfo, Radius, Latitude, Longitude);
            _mockedSearchRequestBuilder
                .Verify(it=> it.WithLicenseInfo(_aLicenseInfo.Login, _aLicenseInfo.Key), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSearchRequestForRadiusSearchWithUriPath()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();
            
            _provider.ByRadius(_aLicenseInfo, Radius, Latitude, Longitude);
            _mockedSearchRequestBuilder
                .Verify(it => it.WithUriPath("places/byradius"), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSearchRequestForRadiusSearchWithStartIndex()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();

            _provider.ByRadius(_aLicenseInfo, Radius, Latitude, Longitude);
            _mockedSearchRequestBuilder
                .Verify(it => it.WithStartIndex(0), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSearchRequestForRadiusSearchWithRadiusArgument()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();

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
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();
            
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
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();
            
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
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();

            _provider.ByRadius(_aLicenseInfo, Radius, Latitude, Longitude);
            _mockedSearchRequestBuilder
                .Verify(it => it.Build(), Times.Once());
        }

        [TestMethod]
        public void ShouldRetrieveResourcesWhenSearchingByRadius()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();

            _provider.ByRadius(_aLicenseInfo, Radius, Latitude, Longitude);
            _mockedRetriever.Verify(it => it.RetrieveFrom(_aRequest), Times.Once());
        }


        [TestMethod]
        public void ShouldConvertToEntityWhenSearchingByRadius()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();

            _provider.ByRadius(_aLicenseInfo, Radius, Latitude, Longitude);
            _mockedConverter.Verify(it => it.ToEntity(_retrievedPlaces), Times.Once());
        }


        [TestMethod]
        public void ShouldRetrievePlacesByTerm()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();

            _provider.ByTerm(_aLicenseInfo, Term).Should().Be.EqualTo(_placeSearchResult);
        }

        [TestMethod]
        public void ShouldBuildSearchRequestForTermSearchWithLicenseInfo()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();
            
            _provider.ByTerm(_aLicenseInfo, Term);
            _mockedSearchRequestBuilder
                .Verify(it=> it.WithLicenseInfo(_aLicenseInfo.Login, _aLicenseInfo.Key), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSearchRequestForTermSearchWithUriPath()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();

            _provider.ByTerm(_aLicenseInfo, Term);
            _mockedSearchRequestBuilder
                .Verify(it => it.WithUriPath("places/byterm"), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSearchRequestForTermSearchWithStartIndex()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();
   
            _provider.ByTerm(_aLicenseInfo, Term);
            _mockedSearchRequestBuilder
                .Verify(it => it.WithStartIndex(0), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSearchRequestForTermSearchWithTermArgument()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();

            _provider.ByTerm(_aLicenseInfo, Term);
            _mockedSearchRequestBuilder
                .Verify(it => it.WithArgument("term", Term), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSearchRequestForTermSearch()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();

            _provider.ByTerm(_aLicenseInfo, Term);
            _mockedSearchRequestBuilder
                .Verify(it => it.Build(), Times.Once());
        }

        [TestMethod]
        public void ShouldRetrieveResourcesWhenSearchingByTerm()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();

            _provider.ByTerm(_aLicenseInfo, Term);
            _mockedRetriever.Verify(it => it.RetrieveFrom(_aRequest), Times.Once());
        }


        [TestMethod]
        public void ShouldConvertToEntityWhenSearchingByTerm()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();

            _provider.ByTerm(_aLicenseInfo, Term);
            _mockedConverter.Verify(it => it.ToEntity(_retrievedPlaces), Times.Once());
        }

        [TestMethod]
        public void ShouldRetrievePlacesForPaginationRequest()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();

           _provider.ByUri(_aLicenseInfo, PaginationUri).Should().Be.SameInstanceAs(_placeSearchResult);
        }

        [TestMethod]
        public void ShouldRetrieveResourcesWhenSearchingForPaginationRequest()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();

            _provider.ByUri(_aLicenseInfo, PaginationUri);
            _mockedRetriever.Verify(it => it.RetrieveFrom(_aRequest), Times.Once());
        }

        [TestMethod]
        public void ShouldConvertToEntityWhenSearchingForPaginationRequest()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();

            _provider.ByUri(_aLicenseInfo, PaginationUri);
            _mockedConverter.Verify(it => it.ToEntity(_retrievedPlaces), Times.Once());
        }

        [TestMethod]
        public void ShouldSearchByCategory()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();

            _provider.ByCategory(_aLicenseInfo, CategoryId).Should().Be.EqualTo(_placeSearchResult);
        }

        [TestMethod]
        public void ShouldBuildSearchRequestWhenSearchingByCategory()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();

            _provider.ByCategory(_aLicenseInfo, CategoryId);
            _mockedSearchRequestBuilder.Verify(it => it.Build(), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSearchRequestWithLicenseInfoWhenSearchingByCategory()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();

            _provider.ByCategory(_aLicenseInfo, CategoryId);
            _mockedSearchRequestBuilder
                .Verify(it => it.WithLicenseInfo(_aLicenseInfo.Login, _aLicenseInfo.Key), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSearchRequestWithCategoryIdWhenSearchingByCategory()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();

            _provider.ByCategory(_aLicenseInfo, CategoryId);
            _mockedSearchRequestBuilder
                .Verify(it => it.WithArgument("categoryId", CategoryId.ToString()), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSearchRequestWithUriPathWhenSearchingByCategory()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();

            _provider.ByCategory(_aLicenseInfo, CategoryId);
            _mockedSearchRequestBuilder
                .Verify(it => it.WithUriPath("places/bycategory"), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSearchRequestWithStartIndexWhenSearchingByCategory()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();

            _provider.ByCategory(_aLicenseInfo, CategoryId);
            _mockedSearchRequestBuilder
                .Verify(it => it.WithStartIndex(0), Times.Once());
        }

        [TestMethod]
        public void ShouldRetrievePlacesWhenSearchingByCategory()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();

            _provider.ByCategory(_aLicenseInfo, CategoryId);
            _mockedRetriever.Verify(it => it.RetrieveFrom(_aRequest), Times.Once());
        }

        [TestMethod]
        public void ShouldConvertPlacesWhenSearchingByCategory()
        {
            GivenTheSearchRequestCanBeBuilt()
                .AndThePlacesCanBeRetrieved()
                .AndThePlacesCanBeConverted();

            _provider.ByCategory(_aLicenseInfo, CategoryId);
            _mockedConverter.Verify(it => it.ToEntity(_retrievedPlaces), Times.Once());
        }

        private void AndThePlacesCanBeConverted()
        {
            _mockedConverter
                .Setup(it => it.ToEntity(It.IsAny<Client.Resources.Places>()))
                .Returns(_placeSearchResult);
        }

        private PlaceSeacherTest AndThePlacesCanBeRetrieved()
        {
            _mockedRetriever
                .Setup(it => it.RetrieveFrom(It.IsAny<Request>()))
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
                .Returns(_aRequest);

            return this;
        }
    }
}
