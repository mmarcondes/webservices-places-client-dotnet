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
        private PlaceSearchResult _placeSearchResult;
        private Client.Resources.Places _retrievedPlaces;
        private PlaceSearchPaginationRequest _aPaginationRequest;
        private SearchRequest _aSearchRequest;

        [TestInitialize]
        public void SetUp()
        {
            _aSearchRequest = new SearchRequest();
            _aPaginationRequest = new PlaceSearchPaginationRequest();
            _retrievedPlaces = new Client.Resources.Places();
            _placeSearchResult = new PlaceSearchResult();

            _mockedRetriever = new Mock<IPlacesSearchRetriever>();
            _mockedRetriever
                .Setup(it => it.RetrieveFrom(It.IsAny<SearchRequest>()))
                .Returns(_retrievedPlaces);
            _mockedRetriever
                .Setup(it => it.RetrieveFrom(It.IsAny<PlaceSearchPaginationRequest>()))
                .Returns(_retrievedPlaces);

            _mockedConverter = new Mock<IPlacesConverter>();
            _mockedConverter
                .Setup(it => it.ToEntity(It.IsAny<Client.Resources.Places>()))
                .Returns(_placeSearchResult);

            _provider = new PlaceSearcher(
                _mockedRetriever.Object,
                _mockedConverter.Object);
        }

        [TestMethod]
        public void ShouldRetrievePlacesByRadiusForSearchRequest()
        {
            _provider.ByRadius(_aSearchRequest).Should().Be.SameInstanceAs(_placeSearchResult);
        }

        [TestMethod]
        public void ShouldRetrieveResourcesWhenSearchingByRadiusForSearchRequest()
        {
            _provider.ByRadius(_aSearchRequest);
            _mockedRetriever.Verify(it => it.RetrieveFrom(_aSearchRequest), Times.Once());
        }

        [TestMethod]
        public void ShouldConvertToEntityWhenSearchingByRadiusForSearchRequest()
        {
            _provider.ByRadius(_aSearchRequest);
            _mockedConverter.Verify(it => it.ToEntity(_retrievedPlaces), Times.Once());
        }

        [TestMethod]
        public void ShouldRetrievePlacesByRadiusForPaginationRequest()
        {
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
            _provider.ByRadius(_aPaginationRequest);
            _mockedConverter.Verify(it => it.ToEntity(_retrievedPlaces), Times.Once());
        }
    }
}
