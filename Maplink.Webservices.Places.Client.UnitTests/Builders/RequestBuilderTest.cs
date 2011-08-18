using System;
using System.Collections.Generic;
using Maplink.Webservices.Places.Client.Builders;
using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpTestsEx;

namespace Maplink.Webservices.Places.Client.UnitTests.Builders
{
    [TestClass]
    public class RequestBuilderTest
    {
        private IRequestBuilder _builder;
        private Mock<IUriBuilder> _mockedUriBuilder;
        private Mock<IClock> _mockedClock;
        private Mock<ISignatureBuilder> _mockedSignatureBuider;
        private Mock<IAllHeadersBuilder> _mockedAllHeadersBuilder;
        private IEnumerable<KeyValuePair<string, string>> _headersBuilt;
        private DateTime _dateRetrieved;
        private PlaceSearchPaginationRequest _paginationRequest;
        private SearchRequest _searchRequest;
        private const string UriBuilt = "uri-built";
        private const string SignatureBuilt = "signature-built";

        [TestInitialize]
        public void SetUp()
        {
            CreateSearchRequest();

            CreatePaginationRequest();

            _headersBuilt = new Dictionary<string, string>();
            _mockedUriBuilder = new Mock<IUriBuilder>();
            _mockedClock = new Mock<IClock>();
            _mockedSignatureBuider = new Mock<ISignatureBuilder>();
            _mockedAllHeadersBuilder = new Mock<IAllHeadersBuilder>();

            _builder = new RequestBuilder(
                _mockedUriBuilder.Object,
                _mockedClock.Object,
                _mockedSignatureBuider.Object,
                _mockedAllHeadersBuilder.Object);
        }
        [TestMethod]
        public void ShouldBuildARequestForSearch()
        {
            GivenTheUriWasBuiltForSearchRequest()
                .AndTheDateWasRetrieved()
                .AndTheSignatureWasBuilt()
                .AndTheHeadersWereBuilt();

            var request = _builder.For(_searchRequest);

            request.Uri.Should().Be.EqualTo(UriBuilt);
            request.Headers.Should().Be.SameInstanceAs(_headersBuilt);
        }

        [TestMethod]
        public void ShouldBuildUriWhenBuildingARequestForSearch()
        {
            GivenTheUriWasBuiltForSearchRequest()
                .AndTheDateWasRetrieved()
                .AndTheSignatureWasBuilt()
                .AndTheHeadersWereBuilt();

            _builder.For(_searchRequest);

            _mockedUriBuilder.Verify(it => it.For(_searchRequest), Times.Once());
        }

