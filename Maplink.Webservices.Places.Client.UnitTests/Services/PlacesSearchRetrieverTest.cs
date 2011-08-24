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
        private Request _requestBuilt;
        private Response _anOkResponse;
        private Response _anInvalidResponse;
        private Response _anNotFoundResponse;
        private CustomRequest _customRequest;
        private SearchRequest _aSearchRequest;

        [TestInitialize]
        public void SetUp()
        {
            _aSearchRequest = new SearchRequest();
            _customRequest = new CustomRequest();

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
        public void ShouldRetrievePlacesFromRequest()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasOk()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_aSearchRequest)
                .Should().Be.EqualTo(_deserializedPlaces);
        }

        [TestMethod]
        public void ShouldBuildRequestWhenRetrievingPlacesFromRequest()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasOk()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_aSearchRequest);

            _mockedRequestBuilder
                .Verify(it => it.For(_aSearchRequest), Times.Once());
        }

        [TestMethod]
        public void ShouldSendRequestWhenRetrievingPlacesFromRequest()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasOk()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_aSearchRequest);

            _mockedHttpClient
                .Verify(it => it.Get(_requestBuilt), Times.Once());
        }

        [TestMethod]
        public void ShouldDeserializeResponseBodyWhenRetrievingPlacesFromRequest()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasOk()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_aSearchRequest);

            _mockedSerializer
                .Verify(
                    it => 
                        it.Deserialize<Client.Resources.Places>(_anOkResponse.Body), Times.Once());
        }

        [ExpectedException(typeof(PlaceClientRequestException))]
        [TestMethod]
        public void ShouldThrowExceptionWhenRetrievingPlacesFromRequestRetrievesAInvalidReponse()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasInvalid()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_aSearchRequest);
        }

        [TestMethod]
        public void ShouldRetrieveAnEmptyPlaceFromRequestWhenRetrievingANotFoundReponse()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasNotFound()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_aSearchRequest).Retrieved
                .Should().Be.Empty();
        }

        [TestMethod]
        public void ShouldRetrievePlacesFromPaginationRequest()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasOk()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_customRequest)
                .Should().Be.EqualTo(_deserializedPlaces);
        }

        [TestMethod]
        public void ShouldBuildRequestWhenRetrievingPlacesFromCustomRequest()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasOk()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_customRequest);

            _mockedRequestBuilder
                .Verify(it => it.ForCustomRequest(_customRequest), Times.Once());
        }

        [TestMethod]
        public void ShouldSendRequestWhenRetrievingPlacesFromPaginationRequest()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasOk()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_customRequest);

            _mockedHttpClient
                .Verify(it => it.Get(_requestBuilt), Times.Once());
        }

        [TestMethod]
        public void ShouldDeserializeResponseBodyWhenRetrievingPlacesFromPaginationRequest()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasOk()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_customRequest);

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

            _retriever.RetrieveFrom(_customRequest);
        }

        [TestMethod]
        public void ShouldRetrieveAEmptyPlaceFromPaginationRequestWhenRetrievesANotFoundReponse()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasNotFound()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_customRequest).Retrieved
                .Should().Be.Empty();
        }

        private PlacesSearchRetrieverTest GivenTheRequestWasBuilt()
        {
            _requestBuilt = new Request();

            _mockedRequestBuilder
                .Setup(it => it.For(It.IsAny<SearchRequest>()))
                .Returns(_requestBuilt);

            _mockedRequestBuilder
                .Setup(it => it.ForCustomRequest(It.IsAny<CustomRequest>()))
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
