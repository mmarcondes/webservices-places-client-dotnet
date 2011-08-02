using System;
using Maplink.Webservices.Places.Client.Builders;
using Maplink.Webservices.Places.Client.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharpTestsEx;

namespace Maplink.Webservices.Client.Places.UnitTests.Builders
{
    [TestClass]
    public class SignatureBuilderTest
    {
        private SignatureBuilder _builder;
        private Mock<IHmacSha1HashGenerator> _mockedHashGenerator;
        private const string AHttpVerb = "http-verb";
        private const string AnUri = "an-uri";
        private const string ALogin = "a-login";
        private const string AKey = "a-key";
        private DateTime _aDate;
        private const string SignatureBuilt = "signature-built";

        [TestInitialize]
        public void SetUp()
        {
            _aDate = new DateTime(2011, 08, 01);

            _mockedHashGenerator = new Mock<IHmacSha1HashGenerator>();
            _mockedHashGenerator
                .Setup(it => it.For(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(SignatureBuilt);

            _builder = new SignatureBuilder(_mockedHashGenerator.Object);
        }

        [TestMethod]
        public void ShouldGenerateSignature()
        {
            _builder.For(AHttpVerb, _aDate, AnUri, ALogin, AKey)
                .Should().Be.EqualTo(SignatureBuilt);
        }

        [TestMethod]
        public void ShouldGenerateHashWhenGeneratingSignature()
        {
            _builder.For(AHttpVerb, _aDate, AnUri, ALogin, AKey);

            var expectedContent = 
                String.Format(
                    "{0}\n{1}\n{2}\n{3}",
                    AHttpVerb.ToUpper(),
                    _aDate.ToString("r"),
                    AnUri,
                    ALogin);

            _mockedHashGenerator
                .Verify(
                    it => it.For(expectedContent, AKey), Times.Once());
        }
    }
}
