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
        private Mock<IHttpRequestBuilder> _mockedRequestBuilder;
        private Mock<IHttpClient> _mockedHttpClient;
        private Mock<IXmlSerializerWrapper> _mockedSerializer;
        private Client.Resources.Places _deserializedPlaces;
        private HttpRequest _httpRequestBuilt;
        private HttpResponse _anOkHttpResponse;
        private HttpResponse _anInvalidHttpResponse;
        private HttpResponse _anNotFoundHttpResponse;
        private CustomRequest _customRequest;
        private Request _aRequest;

        [TestInitialize]
        public void SetUp()
        {
            _aRequest = new Request();
            _customRequest = new CustomRequest();

            _anOkHttpResponse = new HttpResponse { Success = true, StatusCode = 200, Body = "body-content" };
            _anInvalidHttpResponse = new HttpResponse { Success = false, StatusCode = 400 };
            _anNotFoundHttpResponse = new HttpResponse { Success = false, StatusCode = 404 };

            _mockedRequestBuilder = new Mock<IHttpRequestBuilder>();
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

            _retriever.RetrieveFrom(_aRequest)
                .Should().Be.EqualTo(_deserializedPlaces);
        }

        [TestMethod]
        public void ShouldBuildRequestWhenRetrievingPlacesFromRequest()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasOk()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_aRequest);

            _mockedRequestBuilder
                .Verify(it => it.For(_aRequest), Times.Once());
        }

        [TestMethod]
        public void ShouldSendRequestWhenRetrievingPlacesFromRequest()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasOk()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_aRequest);

            _mockedHttpClient
                .Verify(it => it.Get(_httpRequestBuilt), Times.Once());
        }

        [TestMethod]
        public void ShouldDeserializeResponseBodyWhenRetrievingPlacesFromRequest()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasOk()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_aRequest);

            _mockedSerializer
                .Verify(
                    it => 
                        it.Deserialize<Client.Resources.Places>(_anOkHttpResponse.Body), Times.Once());
        }

        [ExpectedException(typeof(PlaceClientRequestException))]
        [TestMethod]
        public void ShouldThrowExceptionWhenRetrievingPlacesFromRequestRetrievesAInvalidReponse()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasInvalid()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_aRequest);
        }

        [TestMethod]
        public void ShouldRetrieveAnEmptyPlaceFromRequestWhenRetrievingANotFoundReponse()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasNotFound()
                .AndTheResponseWasDeserialized();

            _retriever.RetrieveFrom(_aRequest).Retrieved
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
                .Verify(it => it.Get(_httpRequestBuilt), Times.Once());
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
                        it.Deserialize<Client.Resources.Places>(_anOkHttpResponse.Body), Times.Once());
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
            _httpRequestBuilt = new HttpRequest();

            _mockedRequestBuilder
                .Setup(it => it.For(It.IsAny<Request>()))
                .Returns(_httpRequestBuilt);

            _mockedRequestBuilder
                .Setup(it => it.ForCustomRequest(It.IsAny<CustomRequest>()))
                .Returns(_httpRequestBuilt);

            return this;
        }

        private PlacesSearchRetrieverTest AndTheResponseWasOk()
        {
            _mockedHttpClient
                 .Setup(it => it.Get(It.IsAny<HttpRequest>()))
                 .Returns(_anOkHttpResponse);

            return this;
        }

        private PlacesSearchRetrieverTest AndTheResponseWasNotFound()
        {
            _mockedHttpClient
                 .Setup(it => it.Get(It.IsAny<HttpRequest>()))
                 .Returns(_anNotFoundHttpResponse);

            return this;
        }

        private PlacesSearchRetrieverTest AndTheResponseWasInvalid()
        {
            _mockedHttpClient
                .Setup(it => it.Get(It.IsAny<HttpRequest>()))
                .Returns(_anInvalidHttpResponse);

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
