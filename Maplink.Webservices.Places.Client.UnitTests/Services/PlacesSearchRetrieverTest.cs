using Maplink.Webservices.Places.Client.Builders;
using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Exceptions;
using Maplink.Webservices.Places.Client.Services;
using Maplink.Webservices.Places.Client.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpTestsEx;

namespace Maplink.Webservices.Places.Client.UnitTests.Services
{
    [TestClass]
    public class PlacesSearchRetrieverTest
    {
        private IPlacesSearchRetriever _retriever;
        private Mock<IRequestBuilder> _mockedRequestBuilder;
        private Mock<IHttpClient> _mockedHttpClient;
        private Mock<IXmlSerializerWrapper> _mockedSerializer;
        private Client.Resources.Places _deserializedPlaces;
        private RadiusSearchRequest _aRadiusSearchRequest;
        private Request _requestBuilt;
        private Response _anOkResponse;
        private Response _anInvalidResponse;
        private Response _anNotFoundResponse;
        private PlaceSearchPaginationRequest _paginationRequest;

        [TestInitialize]
        public void SetUp()
        {
            _aRadiusSearchRequest = new RadiusSearchRequest();
            _paginationRequest = new PlaceSearchPaginationRequest();

            _anOkResponse = new Response { Success = true, StatusCode = 200, Body = "body-content" };
            _anInvalidResponse = new Response { Success = false, StatusCode = 400 };
            _anNotFoundResponse = new Response { Success = false, StatusCode = 404 };

            _mockedRequestBuilder = new Mock<IRequestBuilder>();
            _mockedHttpClient = new Mock<IHttpClient>();
            _mockedSerializer = new Mock<IXmlSerializerWrapper>();

            _retriever = new PlacesSearchRetriever(
                _mockedRequestBuilder.Object,
                _mockedHttpClient.Object,
                _mockedSerializer.Object);
        }

        [TestMethod]
        public void ShouldRetrivePlacesFromRadiusRequest()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasOk()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_aRadiusSearchRequest)
                .Should().Be.EqualTo(_deserializedPlaces);
        }

        [TestMethod]
        public void ShouldBuildRequestWhenRetrivePlacesFromRadiusRequest()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasOk()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_aRadiusSearchRequest);

            _mockedRequestBuilder
                .Verify(it => it.ForRadiusSearch(_aRadiusSearchRequest), Times.Once());
        }

        [TestMethod]
        public void ShouldSendRequestWhenRetrivePlacesFromRadiusRequest()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasOk()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_aRadiusSearchRequest);

            _mockedHttpClient
                .Verify(it => it.Get(_requestBuilt), Times.Once());
        }

        [TestMethod]
        public void ShouldDeserializeResponseBodyWhenRetrivePlacesFromRadiusRequest()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasOk()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_aRadiusSearchRequest);

            _mockedSerializer
                .Verify(
                    it => 
                        it.Deserialize<Client.Resources.Places>(_anOkResponse.Body), Times.Once());
        }

        [ExpectedException(typeof(PlaceClientRequestException))]
        [TestMethod]
        public void ShouldThrowExceptionWhenRetrievingPlacesFromRadiusRequestRetrievesAInvalidReponse()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasInvalid()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_aRadiusSearchRequest);
        }

        [TestMethod]
        public void ShouldRetrieveAEmptyPlaceFromRadiusRequestWhenRetrievesANotFoundReponse()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasNotFound()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_aRadiusSearchRequest).Retrieved
                .Should().Be.Empty();
        }

        [TestMethod]
        public void ShouldRetrivePlacesFromPaginationRequest()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasOk()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_paginationRequest)
                .Should().Be.EqualTo(_deserializedPlaces);
        }

        [TestMethod]
        public void ShouldBuildRequestWhenRetrivePlacesFromPaginationRequest()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasOk()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_paginationRequest);

            _mockedRequestBuilder
                .Verify(it => it.ForRadiusSearch(_paginationRequest), Times.Once());
        }

        [TestMethod]
        public void ShouldSendRequestWhenRetrivePlacesFromPaginationRequest()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasOk()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_paginationRequest);

            _mockedHttpClient
                .Verify(it => it.Get(_requestBuilt), Times.Once());
        }

        [TestMethod]
        public void ShouldDeserializeResponseBodyWhenRetrivePlacesFromPaginationRequest()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasOk()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_paginationRequest);

            _mockedSerializer
                .Verify(
                    it => 
                        it.Deserialize<Client.Resources.Places>(_anOkResponse.Body), Times.Once());
        }

        [ExpectedException(typeof(PlaceClientRequestException))]
        [TestMethod]
        public void ShouldThrowExceptionWhenRetrievingPlacesFromPaginationRequestRetrievesAInvalidReponse()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasInvalid()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_paginationRequest);
        }

        [TestMethod]
        public void ShouldRetrieveAEmptyPlaceFromPaginationRequestWhenRetrievesANotFoundReponse()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasNotFound()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_paginationRequest).Retrieved
                .Should().Be.Empty();
        }

        private PlacesSearchRetrieverTest GivenTheRequestWasBuilt()
        {
            _requestBuilt = new Request();

            _mockedRequestBuilder
                .Setup(it => it.ForRadiusSearch(It.IsAny<RadiusSearchRequest>()))
                .Returns(_requestBuilt);

            _mockedRequestBuilder
                .Setup(it => it.ForRadiusSearch(It.IsAny<PlaceSearchPaginationRequest>()))
                .Returns(_requestBuilt);

            return this;
        }

        private PlacesSearchRetrieverTest AndTheResponseWasOk()
        {
            _mockedHttpClient
                 .Setup(it => it.Get(It.IsAny<Request>()))
                 .Returns(_anOkResponse);

            return this;
        }

        private PlacesSearchRetrieverTest AndTheResponseWasNotFound()
        {
            _mockedHttpClient
                 .Setup(it => it.Get(It.IsAny<Request>()))
                 .Returns(_anNotFoundResponse);

            return this;
        }

        private PlacesSearchRetrieverTest AndTheResponseWasInvalid()
        {
            _mockedHttpClient
                .Setup(it => it.Get(It.IsAny<Request>()))
                .Returns(_anInvalidResponse);

            return this;
        }

        private void AndTheResponseWasDeserialized()
        {
            _deserializedPlaces = new Client.Resources.Places();

            _mockedSerializer
                .Setup(
                    it =>
                        it.Deserialize<Client.Resources.Places>(It.IsAny<string>()))
                .Returns(_deserializedPlaces);
        }
    }
}
