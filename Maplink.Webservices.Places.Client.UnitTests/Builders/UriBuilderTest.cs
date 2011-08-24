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
    public class UriBuilderTest
    {
        private IUriBuilder _builder;
        private Mock<IConfigurationWrapper> _mockedConfiguration;
        private Mock<IUriQueryBuilder> _mockedUriQueryBuilder;
        private Request _request;

        [TestInitialize]
        public void SetUp()
        {
            _request = new Request
                                 {
                                     Arguments = new List<KeyValuePair<string, string>>(),
                                     UriPath = "uri/path"
                                 };

            _mockedConfiguration = new Mock<IConfigurationWrapper>();
            _mockedConfiguration
                .Setup(it => it.ValueFor(It.IsAny<string>()))
                .Returns("base-uri");

            _mockedUriQueryBuilder = new Mock<IUriQueryBuilder>();
            _mockedUriQueryBuilder
                .Setup(it => it.Build(It.IsAny<IEnumerable<KeyValuePair<string, string>>>()))
                .Returns("query-built");

            _builder = new UriBuilder(_mockedConfiguration.Object, _mockedUriQueryBuilder.Object);      
        }

        [TestMethod]
        public void ShouldCreateForPaginationRequest()
        {
            const string expectedUri = "base-uriuri";

            _builder.ForPagination("uri").Should().Be.EqualTo(expectedUri);
        }

        [TestMethod]
        public void ShouldGetBaseUriFromConfigurationWhenCreatingForPaginationRequest()
        {
            _builder.ForPagination("uri");

            _mockedConfiguration
                .Verify(it => it.ValueFor("Maplink.Webservices.Places.BaseUri"), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildUriForSearchRequest()
        {
            _builder.For(_request).Should().Be.EqualTo("base-uri/uri/path?query-built");
        }

        [TestMethod]
        public void ShouldBuildUriQueryWhenBuildingUriForSearchRequest()
        {
            _builder.For(_request);
            _mockedUriQueryBuilder
                .Verify(it => it.Build(_request.Arguments), Times.Once());
        }

        [TestMethod]
        public void ShouldGetBaseUriWhenBuildingUriForSearchRequest()
        {
            _builder.For(_request);
            _mockedConfiguration
                .Verify(it => it.ValueFor("Maplink.Webservices.Places.BaseUri"), Times.Once());
        }
    }
}
