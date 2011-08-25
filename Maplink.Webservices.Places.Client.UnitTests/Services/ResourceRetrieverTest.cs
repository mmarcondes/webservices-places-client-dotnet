using Maplink.Webservices.Places.Client.Builders;
using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Exceptions;
using Maplink.Webservices.Places.Client.Resources;
using Maplink.Webservices.Places.Client.Services;
using Maplink.Webservices.Places.Client.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpTestsEx;

namespace Maplink.Webservices.Places.Client.UnitTests.Services
{
    [TestClass]
    public class ResourceRetrieverTest
    {
        private IResourceRetriever _retriever;
        private Mock<IHttpRequestBuilder> _mockedRequestBuilder;
        private Mock<IHttpClient> _mockedHttpClient;
        private Mock<IXmlSerializerWrapper> _mockedSerializer;
        private DummyResource _deserializedResource;
        private HttpRequest _httpRequestBuilt;
        private HttpResponse _anOkHttpResponse;
        private HttpResponse _anInvalidHttpResponse;
        private HttpResponse _anNotFoundHttpResponse;
        private Request _aRequest;

        [TestInitialize]
        public void SetUp()
        {
            _aRequest = new Request();

            _anOkHttpResponse = new HttpResponse { Success = true, StatusCode = 200, Body = "body-content" };
            _anInvalidHttpResponse = new HttpResponse { Success = false, StatusCode = 400 };
            _anNotFoundHttpResponse = new HttpResponse { Success = false, StatusCode = 404 };

            _mockedRequestBuilder = new Mock<IHttpRequestBuilder>();
            _mockedHttpClient = new Mock<IHttpClient>();
            _mockedSerializer = new Mock<IXmlSerializerWrapper>();

            _retriever = new ResourceRetriever(
                _mockedRequestBuilder.Object,
                _mockedHttpClient.Object,
                _mockedSerializer.Object);
        }

        [TestMethod]
        public void ShouldRetrieveResourceFromRequest()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasOk()
                .AndTheResponseWasDeserialized();

            _retriever.From<DummyResource>(_aRequest)
                .Should().Be.EqualTo(_deserializedResource);
        }

        [TestMethod]
        public void ShouldRetrieveCategoriesFromRequest()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasOk()
                .AndTheResponseWasDeserialized();

            _retriever.From<DummyResource>(_aRequest)
                .Should().Be.EqualTo(_deserializedResource);
        }

        [TestMethod]
        public void ShouldBuildRequestWhenRetrievingPlacesFromRequest()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasOk()
                .AndTheResponseWasDeserialized();

            _retriever.From<DummyResource>(_aRequest);

            _mockedRequestBuilder
                .Verify(it => it.For(_aRequest), Times.Once());
        }

        [TestMethod]
        public void ShouldSendRequestWhenRetrievingPlacesFromRequest()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasOk()
                .AndTheResponseWasDeserialized();

            _retriever.From<DummyResource>(_aRequest);

            _mockedHttpClient
                .Verify(it => it.Get(_httpRequestBuilt), Times.Once());
        }

        [TestMethod]
        public void ShouldDeserializeResponseBodyWhenRetrievingPlacesFromRequest()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasOk()
                .AndTheResponseWasDeserialized();

            _retriever.From<DummyResource>(_aRequest);

            _mockedSerializer
                .Verify(
                    it =>
                        it.Deserialize<DummyResource>(_anOkHttpResponse.Body), Times.Once());
        }

        [ExpectedException(typeof(PlaceClientRequestException))]
        [TestMethod]
        public void ShouldThrowExceptionWhenRetrievingPlacesFromRequestRetrievesAInvalidReponse()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasInvalid()
                .AndTheResponseWasDeserialized();

            _retriever.From<DummyResource>(_aRequest);
        }

        [TestMethod]
        public void ShouldRetrieveADefaultResourceFromRequestWhenRetrievingANotFoundReponse()
        {
            GivenTheRequestWasBuilt()
                .AndTheResponseWasNotFound()
                .AndTheResponseWasDeserialized();

            var dummyResourceRetrieved = _retriever.From<DummyResource>(_aRequest);
            dummyResourceRetrieved.Should().Be.EqualTo(new DummyResource());
        }

        private ResourceRetrieverTest GivenTheRequestWasBuilt()
        {
            _httpRequestBuilt = new HttpRequest();

            _mockedRequestBuilder
                .Setup(it => it.For(It.IsAny<Request>()))
                .Returns(_httpRequestBuilt);

            return this;
        }

        private ResourceRetrieverTest AndTheResponseWasOk()
        {
            _mockedHttpClient
                 .Setup(it => it.Get(It.IsAny<HttpRequest>()))
                 .Returns(_anOkHttpResponse);

            return this;
        }

        private ResourceRetrieverTest AndTheResponseWasNotFound()
        {
            _mockedHttpClient
                 .Setup(it => it.Get(It.IsAny<HttpRequest>()))
                 .Returns(_anNotFoundHttpResponse);

            return this;
        }

        private ResourceRetrieverTest AndTheResponseWasInvalid()
        {
            _mockedHttpClient
                .Setup(it => it.Get(It.IsAny<HttpRequest>()))
                .Returns(_anInvalidHttpResponse);

            return this;
        }

        private void AndTheResponseWasDeserialized()
        {
            _deserializedResource = new DummyResource();

            _mockedSerializer
                .Setup(
                    it =>
                        it.Deserialize<DummyResource>(It.IsAny<string>()))
                .Returns(_deserializedResource);
        }
    }
}
