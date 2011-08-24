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
    public class HttpRequestBuilderTest
    {
        private IHttpRequestBuilder _builder;
        private Mock<IUriBuilder> _mockedUriBuilder;
        private Mock<IClock> _mockedClock;
        private Mock<ISignatureBuilder> _mockedSignatureBuider;
        private Mock<IAllHttpHeadersBuilder> _mockedAllHeadersBuilder;
        private IEnumerable<KeyValuePair<string, string>> _headersBuilt;
        private DateTime _dateRetrieved;
        private Request _request;
        private const string UriBuilt = "uri-built";
        private const string SignatureBuilt = "signature-built";

        [TestInitialize]
        public void SetUp()
        {
            CreateSearchRequest();

            _headersBuilt = new Dictionary<string, string>();
            _mockedUriBuilder = new Mock<IUriBuilder>();
            _mockedClock = new Mock<IClock>();
            _mockedSignatureBuider = new Mock<ISignatureBuilder>();
            _mockedAllHeadersBuilder = new Mock<IAllHttpHeadersBuilder>();

            _builder = new HttpRequestBuilder(
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

            var request = _builder.For(_request);

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

            _builder.For(_request);

            _mockedUriBuilder.Verify(it => it.For(_request), Times.Once());
        }

        [TestMethod]
        public void ShouldGetRequestDateWhenBuildingARequestForSearch()
        {
            GivenTheUriWasBuiltForSearchRequest()
                .AndTheDateWasRetrieved()
                .AndTheSignatureWasBuilt()
                .AndTheHeadersWereBuilt();

            _builder.For(_request);

            _mockedClock.Verify(it => it.UtcHourNow(), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSignatureWhenBuildingARequestForSearch()
        {
            GivenTheUriWasBuiltForSearchRequest()
                .AndTheDateWasRetrieved()
                .AndTheSignatureWasBuilt()
                .AndTheHeadersWereBuilt();

            _builder.For(_request);

            _mockedSignatureBuider
                .Verify(
                    it =>
                        it.For(
                            "get",
                            _dateRetrieved,
                            UriBuilt,
                            _request.LicenseLogin,
                            _request.LicenseKey), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildAllHeadersWhenBuildingARequestForSearch()
        {
            GivenTheUriWasBuiltForSearchRequest()
                .AndTheDateWasRetrieved()
                .AndTheSignatureWasBuilt()
                .AndTheHeadersWereBuilt();

            _builder.For(_request);

            _mockedAllHeadersBuilder
                .Verify(
                    it =>
                        it.For(
                            _dateRetrieved,
                            _request.LicenseLogin,
                            SignatureBuilt), Times.Once());
        }

        private void CreateSearchRequest()
        {
            _request = new Request
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

        private HttpRequestBuilderTest GivenTheUriWasBuiltForPaginationRequest()
        {
            _mockedUriBuilder
                .Setup(it => it.ForPagination(It.IsAny<string>()))
                .Returns(UriBuilt);

            return this;
        }

        private HttpRequestBuilderTest GivenTheUriWasBuiltForSearchRequest()
        {
            _mockedUriBuilder
                .Setup(it => it.For(It.IsAny<Request>()))
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

        private HttpRequestBuilderTest AndTheSignatureWasBuilt()
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

        private HttpRequestBuilderTest AndTheDateWasRetrieved()
        {
            _dateRetrieved = new DateTime(2011, 08, 01);

            _mockedClock
                .Setup(it => it.UtcHourNow())
                .Returns(_dateRetrieved);

            return this;
        }
    }
}
