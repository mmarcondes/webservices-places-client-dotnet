using System;
using System.Collections.Generic;
using Maplink.Webservices.Places.Client.Builders;
using Maplink.Webservices.Places.Client.Entities;
using Maplink.Webservices.Places.Client.Wrappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpTestsEx;

namespace Maplink.Webservices.Client.Places.UnitTests.Builders
{
    [TestClass]
    public class RequestBuilderTest
    {
        private IRequestBuilder _builder;
        private Mock<IUriBuilder> _mockedUriBuilder;
        private Mock<IClock> _mockedClock;
        private Mock<ISignatureBuilder> _mockedSignatureBuider;
        private Mock<IAllHeadersBuilder> _mockedAllHeadersBuilder;
        private RadiusSearchRequest _radiusRequest;
        private IEnumerable<KeyValuePair<string, string>>  _headersBuilt;
        private DateTime _dateRetrieved;
        private const string UriBuilt = "uri-built";
        private const string SignatureBuilt = "signature-built";

        [TestInitialize]
        public void SetUp()
        {
            _radiusRequest = new RadiusSearchRequest
                                 {
                                     Key = "key",
                                     Latitude = -12.432,
                                     Longitude = -24.124,
                                     Login = "login",
                                     Radius = 3
                                 };

            _headersBuilt = new Dictionary<string, string>();

            _dateRetrieved = new DateTime(2011, 08, 01);

            _mockedUriBuilder = new Mock<IUriBuilder>();
            _mockedUriBuilder
                .Setup(it => it.ForRadiusSearch(It.IsAny<RadiusSearchRequest>()))
                .Returns(UriBuilt);

            _mockedClock = new Mock<IClock>();
            _mockedClock
                .Setup(it => it.UtcHourNow())
                .Returns(_dateRetrieved);

            _mockedSignatureBuider = new Mock<ISignatureBuilder>();
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
            
            _mockedAllHeadersBuilder = new Mock<IAllHeadersBuilder>();
            _mockedAllHeadersBuilder
                .Setup(
                    it =>
                        it.For(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_headersBuilt);

            _builder = new RequestBuilder(
                _mockedUriBuilder.Object,
                _mockedClock.Object,
                _mockedSignatureBuider.Object,
                _mockedAllHeadersBuilder.Object);
        }

        [TestMethod]
        public void ShouldBuildARequestForRadiusSearch()
        {
            var request = _builder.ForRadiusSearch(_radiusRequest);

            request.Uri.Should().Be.EqualTo(UriBuilt);
            request.Headers.Should().Be.SameInstanceAs(_headersBuilt);
        }

        [TestMethod]
        public void ShouldBuildUriWhenBuildingARequestForRadiusSearch()
        {
            _builder.ForRadiusSearch(_radiusRequest);

            _mockedUriBuilder.Verify(it => it.ForRadiusSearch(_radiusRequest), Times.Once());
        }

        [TestMethod]
        public void ShouldGetRequestDateWhenBuildingARequestForRadiusSearch()
        {
            _builder.ForRadiusSearch(_radiusRequest);

            _mockedClock.Verify(it => it.UtcHourNow(), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildSignatureWhenBuildingARequestForRadiusSearch()
        {
            _builder.ForRadiusSearch(_radiusRequest);

            _mockedSignatureBuider
                .Verify(
                    it => 
                        it.For(
                            "get",
                            _dateRetrieved,
                            UriBuilt,
                            _radiusRequest.Login,
                            _radiusRequest.Key), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildAllHeadersWhenBuildingARequestForRadiusSearch()
        {
            _builder.ForRadiusSearch(_radiusRequest);

            _mockedAllHeadersBuilder
                .Verify(
                    it => 
                        it.For(
                            _dateRetrieved,
                            _radiusRequest.Login,
                            SignatureBuilt), Times.Once());
        }
    }
}
