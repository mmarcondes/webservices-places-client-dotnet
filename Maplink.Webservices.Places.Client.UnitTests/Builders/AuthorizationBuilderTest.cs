using Maplink.Webservices.Places.Client.Builders;
using Maplink.Webservices.Places.Client.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpTestsEx;

namespace Maplink.Webservices.Client.Places.UnitTests.Builders
{
    [TestClass]
    public class AuthorizationBuilderTest
    {
        private IAuthorizationBuilder _builder;
        private Mock<IBase64Encoder> _mockedBase64Encoder;
        private const string EncodedAuthorization = "encoded-authorization";
        private const string Login = "login";
        private const string Signature = "signature";

        [TestInitialize]
        public void SetUp()
        {
            _mockedBase64Encoder = new Mock<IBase64Encoder>();
            _mockedBase64Encoder
                .Setup(it => it.Encode(It.IsAny<string>()))
                .Returns(EncodedAuthorization);

            _builder = new AuthorizationBuilder(_mockedBase64Encoder.Object);
        }

        [TestMethod]
        public void ShouldBuildAuthorization()
        {
            _builder.For(Login, Signature).Should().Be.EqualTo(EncodedAuthorization);
        }

        [TestMethod]
        public void ShouldEncodeInBase64StringWhenBuildingAuthorization()
        {
            _builder.For(Login, Signature);

            _mockedBase64Encoder.Verify(it => it.Encode("login:signature"), Times.Once());
        }
    }
}
