using System;
using System.Collections.Generic;
using Maplink.Webservices.Places.Client.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpTestsEx;

namespace Maplink.Webservices.Client.Places.UnitTests.Builders
{
    [TestClass]
    public class AllHeadersBuilderTest
    {
        private AllHeadersBuilder _builder;
        private Mock<IHeaderBuilder> _mockedHeaderBuilder;
        private Mock<IAuthorizationBuilder> _mockedAuthorizationBuilder;
        private DateTime _aDate;
        private KeyValuePair<string, string> _authorizationHeader;
        private KeyValuePair<string, string> _xMaplinkDateHeader;
        private const string Login = "login";
        private const string Signature = "signature";
        private const string AuthorizationBuilt = "authorization-built";

        [TestInitialize]
        public void SetUp()
        {
            _aDate = new DateTime(2011, 08, 01);

            _authorizationHeader = new KeyValuePair<string, string>("authorization", "header");
            _xMaplinkDateHeader = new KeyValuePair<string, string>("x-maplink-date", "header");
            
            _mockedHeaderBuilder = new Mock<IHeaderBuilder>();
            _mockedHeaderBuilder
                .Setup(it => it.ForAuthorization(It.IsAny<string>()))
                .Returns(_authorizationHeader);

            _mockedHeaderBuilder
                .Setup(it => it.ForXMaplinkDate(It.IsAny<DateTime>()))
                .Returns(_xMaplinkDateHeader);

            _mockedAuthorizationBuilder = new Mock<IAuthorizationBuilder>();
            _mockedAuthorizationBuilder
                .Setup(it => it.For(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(AuthorizationBuilt);
            
            _builder = new AllHeadersBuilder(
                _mockedHeaderBuilder.Object,
                _mockedAuthorizationBuilder.Object);
        }

        [TestMethod]
        public void ShouldRetrieveAllHeaders()
        {
            var headers = _builder.For(_aDate, Login, Signature);

            headers.Should().Have.Count.EqualTo(2);
            headers.Should().Contain(_authorizationHeader);
            headers.Should().Contain(_xMaplinkDateHeader);
        }

        [TestMethod]
        public void ShouldBuildXMaplinkDateHeaderWhenRetrievingAllHeaders()
        {
            _builder.For(_aDate, Login, Signature);

            _mockedHeaderBuilder.Verify(it => it.ForXMaplinkDate(_aDate), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildAuthorizationHeaderWhenRetrievingAllHeaders()
        {
            _builder.For(_aDate, Login, Signature);

            _mockedHeaderBuilder.Verify(it => it.ForAuthorization(AuthorizationBuilt), Times.Once());
        }

        [TestMethod]
        public void ShouldBuildAuthorizationValueWhenRetrievingAllHeaders()
        {
            _builder.For(_aDate, Login, Signature);

            _mockedAuthorizationBuilder.Verify(it => it.For(Login, Signature), Times.Once());
        }
    }
}
