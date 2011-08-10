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
        private RadiusSearchRequest _aRadiusRequest;
        private PlaceSearchResult _placeSearchResult;
        private Client.Resources.Places _retrievedPlaces;

        [TestInitialize]
        public void SetUp()
        {
            _aRadiusRequest = new RadiusSearchRequest();
            _retrievedPlaces = new Client.Resources.Places();
            _placeSearchResult = new PlaceSearchResult();

            _mockedRetriever = new Mock<IPlacesSearchRetriever>();
            _mockedRetriever
                .Setup(it => it.RetrieveFrom(It.IsAny<RadiusSearchRequest>()))
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
        public void ShouldRetrievePlacesByRadius()
        {
            _provider.ByRadius(_aRadiusRequest).Should().Be.SameInstanceAs(_placeSearchResult);
        }

        [TestMethod]
        public void ShouldRetrieveResourcesWhenSearchingByRadius()
        {
            _provider.ByRadius(_aRadiusRequest);
            _mockedRetriever.Verify(it => it.RetrieveFrom(_aRadiusRequest), Times.Once());
        }

        [TestMethod]
        public void ShouldConvertToEntityWhenSearchingByRadius()
        {
            _provider.ByRadius(_aRadiusRequest);
            _mockedConverter.Verify(it => it.ToEntity(_retrievedPlaces), Times.Once());
        }
    }
}