        [TestMethod]
        public void ShouldGetRequestDateWhenBuildingARequestForSearch()
        {
            GivenTheUriWasBuiltForSearchRequest()
                .AndTheDateWasRetrieved()
                .AndTheSignatureWasBuilt()
                .AndTheHeadersWereBuilt();

            _builder.For(_searchRequest);

            _mockedClock.Verify(it => it.UtcHourNow(), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSignatureWhenBuildingARequestForSearch()
        {
            GivenTheUriWasBuiltForSearchRequest()
                .AndTheDateWasRetrieved()
                .AndTheSignatureWasBuilt()
                .AndTheHeadersWereBuilt();

            _builder.For(_searchRequest);

            _mockedSignatureBuider
                .Verify(
                    it =>
                        it.For(
                            "get",
                            _dateRetrieved,
                            UriBuilt,
                            _searchRequest.LicenseLogin,
                            _searchRequest.LicenseKey), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildAllHeadersWhenBuildingARequestForSearch()
        {
            GivenTheUriWasBuiltForSearchRequest()
                .AndTheDateWasRetrieved()
                .AndTheSignatureWasBuilt()
                .AndTheHeadersWereBuilt();

            _builder.For(_searchRequest);

            _mockedAllHeadersBuilder
                .Verify(
                    it =>
                        it.For(
                            _dateRetrieved,
                            _searchRequest.LicenseLogin,
                            SignatureBuilt), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildARequestForRadiusPaginationSearch()
        {
            GivenTheUriWasBuiltForPaginationRequest()
                .AndTheDateWasRetrieved()
                .AndTheSignatureWasBuilt()
                .AndTheHeadersWereBuilt();

            var request = _builder.ForPaginationSearch(_paginationRequest);

            request.Uri.Should().Be.EqualTo(UriBuilt);
            request.Headers.Should().Be.SameInstanceAs(_headersBuilt);
        }

        [TestMethod]
        public void ShouldBuildUriWhenBuildingARequestForRadiusPaginationSearch()
        {
            _builder.ForPaginationSearch(_paginationRequest);

            _mockedUriBuilder.Verify(it => it.ForPagination(_paginationRequest.Uri), Times.Once());
        }

        [TestMethod]
        public void ShouldGetRequestDateWhenBuildingARequestForRadiusPaginationSearch()
        {
            _builder.ForPaginationSearch(_paginationRequest);

            _mockedClock.Verify(it => it.UtcHourNow(), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSignatureWhenBuildingARequestForRadiusPaginationSearch()
        {
            GivenTheUriWasBuiltForPaginationRequest()
                .AndTheDateWasRetrieved()
                .AndTheSignatureWasBuilt()
                .AndTheHeadersWereBuilt();

            _builder.ForPaginationSearch(_paginationRequest);

            _mockedSignatureBuider
                .Verify(
                    it =>
                        it.For(
                            "get",
                            _dateRetrieved,
                            UriBuilt,
                            _paginationRequest.Login,
                            _paginationRequest.Key), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildAllHeadersWhenBuildingARequestForRadiusPaginationSearch()
        {
            GivenTheUriWasBuiltForPaginationRequest()
                .AndTheDateWasRetrieved()
                .AndTheSignatureWasBuilt()
                .AndTheHeadersWereBuilt();

            _builder.ForPaginationSearch(_paginationRequest);

            _mockedAllHeadersBuilder
                .Verify(
                    it =>
                        it.For(
                            _dateRetrieved,
                            _paginationRequest.Login,
                            SignatureBuilt), Times.Once());
        }

        private void CreatePaginationRequest()
        {
            _paginationRequest = new PlaceSearchPaginationRequest
            {
                Key = "a-key",
                Login = "a-login",
                Uri = "a-uri"
            };
        }

        private void CreateSearchRequest()
        {
            _searchRequest = new SearchRequest
            {
                LicenseKey = "key",
                LicenseLogin = "login",
                Arguments = new List<KeyValuePair<string, string>>
                                                     {
                                                         new KeyValuePair<string, string>("latitude", "-12.432"),
                                                         new KeyValuePair<string, string>("longitude", "-24.124"),
                                                         new KeyValuePair<string, string>("radius", "3")
                                                     }

            };
        }

        private RequestBuilderTest GivenTheUriWasBuiltForPaginationRequest()
        {
            _mockedUriBuilder
                .Setup(it => it.ForPagination(It.IsAny<string>()))
                .Returns(UriBuilt);

            return this;
        }

        private RequestBuilderTest GivenTheUriWasBuiltForSearchRequest()
        {
            _mockedUriBuilder
                .Setup(it => it.For(It.IsAny<SearchRequest>()))
                .Returns(UriBuilt);

            return this;
        }

        private void AndTheHeadersWereBuilt()
        {
            _mockedAllHeadersBuilder
                .Setup(
                    it =>
                    it.For(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_headersBuilt);
        }

        private RequestBuilderTest AndTheSignatureWasBuilt()
        {
            _mockedSignatureBuider
                .Setup(
                    it =>
                    it.For(
                        It.IsAny<string>(),
                        It.IsAny<DateTime>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>()))
                .Returns(SignatureBuilt);

            return this;
        }

        private RequestBuilderTest AndTheDateWasRetrieved()
        {
            _dateRetrieved = new DateTime(2011, 08, 01);

            _mockedClock
                .Setup(it => it.UtcHourNow())
                .Returns(_dateRetrieved);

            return this;
        }
    }
}
