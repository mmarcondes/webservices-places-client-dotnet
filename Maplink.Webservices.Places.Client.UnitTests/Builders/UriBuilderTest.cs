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
        private RadiusSearchRequest _aRadiusRequest;
        private Mock<IUriQueryBuilder> _mockedUriQueryBuilder;
        private SearchRequest _searchRequest;

        [TestInitialize]
        public void SetUp()
        {
            _aRadiusRequest = new RadiusSearchRequest
            {
                Radius = 3.3,
                Latitude = -32.345,
                Longitude = 12.325263,
                StartsAtIndex = 10
            };

            _searchRequest = new SearchRequest
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
        public void ShouldCreateForRadiusSearch()
        {
            const string expectedUri = "base-uri/places/byradius/?radius=3.3&latitude=-32.345&longitude=12.325263&start=10";

            _builder.ForRadiusSearch(_aRadiusRequest).Should().Be.EqualTo(expectedUri);
        }

        [TestMethod]
        public void ShouldGetBaseUriFromConfigurationWhenCreatingForRadiusSearch()
        {
            _builder.ForRadiusSearch(_aRadiusRequest);

            _mockedConfiguration
                .Verify(it => it.ValueFor("Maplink.Webservices.Places.BaseUri"), Times.Once());
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
            _builder.For(_searchRequest).Should().Be.EqualTo("base-uri/uri/path?query-built");
        }

        [TestMethod]
        public void ShouldBuildUriQueryWhenBuildingUriForSearchRequest()
        {
            _builder.For(_searchRequest);
            _mockedUriQueryBuilder
                .Verify(it => it.Build(_searchRequest.Arguments), Times.Once());
        }

        [TestMethod]
        public void ShouldGetBaseUriWhenBuildingUriForSearchRequest()
        {
            _builder.For(_searchRequest);
            _mockedConfiguration
                .Verify(it => it.ValueFor("Maplink.Webservices.Places.BaseUri"), Times.Once());
        }
    }
}
